using System;

namespace TpPuissance4PooCs
{
    public class Puissance4 : Jeu
    {
        private Joueur _joueur1;
        private Joueur _joueur2;
        private Grille _plateau;

        public Puissance4(Joueur joueur1, Joueur joueur2, Grille grille)
        {
            this._nom = "Puissance 4";
            this._titre = "-------------------------------------------" + "\n" + " P U I S S A N C E   IV : T H E   G A M E" + "\n" + "-------------------------------------------";
            this.Joueur1 = joueur1;
            this.Joueur2 = joueur2;
            this.Plateau = grille;
        }

        // Accesseurs
        public string Nom { get => this._nom;/* set => this.nom = value;*/ }
        public Joueur Joueur1 { get => _joueur1; set => _joueur1 = value; }
        public Joueur Joueur2 { get => _joueur2; set => _joueur2 = value; }
        public Grille Plateau { get => _plateau; set => _plateau = value; }

        /// <summary>
        /// Permet de démarrer le jeu
        /// </summary>
        public void Demarrer()
        {
            this.AfficherTitre(true);
            bool Rejouer = false;
            bool FinParVictoire = true;
            do
            {
                //Plateau.Positionner(5, 0, 2);
                //Plateau.Positionner(4, 3, 1);
                //Plateau.Positionner(3, 3, 2);
                //Plateau.Positionner(2, 3, 1);
                //Plateau.Positionner(1, 3, 2);
                //Plateau.Positionner(0, 3, 1);
                //Plateau.NbTour = 6;

                Console.Clear();
                this.AfficherTitre(false);
                Console.WriteLine($"{this.Joueur1.NomJoueur} : {this.Joueur1.Score}, {this.Joueur2.NomJoueur} : {this.Joueur2.Score}\n");

                int gagnant = 0;
                bool victoire = false;
                this.Plateau.NbTour = 0;
                this.Plateau.LigneCaseGagnanteDebut = -1;
                this.Plateau.ColonneCaseGagnanteDebut = -1;
                

                // On tire au hasard le joueur qui commence :
                Random randomSeed = new Random();
                bool TourDuJoueur1 = Convert.ToBoolean(randomSeed.Next(0, 2));
                
                do
                {
                    if (Plateau.YouCanPlay())
                    {
                        Console.Clear();
                        this.AfficherTitre(false);
                        Console.WriteLine($"{this.Joueur1.NomJoueur} : {this.Joueur1.Score}, {this.Joueur2.NomJoueur} : {this.Joueur2.Score}\n");
                        Plateau.Afficher();
                        if (TourDuJoueur1)
                        {
                            Joueur1.Jouer(Plateau, this);
                            TourDuJoueur1 = false;
                        }
                        else
                        {
                            Joueur2.Jouer(Plateau, this);
                            TourDuJoueur1 = true;
                        }
                        Plateau.IncrementNbTours();

                        gagnant = Plateau.TesterGagner();
                        if (gagnant > 0)
                        {
                            victoire = true;
                        }
                    }
                    else
                    {
                        victoire = true;
                        FinParVictoire = false;
                    }
                } while (!victoire);
                Console.Clear();

                string resultat;

                if (FinParVictoire)
                {
                    var victoryType = Plateau.VictoryType;

                    // Boucle d'animation (3 est ici le nombre de clognotement)
                    for (int i = 0; i < 3; i++)
                    {
                        Plateau.VictoryType = -1;
                        Console.Clear();
                        this.AfficherTitre(false);
                        Console.WriteLine($"{this.Joueur1.NomJoueur} : {this.Joueur1.Score}, {this.Joueur2.NomJoueur} : {this.Joueur2.Score}\n");
                        Plateau.Afficher();
                        System.Threading.Thread.Sleep(10);
                        Plateau.VictoryType = victoryType;
                        Console.Clear();
                        this.AfficherTitre(false);
                        Console.WriteLine($"{this.Joueur1.NomJoueur} : {this.Joueur1.Score}, {this.Joueur2.NomJoueur} : {this.Joueur2.Score}\n");
                        Plateau.Afficher();
                        Console.Beep();
                        System.Threading.Thread.Sleep(10);
                    }

                    resultat = " |  ";
                    switch (gagnant)
                    {
                        case 1:
                            resultat += "Victoire";
                            this.Joueur1.Score++;
                            break;
                        case 2:
                            resultat += "Defaite";
                            this.Joueur2.Score++;
                            break;
                        default:
                            resultat += "Victoire d'un joueur non-déclaré"; break;
                    }
                    switch (Plateau.VictoryType)
                    {
                        case 1:
                            resultat += $" en ({Plateau.ColonneCaseGagnanteDebut + 1},{Plateau.LigneCaseGagnanteDebut + 1}) de type {Plateau.VictoryType} (Horizontal)"; break;
                        case 2:
                            resultat += $" en ({Plateau.ColonneCaseGagnanteDebut + 1},{Plateau.LigneCaseGagnanteDebut + 1}) de type {Plateau.VictoryType} (Vertical)"; break;
                        case 3:
                            resultat += $" en ({Plateau.ColonneCaseGagnanteDebut + 1},{Plateau.LigneCaseGagnanteDebut + 1}) de type {Plateau.VictoryType} (Diagonal de haut-gauche a bas-droite)"; break;
                        case 4:
                            resultat += $" en ({Plateau.ColonneCaseGagnanteDebut + 1},{Plateau.LigneCaseGagnanteDebut + 1}) de type {Plateau.VictoryType} (Diagonal de haut-droite a bas-gauche)"; break;
                        default: break;
                    }
                    resultat += "  |";
                }
                else
                {
                    this.AfficherTitre(false);
                    Console.WriteLine($"{this.Joueur1.NomJoueur} : {this.Joueur1.Score}, {this.Joueur2.NomJoueur} : {this.Joueur2.Score}\n");
                    Plateau.Afficher();
                    resultat = " |  Egalite  |";
                }

                

                

                // Affichage du resultat de la partie
                Console.Write(Environment.NewLine);

                string ligne = " ";
                for (int i = 0; i < resultat.Length - 1; i++)
                {
                    ligne += "-";
                }

                Console.WriteLine(ligne);
                Console.WriteLine(resultat);
                Console.WriteLine(ligne);


                // --- ZONE REJOUER ---
                Console.Write(Environment.NewLine);
                Console.Write("Voulez vous rejouer ? (y/n)");

                bool InputIsValid = false;
                string Input = string.Empty;
                while (!InputIsValid)
                {
                    if (this.Joueur1 is JoueurHumain || this.Joueur2 is JoueurHumain)
                    {
                        Input = Console.ReadLine();
                    }
                    else
                    {
                        Input = "y";
                    }
                    switch (Input)
                    {
                        case "y":
                        case "Y":
                            Rejouer = true;
                            InputIsValid = true;
                            break;
                        case "n":
                        case "N":
                            Rejouer = false;
                            InputIsValid = true;
                            break;
                        default:
                            Console.Write("Erreur, veuillez rentrer \"y\" ou \"n\" : ");
                            break;
                    }
                }

                Plateau.init();

            } while (Rejouer);
        }

        public override void AfficherTitre(bool Animation)
        {
            if (Animation)
            {
                string ligne = "------------------------------------------";
                string ligneFifty = string.Empty;
                for (int i = 0; i < ligne.Length/4; i++)
                {
                    ligneFifty += "----";
                    Console.WriteLine(ligneFifty);
                    Console.Write(" P U I S S A N C E   IV : T H E   ");
                    if (i >= ligne.Length / (4 * 5))
                    {
                        Console.Write("G ");
                    }
                    if (i >= (ligne.Length * 2) / (4 * 5))
                    {
                        Console.Write("A ");
                    }
                    if (i >= (ligne.Length * 3) / (4 * 5))
                    {
                        Console.Write("M ");
                    }
                    if (i >= (ligne.Length * 2) / (4 * 5))
                    {
                        Console.Write("E ");
                    }
                    Console.Write(Environment.NewLine);
                    Console.WriteLine(ligneFifty);
                    System.Threading.Thread.Sleep(20);
                    Console.Clear();
                }
                base.AfficherTitre(Animation);
            }
            else
            {
                base.AfficherTitre(Animation);
            }
        }
    }
}
