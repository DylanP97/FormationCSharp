using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class Search
    {
        public static int LinearSearch(int[] tableau, int valeur)
        {
            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] == valeur)
                {
                    return i; // Found the value, return its index
                }
            }
            return -1; // Value not found in the array
        }

        public static int BinarySearch(int[] tableau, int valeur)
        {
            // Avant d'appliquer la recherche binaire, triez le tableau
            Array.Sort(tableau);
            int gauche = 0;
            int droite = tableau.Length;

            while (gauche <= droite)
            {
                int milieu = gauche + (droite - gauche) / 2; // Utilisation de la division entière

                if (tableau[milieu] == valeur)
                {
                    return milieu; // La valeur a été trouvée, retourne l'index
                }
                else if (tableau[milieu] < valeur)
                {
                    gauche = milieu + 1; // La valeur est dans la moitié droite
                }
                else
                {
                    droite = milieu - 1; // La valeur est dans la moitié gauche
                }
            }

            return -1;
        }
    }
}
