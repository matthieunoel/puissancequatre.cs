using System;

namespace TpPuissance4PooCs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TextColor.PrintDemo();
                Console.WriteLine("\nMettre un point d'arret ici ...");

                Top:

                Console.Clear();

                Console.WriteLine("P = Player, IA2 = IA de niveau 2, v = versus\n");
                Console.WriteLine("Mode de jeu 1 : PvP");
                Console.WriteLine("Mode de jeu 2 : PvIA1");
                Console.WriteLine("Mode de jeu 3 : PvIA2");
                Console.WriteLine("Mode de jeu 4 : PvIA3");
                Console.WriteLine("Mode de jeu 5 : PvIA4");
                Console.WriteLine("Mode de jeu 6 : IA3vIA4");
                Console.WriteLine("Mode de jeu 7 : PvIA-1 (L'IA demandée par Mr.Chevalier)");
                Console.WriteLine("Mode de jeu 8 : IA4vIA-1");
                Console.WriteLine("Mode de jeu 9 : PvIA3 [9x7]");
                Console.Write(Environment.NewLine);
                int input = 0;

                bool rester = true;
                do
                {
                    Console.Write("Veuillez choisir un mode de jeu (0 = manuel d'instruction/README) : ");

                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input < 0 || input > 9)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            rester = false;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Saisie invalide.");
                    }
                } while (rester);

                Joueur joueur1 = new Joueur();
                Joueur joueur2 = new Joueur();
                Grille plateau = new Grille(6, 7);

                switch (input)
                {
                    case 1:
                        joueur1 = new JoueurHumain(1, "Player 1 [X]");
                        joueur2 = new JoueurHumain(2, "Player 2 [O]");
                        break;
                    case 2:
                        joueur1 = new JoueurHumain(1, "Player 1 [X]");
                        joueur2 = new JoueurIA(2, "Player 2 (AI1) [O]", 1);
                        break;
                    case 3:
                        joueur1 = new JoueurHumain(1, "Player 1 [X]");
                        joueur2 = new JoueurIA(2, "Player 2 (AI2) [O]", 2);
                        break;
                    case 4:
                        joueur1 = new JoueurHumain(1, "Player 1 [X]");
                        joueur2 = new JoueurIA(2, "Player 2 (AI3) [O]", 3);
                        break;
                    case 5:
                        joueur1 = new JoueurHumain(1, "Player 1 [X]");
                        joueur2 = new JoueurIA(2, "Player 2 (AI4) [O]", 4);
                        break;
                    case 6:
                        joueur1 = new JoueurIA(1, "Player 1 (AI3) [X]", 3);
                        joueur2 = new JoueurIA(2, "Player 2 (AI4) [O]", 4);
                        break;
                    case 7:
                        joueur1 = new JoueurHumain(1, "Player 1 [X]");
                        joueur2 = new JoueurIA(2, "Player 2 (AI-1) [O]", -1);
                        break;
                    case 8:
                        joueur1 = new JoueurIA(1, "Player 1 (AI4) [X]", 4);
                        joueur2 = new JoueurIA(2, "Player 2 (AI-1) [O]", -1);
                        break;
                    case 9:
                        joueur1 = new JoueurHumain(1, "Player 1 [X]");
                        joueur2 = new JoueurIA(2, "Player 2 (AI3) [O]", 3);
                        plateau = new Grille(7, 9);
                        break;
                    case 0:
                        PrintRules();
                        Console.ReadKey();
                        goto Top;
                        //break;
                }

                Puissance4 Jeu = new Puissance4(joueur1, joueur2, plateau);
                Jeu.Demarrer();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(Environment.NewLine + $"/!\\ - Erreur fatale ({ex.Data}) : {ex.Message} - /!\\");
                TextColor.PrintWithColor(Environment.NewLine + $"/!\\ - Erreur fatale ({ex.Data}) : {ex.Message} - /!\\", ConsoleColor.Black, ConsoleColor.Red, true);
                Console.ReadKey();
            }
            //Console.ReadKey();
        }

        static void PrintRules()
        {
            //Console.Write(Environment.NewLine);
            Console.Clear();

            Console.WriteLine("------------------------- REGLES DU JEU -------------------------\n");

            Console.WriteLine("Bonjour et bienvenue dans le jeu du puissance 4.\n");
            Console.WriteLine("Avant de commencer, sachez que le jeu en lui meme n'est pas totalement finis. Et il faudrais l'optimiser\n");
            Console.WriteLine("Les regles, vous les connaissez, je vais skip, (...)");
            Console.WriteLine("Pour placer un pion, vous pouvez utiliser les fleches pour déplacer le curseur a droite et a gauche.");
            Console.WriteLine("Vous pouvez aussi utiliser le pavé numérique pour aller directement a la bonne case.");
            Console.WriteLine("");
            Console.WriteLine("Pour les developpeurs, ou les profs qui vont regarder le code ;) ,");
            Console.WriteLine("sachez que la grille et tout ce qui va avec a une taille dynamique jusqu'a 9");
            Console.WriteLine("L'intelligence artificielle a été améliorée par rapport a celle proposée originallement.");
            Console.WriteLine("Le jeu s'adapte en fonction des joueurs, s'ils sont tout les deux humains, si on retrouve\n" +
                "un humain et une IA ou deux IA");
            Console.WriteLine("");
            Console.WriteLine("Voici la description des niveaux des IA :");
            Console.WriteLine(" -1 - Niveau de l'IA originale");
            Console.WriteLine("  1 - [1-100 Hasard]");
            Console.WriteLine("  2 - [1-50 WinOuContre] sinon [100 Hasard]");
            Console.WriteLine("  3 - [1-80 WinOuContre] sinon [1-50 Jeu au milieu / 51-100 Hasard]");
            Console.WriteLine("  4 - [1-100 WinOuContre] sinon [1-50 Jeu au milieu / 51-100 Jeu vers la moyenne]");
            Console.WriteLine("  5 - [1-100 WinOuContre] sinon [1-50 Prévision sur deux coups / 51-75 Jeu vers la moyenne / 76-100 Jeu au milieu]");
            Console.WriteLine("  6 - [1-100 WinOuContre] sinon [1-100 Prévision sur deux coups / {Dosage parfait des différentes techniques}]");
            Console.WriteLine("");
            Console.WriteLine("Sur ce, bon jeu, essayez de battre mon IA de niveau 4, c'est possible mais pas non plus des plus facile ;)");

            Console.Write(Environment.NewLine);
        }
    }
}
