using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    public class Percolation
    {
        private readonly bool[,] _open;
        private readonly bool[,] _full;
        private readonly int _size;
        public bool _percolate;

        public Percolation(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, "Taille de la grille négative ou nulle.");
            }

            _open = new bool[size, size];
            _full = new bool[size, size];
            _size = size;
        }

        public bool IsOpen(int i, int j)
        {
            ValidateIndices(i, j);
            return _open[i, j];
        }

        private bool IsFull(int i, int j)
        {
            ValidateIndices(i, j);
            return _full[i, j];
        }

        private void DepthFirstSearch(int i, int j)
        {
            if (!_percolate && !_full[i, j] && _open[i, j])
            {
                _full[i, j] = true;

                // Log when a case becomes full during DFS
                //Console.WriteLine($"Case ({i}, {j}) became full during DFS.");

                if (i == _size - 1)
                {
                    // If we reach the bottom row, set _percolate to true
                    _percolate = true;
                    Console.WriteLine("Percolation completed!");
                }
                else
                {
                    // Explore neighbors
                    List<KeyValuePair<int, int>> neighbors = CloseNeighbors(i, j);
                    foreach (var neighbor in neighbors)
                    {
                        // Check if the neighbors are open
                        if (_open[neighbor.Key, neighbor.Value])
                        {
                            DepthFirstSearch(neighbor.Key, neighbor.Value);
                        }
                    }
                }
            }
        }

        private List<KeyValuePair<int, int>> CloseNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> neighbors = new List<KeyValuePair<int, int>>();

            // Check above neighbor
            if (IsValidNeighbor(i - 1, j))
            {
                neighbors.Add(new KeyValuePair<int, int>(i - 1, j));
            }

            // Check below neighbor
            if (IsValidNeighbor(i + 1, j))
            {
                neighbors.Add(new KeyValuePair<int, int>(i + 1, j));
            }

            // Check left neighbor
            if (IsValidNeighbor(i, j - 1))
            {
                neighbors.Add(new KeyValuePair<int, int>(i, j - 1));
            }

            // Check right neighbor
            if (IsValidNeighbor(i, j + 1))
            {
                neighbors.Add(new KeyValuePair<int, int>(i, j + 1));
            }

            return neighbors;
        }

        private bool IsValidNeighbor(int i, int j)
        {
            return i >= 0 && i < _size && j >= 0 && j < _size;
        }


        public void Open(int i, int j)
        {
            ValidateIndices(i, j);

            _open[i, j] = true;

            //Console.WriteLine($"Opened case: ({i}, {j})");

            // Check if the opened box is in the top row
            if (i == 0)
            {
                // If so, perform DFS to update the _full array
                DepthFirstSearch(i, j);
            }
            else
            {
                // Check if any neighbors in the top row are full
                List<KeyValuePair<int, int>> topRowNeighbors = CloseNeighbors(i, j).Where(n => n.Key == 0).ToList();

                foreach (var neighbor in topRowNeighbors)
                {
                    if (_full[neighbor.Key, neighbor.Value])
                    {
                        // If a full square is next to the opened box, mark it as full
                        _full[i, j] = true;

                        // Log when a case becomes full
                        //Console.WriteLine($"Case ({i}, {j}) became full.");
                        break;
                    }
                }

                // Check if the opened box is full
                if (_full[i, j])
                {
                    // If so, update the full status of its open neighbors
                    List<KeyValuePair<int, int>> openNeighbors = CloseNeighbors(i, j).Where(n => _open[n.Key, n.Value]).ToList();
                    foreach (var neighbor in openNeighbors)
                    {
                        _full[neighbor.Key, neighbor.Value] = true;
                    }
                }
            }
        }

        private void ValidateIndices(int i, int j)
        {
            if (i < 0 || i >= _size)
            {
                throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid row index.");
            }
            else if (j < 0 || j >= _size)
            {
                throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid column index.");
            }
        }

        public double PercolationValue()
        {
            int openSites = 0;

            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    if (_open[row, col])
                    {
                        openSites++;
                    }
                }
            }

            return (double)openSites / (_size * _size);
        }
    }
}
