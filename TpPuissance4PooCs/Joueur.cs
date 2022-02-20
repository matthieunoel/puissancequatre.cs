using System;

namespace TpPuissance4PooCs
{
    public class Joueur
    {
        protected int _numeroJoueur = 0;
        protected string _nomJoueur = string.Empty;
        protected int _score = 0;

        public int NumeroJoueur { get => _numeroJoueur; set => _numeroJoueur = value; }
        public string NomJoueur { get => _nomJoueur; set => _nomJoueur = value; }
        public int Score { get => _score; set => _score = value; }

        /// <summary>
        /// Constrcteur vide
        /// </summary>
        public Joueur()
        {
        }

        /// <summary>
        /// Constrcteur basique
        /// </summary>
        /// <param name="numeroJoueur">Numéro assigné au joueur (1 ou 2. Un autre numéro entrainerais des bugs)</param>
        /// <param name="nomJoueur">Nom affiché pour le joueur</param>
        public Joueur(int numeroJoueur, string nomJoueur)
        {
            NumeroJoueur = numeroJoueur;
            NomJoueur = nomJoueur ?? throw new ArgumentNullException(nameof(nomJoueur));
            Score = 0;
        }

        /// <summary>
        /// Fonction de base jouer dans un jeu de classe Puissance4. Sachant que le joueur ne joue a rien là, ce qui est interessant c'est ce qui est fait plus bas dans les heritages
        /// </summary>
        /// <param name="grille">grille dans laquelle le joueur joue</param>
        /// <param name="jeu">jeu dans lequel le joueur joue</param>
        public virtual void Jouer(Grille grille, Puissance4 jeu)
        {

        }
    }
}
