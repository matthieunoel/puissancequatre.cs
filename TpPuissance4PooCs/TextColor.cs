using System;

namespace TpPuissance4PooCs
{
    public class TextColor
    {
        /// <summary>
        /// Permet d'afficher du texte en couleur
        /// </summary>
        /// <param name="message">Message a afficher</param>
        /// <param name="foreground">Couleur du texte</param>
        /// <param name="background">Couleur du fond</param>
        /// <param name="NewLine">Si on veut ajouter un saut a la ligne ou non</param>
        public static void PrintWithColor(string message, ConsoleColor foreground, ConsoleColor background, bool NewLine)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            if (NewLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Permet de changer la couleur du texte
        /// </summary>
        /// <param name="foreground">Couleur demandée</param>
        public static void SetForeground(ConsoleColor foreground) => Console.ForegroundColor = foreground;

        /// <summary>
        /// Permet de changer la couleur du fond
        /// </summary>
        /// <param name="background">Couleur demandée</param>
        public static void SetBackGround(ConsoleColor background) => Console.BackgroundColor = background;

        /// <summary>
        /// Permet de mettre le fond sur noir et le texte sur blanc
        /// </summary>
        public static void ResetColor() => Console.ResetColor();

        /// <summary>
        /// Affichage des couleures disponibles
        /// </summary>
        public static void PrintDemo()
        { 
            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                Console.ForegroundColor = color;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"Foreground color set to {color}");
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine($"Foreground color set to {color}");
                Console.Write(Environment.NewLine);
            }
            Console.ResetColor();
            Console.Write(Environment.NewLine);
            Console.WriteLine("=====================================");
            Console.Write(Environment.NewLine);
            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                Console.BackgroundColor = color;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Background color set to {color}");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"Background color set to {color}");
                Console.Write(Environment.NewLine);
            }
            Console.ResetColor();
        }
    }
}
