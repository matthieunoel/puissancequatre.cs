using System;

namespace TpPuissance4PooCs
{
    public class Jeu
    {
        protected string _nom;
        protected string _titre;

        /// <summary>
        /// Permet d'afficher le titre
        /// </summary>
        /// <param name="Animation">Permet de déclencher ou non l'animation du titre</param>
        public virtual void AfficherTitre(bool Animation)
        {
            Console.WriteLine(this._titre);
            Console.Write(Environment.NewLine);
        }

    }
}
