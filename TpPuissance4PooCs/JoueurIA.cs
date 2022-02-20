using System;
using System.Collections.Generic;
using System.Linq;

namespace TpPuissance4PooCs
{
    public class JoueurIA : Joueur
    {
        private Random _randomSeed;
        private List<int> _colonnesToEventuallyPlay = new List<int>();
        private int _niveau;
        private Random RandomSeed { get => _randomSeed; }
        private List<int> ColonnesToEventuallyPlay { get => _colonnesToEventuallyPlay; set => _colonnesToEventuallyPlay = value; }
        public int Niveau { get => _niveau; set => _niveau = value; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="niveauIA">Le niveau varie entre 1 et 6 compris</param>
        public JoueurIA(int numeroJoueur, string nomJoueur, int niveauIA) : base(numeroJoueur, nomJoueur)
        {
            this._randomSeed = new Random();
            this._niveau = niveauIA;
            this._score = 0;
            System.Threading.Thread.Sleep(20);
        }

        // Niveaux :
        // -1 - Niveau de l'IA originale
        //  1 - [1-100 Hasard]
        //  2 - [1-50 WinOuContre] sinon [100 Hasard]
        //  3 - [1-80 WinOuContre] sinon [1-50 Jeu au milieu / 51-100 Hasard]
        //  4 - [1-100 WinOuContre] sinon [1-50 Jeu au milieu / 51-100 Jeu vers la moyenne]
        //  5 - [1-100 WinOuContre] sinon [1-50 Prévision sur deux coups / 51-75 Jeu vers la moyenne / 76-100 Jeu au milieu]
        //  6 - [1-100 WinOuContre] sinon [1-100 Prévision sur deux coups]

        /// <summary>
        /// Permet à une IA de jouer son tour.
        /// </summary>
        public override void Jouer(Grille grille, Puissance4 jeu)
        {
            int ThinkLevel = 0;
            int ThinkTime = 0;
            int StrategyDice = RandomSeed.Next(1, 101);
            int colonne = 0;
            int ligne = 0;
            int timeout = 30;
            bool rester = true;
            Joueur AutreJoueur = new Joueur();

            if (this.NumeroJoueur == 1)
            {
                AutreJoueur = jeu.Joueur2;
            }
            else if (this.NumeroJoueur == 2)
            {
                AutreJoueur = jeu.Joueur1;
            }

            do
            {
                // Strategie complete de l'IA niveau 6 : 
                // 1 - Voir si on peut gagner pour le faire
                // 2 - Voir si l'adversaire peut gagner
                // 3 - Voir pour planifier un stratageme sur deux coups (offensifs comme defensifs)
                // 4 - Jouer là ou il y a des pions a nous
                // 5 - Jouer au milieu
                // 6 - Jouer au hasard

                int NumeroJoueurAdverse = 0;

                if (NumeroJoueur == 1)
                {
                    NumeroJoueurAdverse = 2;
                }
                else if (NumeroJoueur == 2)
                {
                    NumeroJoueurAdverse = 1;
                }

                // Le niveau -1 est celui du prof (qui est a -1 vu que j'ai du l'ajouter apres coup et que je vais quand meme pas ajouter en 6, non mais ça va pas ?, merci)
                if (Niveau == -1)
                {
                    if (this.AnalyserJoueur(this.NumeroJoueur, grille))
                    {
                        colonne = ColonnesToEventuallyPlay[RandomSeed.Next(ColonnesToEventuallyPlay.Count)];
                        ThinkLevel = 1;
                    }
                    else
                    {
                        int[] TableauDesComptes = new int[grille.NbColonnes];
                        int BestScore = 0;
                        for (int i = 0; i < grille.NbColonnes; i++)
                        {
                            ligne = grille.GetLigne(i);
                            int undercolor = 0;
                            int count = 0;
                            for (int j = ligne; j < grille.NbLignes; j++)
                            {
                                if (j + 1 < grille.NbLignes)
                                {
                                    if (undercolor == 0)
                                    {
                                        undercolor = grille.Tableau[i, j + 1];
                                        count++;
                                    }
                                    else
                                    {
                                        if (grille.Tableau[i, j + 1] == undercolor)
                                        {
                                            count++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            TableauDesComptes[i] = count;
                            if (count > BestScore)
                            {
                                BestScore = count;
                            }
                        }

                        int index = 0;
                        foreach (var compte in TableauDesComptes)
                        {
                            if (compte == BestScore)
                            {
                                ColonnesToEventuallyPlay.Add(index);
                            }
                            index++;
                        }

                        colonne = ColonnesToEventuallyPlay[RandomSeed.Next(ColonnesToEventuallyPlay.Count)];
                        ThinkLevel = 2;
                    }

                }
                else
                {
                    // --- WIN N COUNTER ---

                    if ((Niveau == 2 && StrategyDice <= 50) || (Niveau == 3 && StrategyDice <= 80) || Niveau >= 4)
                    {
                        // Tests pour se faire gagner (Offensifs)
                        if (this.AnalyserJoueur(this.NumeroJoueur, grille))
                        {
                            int index = RandomSeed.Next(ColonnesToEventuallyPlay.Count);
                            colonne = ColonnesToEventuallyPlay[index];
                            ThinkLevel = 1;
                        }

                        // Tests pour empécher l'autre de gagner (Defensifs)
                        else if (this.AnalyserJoueur(NumeroJoueurAdverse, grille))
                        {
                            int index = RandomSeed.Next(ColonnesToEventuallyPlay.Count);
                            colonne = ColonnesToEventuallyPlay[index];
                            ThinkLevel = 2;
                        }
                    }

                    // ---------------------

                    if (!ColonnesToEventuallyPlay.Any())
                    {
                        // --- 2 STRIKES 2 WIN ---
                        if ((Niveau == 5 && StrategyDice <= 50) || Niveau >= 6)
                        {
                            colonne = RandomSeed.Next(0, grille.Tableau.GetLength(0));
                            ThinkLevel = 4;
                        }
                        // -----------------------


                        // ---AVERAGE PLAY ---
                        // L'IA compte le nombre de pions qu'il possede dans chaque colonne, les deux dernières lignes. Il jouera dans une colonne juxtaposant celle où il en a le plus
                        else if ((Niveau == 4 && (50 < StrategyDice && StrategyDice <= 100)) || (Niveau == 5 && (50 < StrategyDice && StrategyDice <= 75)))
                        {
                            List<bool> Availables = new List<bool>();
                            List<int> MesTrucsParColonnes = new List<int>();

                            // On parcoure les colonnes et on compte
                            for (colonne = 0; colonne < grille.NbColonnes; colonne++)
                            {
                                int count = 0;
                                ligne = grille.GetLigne(colonne);
                                if (ligne + 1 < grille.NbLignes && grille.Tableau[colonne, ligne + 1] == NumeroJoueur)
                                {
                                    count++;
                                }
                                if (ligne + 2 < grille.NbLignes && grille.Tableau[colonne, ligne + 2] == NumeroJoueur)
                                {
                                    count++;
                                }
                                if (ligne + 3 < grille.NbLignes && grille.Tableau[colonne, ligne + 3] == NumeroJoueur)
                                {
                                    count++;
                                }
                                if (ligne + 4 < grille.NbLignes && grille.Tableau[colonne, ligne + 4] == NumeroJoueur)
                                {
                                    count++;
                                }

                                if (ligne < 0)
                                {
                                    Availables.Add(false);
                                }
                                else
                                {
                                    Availables.Add(true);
                                }

                                MesTrucsParColonnes.Add(count);
                            }

                            // On prends un colonne a coté du plus grand
                            //bool AllerADroiteFirst = Convert.ToBoolean(RandomSeed.Next(0, 2));
                            int MaxCount = 0;
                            foreach (var count in MesTrucsParColonnes)
                            {
                                if (count > MaxCount)
                                {
                                    MaxCount = count;
                                }
                            }
                            for (colonne = 0; colonne < grille.NbColonnes; colonne++)
                            {
                                if (MesTrucsParColonnes[colonne] == MaxCount)
                                {
                                    // Colonne de droite
                                    if (colonne + 1 < grille.NbColonnes && Availables[colonne + 1])
                                    {
                                        ColonnesToEventuallyPlay.Add(colonne + 1);
                                    }
                                    // Colonne de gauche
                                    if (colonne > 0 && Availables[colonne - 1])
                                    {
                                        ColonnesToEventuallyPlay.Add(colonne - 1);
                                    }
                                }
                            }


                            if (ColonnesToEventuallyPlay.Count != 0)
                            {
                                int index = RandomSeed.Next(ColonnesToEventuallyPlay.Count);
                                colonne = ColonnesToEventuallyPlay[index];
                            }
                            else
                            {
                                colonne = RandomSeed.Next(0, grille.Tableau.GetLength(0));
                            }


                            ThinkLevel = 3;
                        }
                        // -------------------


                        // --- MIDDLE PLAY ---
                        else if ((Niveau == 3 && StrategyDice <= 50) || (Niveau == 4 && StrategyDice <= 50) || (Niveau == 5 && StrategyDice > 75 && StrategyDice <= 100))
                        {
                            List<int> colonnesDuMilieu = new List<int>();
                            for (int i = 3; i < grille.NbColonnes - 3; i++)
                            {
                                colonnesDuMilieu.Add(i);
                            }
                            colonne = colonnesDuMilieu[RandomSeed.Next(colonnesDuMilieu.Count)];

                            if (grille.GetLigne(colonne) < 0)
                            {
                                colonne = RandomSeed.Next(0, grille.Tableau.GetLength(0));
                            }

                            ThinkLevel = 2;
                        }
                        // -------------------


                        // --- RANDOM PLAY ---
                        else
                        {
                            colonne = RandomSeed.Next(0, grille.Tableau.GetLength(0));
                            ThinkLevel = -1;
                        }
                        // --------------------
                    }
                }


                ligne = grille.GetLigne(colonne);
                if (ligne >= 0)
                {
                    rester = false;
                }
                else
                {
                    if (timeout > 0)
                    {
                        timeout--;
                    }
                    else
                    {
                        do
                        {
                            colonne = RandomSeed.Next(0, grille.Tableau.GetLength(0));
                            ligne = grille.GetLigne(colonne);
                        } while (ligne < 0);
                        rester = false;
                    }
                }
            } while (rester);


            if (AutreJoueur is JoueurHumain)
            {
                // Animation des trois petits points
                switch (ThinkLevel)
                {
                    case 1:
                        ThinkTime = RandomSeed.Next(1, 2);
                        break;
                    case 2:
                        ThinkTime = RandomSeed.Next(2, 3);
                        break;
                    case 3:
                        ThinkTime = RandomSeed.Next(3, 4);
                        break;
                    case 4:
                        ThinkTime = RandomSeed.Next(4, 5);
                        break;
                    default:
                        ThinkTime = RandomSeed.Next(2, 5);
                        break;
                }
            }
            else
            {
                ThinkTime = 1;
            }


            for (int i = 0; i < ThinkTime; i++)
            {
                Console.Write("\n ");
                for (int j = 0; j < 3; j++)
                {
                    System.Threading.Thread.Sleep(200);
                    Console.Write(" . ");
                }
                System.Threading.Thread.Sleep(200);
                Console.Clear();
                jeu.AfficherTitre(false);
                Console.WriteLine($"{jeu.Joueur1.NomJoueur} : {jeu.Joueur1.Score}, {jeu.Joueur2.NomJoueur} : {jeu.Joueur2.Score} \n");
                grille.Afficher();
            }

            // Animation
            for (int i = 0; i <= ligne; i++)
            {
                Console.Clear();
                jeu.AfficherTitre(false);
                Console.WriteLine($"{jeu.Joueur1.NomJoueur} : {jeu.Joueur1.Score}, {jeu.Joueur2.NomJoueur} : {jeu.Joueur2.Score} \n");
                grille.Positionner(i, colonne, NumeroJoueur);
                grille.Afficher();
                Console.Write(Environment.NewLine);
                System.Threading.Thread.Sleep(20);
                grille.Positionner(i, colonne, 0);
            }

            if (AutreJoueur is JoueurHumain)
            {
                Console.Beep();
            }
            grille.Positionner(ligne, colonne, NumeroJoueur);
            ColonnesToEventuallyPlay.Clear();
        }

        /// <summary>
        /// Permet de savoir si on joueur va gagner au prochain tour
        /// </summary>
        /// <param name="numJoueur">Le numéro du joueur a tester</param>
        /// <param name="plateau">La grille de jeu sur la quelle il faut tester les combinaisons</param>
        /// <returns>True si le joueur en question peut gagner au prochain tour</returns>
        private bool AnalyserJoueur(int numJoueur, Grille plateau)
        {
            // L'idée etant de remplir la liste ColonnesToEventuallyPlay si on trouve des trucs a faire
            ColonnesToEventuallyPlay.Clear();

            // On va jouer dans chaque colonne comme si on etais le joueur (celui qui est analysé) et on regarde
            // le resultat, et ce avec les methodes de la classe Grille

            for (int colonne = 0; colonne < plateau.NbColonnes; colonne++)
            {
                int ligne = plateau.GetLigne(colonne);

                if (ligne >= 0)
                {
                    plateau.Positionner(ligne, colonne, numJoueur);
                    if (plateau.TesterGagner() > 0)
                    {
                        ColonnesToEventuallyPlay.Add(colonne);
                    }
                    plateau.Positionner(ligne, colonne, 0);
                    plateau.ColonneCaseGagnanteDebut = -1;
                    plateau.LigneCaseGagnanteDebut = -1;
                    plateau.VictoryType = -1;
                }
            }

            if (ColonnesToEventuallyPlay.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
