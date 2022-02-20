using System;

namespace TpPuissance4PooCs
{
    public class JoueurHumain : Joueur
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public JoueurHumain(int numeroJoueur, string nomJoueur) : base(numeroJoueur, nomJoueur) { Score = 0; }

        /// <summary>
        /// Permet de faire jouer un joueur humain au puissance 4
        /// </summary>
        /// <param name="grille">Grille dans laquelle la partie se passe</param>
        /// <param name="jeu">Jeu dans lequel la partie se passe</param>
        public override void Jouer(Grille grille, Puissance4 jeu)
        {
            int colonne = 0;
            int ligne = 0;
            bool rester = true;
            do
            {
                Console.Clear();
                jeu.AfficherTitre(false);
                Console.WriteLine($"{jeu.Joueur1.NomJoueur} : {jeu.Joueur1.Score}, {jeu.Joueur2.NomJoueur} : {jeu.Joueur2.Score} \n");
                grille.Afficher();

                // On affiche la fleche en dessous de la colonne séléctionnée
                string ligneBtm = "      ";
                string fleche = "^";
                for (int i = 0; i < grille.NbColonnes; i++)
                {
                    if (i == colonne)
                    {
                        ligneBtm += $"  {fleche} ";
                    }
                    else
                    {
                        ligneBtm += $"    ";
                    }
                }

                Console.WriteLine(ligneBtm);

                // Si le joueur Humain "combat" un autre joueur humain, alors on affiche de qui c'est le tour
                if (this.NumeroJoueur == 1)
                {
                    if (jeu.Joueur2 is JoueurHumain)
                    {
                        Console.WriteLine($"Tour de {this.NomJoueur}");
                    }
                }
                else
                {
                    if (jeu.Joueur1 is JoueurHumain)
                    {
                        Console.WriteLine($"Tour de {this.NomJoueur}");
                    }
                }

                var input = Console.ReadKey();


                // Si une touche est appuyée, alors on regarde si elle correspond a un des couples Touche/Action
                if (input.Key == ConsoleKey.RightArrow)
                {
                    if (colonne < grille.NbColonnes - 1)
                    {
                        colonne++;
                    }
                }
                else if (input.Key == ConsoleKey.LeftArrow)
                {
                    if (colonne > 0)
                    {
                        colonne--;
                    }
                }
                else if (input.Key == ConsoleKey.NumPad1 && grille.Tableau.GetLength(0) >= 1)
                {
                    colonne = 0;
                }
                else if (input.Key == ConsoleKey.NumPad2 && grille.Tableau.GetLength(0) >= 2)
                {
                    colonne = 1;
                }
                else if (input.Key == ConsoleKey.NumPad3 && grille.Tableau.GetLength(0) >= 3)
                {
                    colonne = 2;
                }
                else if (input.Key == ConsoleKey.NumPad4 && grille.Tableau.GetLength(0) >= 4)
                {
                    colonne = 3;
                }
                else if (input.Key == ConsoleKey.NumPad5 && grille.Tableau.GetLength(0) >= 5)
                {
                    colonne = 4;
                }
                else if (input.Key == ConsoleKey.NumPad6 && grille.Tableau.GetLength(0) >= 6)
                {
                    colonne = 5;
                }
                else if (input.Key == ConsoleKey.NumPad7 && grille.Tableau.GetLength(0) >= 7)
                {
                    colonne = 6;
                }
                else if (input.Key == ConsoleKey.NumPad8 && grille.Tableau.GetLength(0) >= 8)
                {
                    colonne = 7;
                }
                else if (input.Key == ConsoleKey.NumPad9 && grille.Tableau.GetLength(0) >= 9)
                {
                    colonne = 8;
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    if (colonne < grille.Tableau.GetLength(0) && colonne >= 0)
                    {
                        ligne = grille.GetLigne(colonne);
                        if (ligne >= 0)
                        {
                            rester = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" | Saisie incorrecte");
                    }
                }

            } while (rester);

            // Animation
            for (int i = 0; i <= ligne; i++)
            {
                Console.Clear();
                jeu.AfficherTitre(false);
                Console.WriteLine($"{jeu.Joueur1.NomJoueur} : {jeu.Joueur1.Score}, {jeu.Joueur2.NomJoueur} : {jeu.Joueur2.Score} \n");
                grille.Positionner(i, colonne, NumeroJoueur);
                grille.Afficher();
                System.Threading.Thread.Sleep(20);
                grille.Positionner(i, colonne, 0);
            }

            // On place le pion
            Console.Beep();
            grille.Positionner(ligne, colonne, NumeroJoueur);
        }
    }
}
