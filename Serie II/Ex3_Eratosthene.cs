using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class Eratosthene
    {
        public static int[] EratosthenesSieve(int n)
        {
            // Initialiser un tableau de booléens pour marquer les nombres comme premiers ou non premiers
            bool[] isPrime = new bool[n + 1];
            for (int i = 2; i <= n; i++)
            {
                isPrime[i] = true;
            }

            // Appliquer le crible d'Eratosthène
            for (int i = 2; i * i <= n; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= n; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            // Compter le nombre de nombres premiers
            int count = 0;
            for (int i = 2; i <= n; i++)
            {
                if (isPrime[i]) count++;
            }

            // Stocker les nombres premiers dans un tableau
            int[] primes = new int[count];
            int index = 0;
            for (int i = 2; i <= n; i++)
            {
                if (isPrime[i])
                {
                    primes[index] = i;
                    index++;
                }
            }

            return primes;
        }
    }
}
