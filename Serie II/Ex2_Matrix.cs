using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class Matrix
    {
        public static int[][] BuildingMatrix(int[] leftVector, int[] rightVector)
        {
            int rows = leftVector.Length;
            int cols = rightVector.Length;

            // Create a 2D array to store the matrix
            int[][] matrix = new int[rows][];

            // Initialize the rows of the matrix
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new int[cols];

                // Multiply each element of the left vector with each element of the right vector
                for (int j = 0; j < cols; j++)
                {
                    matrix[i][j] = leftVector[i] * rightVector[j];
                }
            }

            return matrix;
        }

        public static int[][] Addition(int[][] leftMatrix, int[][] rightMatrix)
        {
            int rows = 3;
            int cols = 2;
            int[][] matrix = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new int[rows];

                for (int j = 0; j < cols; j++)
                {
                    matrix[i][j] = leftMatrix[i][j] + rightMatrix[i][j];
                }
            }
            return matrix;
        }

        public static int[][] Substraction(int[][] leftMatrix, int[][] rightMatrix)
        {
            int rows = 3;
            int cols = 2;
            int[][] matrix = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new int[rows];

                for (int j = 0; j < cols; j++)
                {
                    matrix[i][j] = leftMatrix[i][j] - rightMatrix[i][j];
                }
            }
            return matrix;
        }

        public static int[][] Multiplication(int[][] leftMatrix, int[][] rightMatrix)
        {
            int leftRows = leftMatrix.Length;
            int leftCols = leftMatrix[0].Length;
            int rightCols = rightMatrix[0].Length;

            int[][] resultMatrix = new int[leftRows][];

            for (int i = 0; i < leftRows; i++)
            {
                resultMatrix[i] = new int[rightCols];
                for (int j = 0; j < rightCols; j++)
                {
                    resultMatrix[i][j] = 0;
                    for (int k = 0; k < leftCols; k++)
                    {
                        resultMatrix[i][j] += leftMatrix[i][k] * rightMatrix[k][j];
                    }
                }
            }

            return resultMatrix;
        }

        public static void DisplayMatrix(int[][] matrix)
        {
            string s = string.Empty;
            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    s += matrix[i][j].ToString().PadLeft(5) + " ";
                }
                s += Environment.NewLine;
            }
            Console.WriteLine(s);
        }
    }
}
