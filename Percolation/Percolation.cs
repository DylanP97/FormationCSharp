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
        private bool _percolate;

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

        public bool Percolate()
        {
            _percolate = false;

            // Initialize the _full array for each cell in the top row
            for (int j = 0; j < _size; j++)
            {
                if (_open[0, j])
                {
                    DepthFirstSearch(0, j);
                }
            }

            return _percolate;
        }

        private void DepthFirstSearch(int i, int j)
        {
            if (!_percolate && !_full[i, j])
            {
                _full[i, j] = true;

                if (i == _size - 1)
                {
                    // If we reach the bottom row, set _percolate to true
                    _percolate = true;
                }
                else
                {
                    // Explore neighbors
                    List<KeyValuePair<int, int>> neighbors = CloseNeighbors(i, j);
                    foreach (var neighbor in neighbors)
                    {
                        DepthFirstSearch(neighbor.Key, neighbor.Value);
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

            // Open the specified box
            _open[i, j] = true;

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
            if (i < 0 || i >= _size || j < 0 || j >= _size)
            {
                throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid row index.");
            }
        }
    }
}
