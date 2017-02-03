using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Puissance4_GUI;


namespace BrunetHugo_NounahonJeanMickael
{
    class Program
    {
        /*
         * Test de la validité du type du caractère saisi par l'utilisateur
         */
        static int TestValiditeDuTypeDeLaSaisie()
        {
            int r;
            while (!int.TryParse(Console.ReadLine(), out r))
            {
                Console.WriteLine("Veuillez saisir un entier!");
            }
            return r;
        }//TestValiditeDuTypeDeLaSaisie()

        /*
         * L'utilisateur choisit combien de pion les joueurs doivent aligner pour gagner
         */
        static int NombreCoupsPourGagner()
        {
            int nombre = 0;
            do
            {
                Console.Write("Veuillez saisir le nombre de pion à aligner pour gagner: \n(nombre >= 3) : ");
                nombre = TestValiditeDuTypeDeLaSaisie();
            } while (nombre < 3);
            return nombre;
        }//NombreCoupsPourGagner()

        /*
         * Création de la grille en fonction du nombre de pion à aligner pour gagner
         */
        static int[,] CreationGrille(int nbCoupsVictoire)
        {
            Console.WriteLine("Création de la grille:");
            Console.WriteLine();
            int ligne = 0;
            int colonne = 0;
            do
            {
                Console.Write("Veuillez saisir le nombre de ligne \n(nombre >=" + nbCoupsVictoire + ") : ");
                ligne = TestValiditeDuTypeDeLaSaisie();

                Console.WriteLine();

                Console.Write("Veuillez saisir le nombre de colonne \n(nombre >= " + nbCoupsVictoire + "): ");
                colonne = TestValiditeDuTypeDeLaSaisie();

                Console.WriteLine();
            } while (ligne < nbCoupsVictoire || colonne < nbCoupsVictoire);

            int[,] grille = new int[ligne, colonne];
            return grille;
        }//CreationGrille()


        static bool TestHorizontales(int[,] grille, int ligne, int colonne, int noJoueur, int nbCoupsPourGagner)
        {
            bool test = false;
            int compteur = 0;
            for (int i = 0; i < nbCoupsPourGagner; ++i)
            {
                if (grille[ligne, colonne] == noJoueur && grille[ligne, colonne + i] == noJoueur)
                {
                    ++compteur;
                }
            }
            if (compteur == nbCoupsPourGagner)
            {
                test = true;
            }
            return test;
        }//TestHorizontales()

        static bool TestVertical(int[,] grille, int ligne, int colonne, int noJoueur, int nbCoupsPourGagner)
        {
            bool test = false;
            int compteur = 0;
            for (int i = 0; i < nbCoupsPourGagner; ++i)
            {
                if (grille[ligne, colonne] == noJoueur && grille[ligne - i, colonne] == noJoueur)
                {
                    ++compteur;
                }
            }
            if (compteur == nbCoupsPourGagner)
            {
                test = true;
            }
            return test;
        }//TestVertical()

        static bool TestDiagoGaucheDroite(int[,] grille, int ligne, int colonne, int noJoueur, int nbCoupsPourGagner)
        {
            bool test = false;
            int compteur = 0;
            for (int i = 0; i < nbCoupsPourGagner; ++i)
            {
                if (grille[ligne, colonne] == noJoueur && grille[ligne - i, colonne + i] == noJoueur)
                {
                    ++compteur;
                }
            }
            if (compteur == nbCoupsPourGagner)
            {
                test = true;
            }
            return test;
        }//TestDiagoGaucheDroite()

        static bool TestDiagoDroiteGauche(int[,] grille, int ligne, int colonne, int noJoueur, int nbCoupsPourGagner)
        {
            bool test = false;
            int compteur = 0;
            for (int i = 0; i < nbCoupsPourGagner; ++i)
            {
                if (grille[ligne, colonne] == noJoueur && grille[ligne - i, colonne - i] == noJoueur)
                {
                    ++compteur;
                }
            }
            if (compteur == nbCoupsPourGagner)
            {
                test = true;
            }
            return test;
        }//TestDiagoDroiteGauche()

        static bool TestNumColonneValide(int colonne, int[,] grille)
        {
            bool test = false;
            if (colonne > 0 && colonne <= grille.GetLength(1) && !ColonnePleine(grille, colonne))
            {
                test = true;
            }
            return test;
        }//TestNumColonneValide()

        static int SaisirUnNumColonneValide(int[,] grille)
        {
            int numColonne = -1;
            do
            {
                Console.Write("Veuillez saisir une colonne valide: ");
                numColonne = TestValiditeDuTypeDeLaSaisie();
                Console.WriteLine();
            } while (!TestNumColonneValide(numColonne, grille));
            return numColonne;
        }//SaisirUnNumColonneValide()

        static bool ColonnePleine(int[,] grille, int colonne)
        {
            bool colonnePleine = false;

            if (grille[0, colonne - 1] == 1 || grille[0, colonne - 1] == 2)
            {
                colonnePleine = true;
            }
            return colonnePleine;
        }//SaisirUnNumColonneValide()

        static bool VictoireHorizontale(int[,] grille, int numJoueur, int nbCoupsVictoire)
        {
            bool victoireHorizontale = false;
            for (int i = grille.GetLength(0) - 1; i >= 0; --i)
            {
                for (int j = 0; j < grille.GetLength(1) - (nbCoupsVictoire - 1); ++j)
                {
                    bool testHorizontal = TestHorizontales(grille, i, j, numJoueur, nbCoupsVictoire);
                    if (testHorizontal)
                    {
                        victoireHorizontale = true;
                    }
                }
            }
            return victoireHorizontale;
        }//VictoireHorizontale()

        static bool VictoireDiagonaleGaucheDroite(int[,] grille, int numJoueur, int nbCoupsVictoire)
        {
            bool victoireDiagonaleGaucheDroite = false;
            for (int i = grille.GetLength(0) - 1; i >= (nbCoupsVictoire - 1); --i)
            {
                for (int j = 0; j < grille.GetLength(1) - (nbCoupsVictoire - 1); ++j)
                {
                    bool testDiagoGaucheDroite = TestDiagoGaucheDroite(grille, i, j, numJoueur, nbCoupsVictoire);
                    if (testDiagoGaucheDroite)
                    {
                        victoireDiagonaleGaucheDroite = true;
                    }
                }
            }
            return victoireDiagonaleGaucheDroite;
        }//VictoireDiagonaleGaucheDroite()

        static bool VictoireDiagonaleDroiteGauche(int[,] grille, int numJoueur, int nbCoupsVictoire)
        {
            bool victoireDiagonaleDroiteGauche = false;
            for (int i = grille.GetLength(0) - 1; i >= (nbCoupsVictoire - 1); --i)
            {
                for (int j = grille.GetLength(1) - 1; j >= (nbCoupsVictoire - 1); --j)
                {
                    bool testDiagoDroiteGauche = TestDiagoDroiteGauche(grille, i, j, numJoueur, nbCoupsVictoire);
                    if (testDiagoDroiteGauche)
                    {
                        victoireDiagonaleDroiteGauche = true;
                    }
                }
            }
            return victoireDiagonaleDroiteGauche;
        }//VictoireDiagonaleDroiteGauche()

        static bool VictoireVerticale(int[,] grille, int numJoueur, int nbCoupsVictoire)
        {
            bool victoireVerticale = false;
            for (int i = grille.GetLength(0) - 1; i >= (nbCoupsVictoire - 1); --i)
            {
                for (int j = 0; j < grille.GetLength(1); ++j)
                {
                    bool testVertical = TestVertical(grille, i, j, numJoueur, nbCoupsVictoire);
                    if (testVertical)
                    {
                        victoireVerticale = true;
                    }
                }
            }
            return victoireVerticale;
        }//VictoireVerticale()


        /*
        * Fonction qui renvoie un booleen. 
        * On parcours chaque colonne de la plus haute ligne de la grille pour savoir
        * si cette colonne est vide ou non afin de savoir si la grille est pleine ou non.  
        */
        static bool Egalite(int[,] grille)
        {
            bool egalite = true;
            int ligne = 0;
            for (int colonne = 0; colonne < grille.GetLength(1); ++colonne)
            {
                if (grille[ligne, colonne] != 1 && grille[ligne, colonne] != 2)
                {
                    egalite = false;
                }
            }
            return egalite;
        }//Egalite()

        static bool Victoire(int[,] grille, int numJoueur, int nbCoupsVictoire)
        {
            bool partieTerminee = false;

            if (VictoireHorizontale(grille, numJoueur, nbCoupsVictoire) ||
                VictoireVerticale(grille, numJoueur, nbCoupsVictoire) ||
                VictoireDiagonaleDroiteGauche(grille, numJoueur, nbCoupsVictoire) ||
                VictoireDiagonaleGaucheDroite(grille, numJoueur, nbCoupsVictoire))
            {
                partieTerminee = true;
            }
            return partieTerminee;
        }//Victoire()

        static bool PositionValide(int[,] grille, int ligne, int colonne)
        {
            bool test = false;
            if (ligne >= 0 && ligne <= grille.GetLength(0) &&
                colonne >= 0 && colonne <= grille.GetLength(1) &&
                (grille[ligne, colonne] != 1 && grille[ligne, colonne] != 2))
            {
                test = true;
            }
            return test;
        }//PositionValide()

        static bool ModifierGrille(int[,] grille, int colonne, int noJoueur)
        {
            int num = 0;
            if (noJoueur == 1)
            {
                num = 1;
            }
            else
            {
                num = 2;
            }
            bool modificationReussie = false;
            int i = grille.GetLength(0) - 1;

            while (i >= 0 && !modificationReussie)
            {
                if (PositionValide(grille, i, colonne))
                {
                    grille[i, colonne] = num;
                    modificationReussie = true;
                }
                --i;
            }
            return modificationReussie;
        }//ModifierGrille()

        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            int nbCoupsVictoire = NombreCoupsPourGagner();
            Console.Clear();
            int[,] grille = CreationGrille(nbCoupsVictoire);
            Console.Clear();

            Fenetre gui = new Fenetre(grille);

            int noJoueur = 2;
            int colonne = 0;

            bool victoire = false;
            bool egalite = false;

            while (!Victoire(grille, noJoueur, nbCoupsVictoire) && !Egalite(grille))
            {
                noJoueur = (noJoueur % 2) + 1;
                Console.WriteLine("C'est au tour du joueur " + noJoueur);

                colonne = SaisirUnNumColonneValide(grille);
                ModifierGrille(grille, colonne - 1, noJoueur);

                victoire = Victoire(grille, noJoueur, nbCoupsVictoire);
                egalite = Egalite(grille);

                gui.rafraichirGrille();
                Console.WriteLine();
            }
            if (victoire)
            {
                Console.WriteLine("Le joueur " + noJoueur + " a gagné!");
            }
            else if (egalite)
            {
                Console.WriteLine("Egalité!");
            }
            Console.ReadKey();
        }//Main()
    }//Programme
}//namespace()

