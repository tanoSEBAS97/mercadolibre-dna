using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoLibre.Mutant.Dna.Core.Util
{
    public static class DnaMutantUtil
    {
        private const int NumberOfOcurrences = 4;
        private const int RightDiagonalPosition = 0;
        private const int LeftDiagonalPosition = 1;
        private const int HorizontalPosition = 2;
        private const int VerticalPosition = 3;
        private static int size;

        public static bool IsMutant(String[] dna)
        {
            char[][] matrix = dna.Select(item => item.ToArray()).ToArray();
            size = dna.GetLength(0);
            return size < NumberOfOcurrences ? false : IsMutant(matrix);
        }

        private static bool IsMutant(char[][] dna)
        {
            int total = 0;
            List<Action> actions = new List<Action>();
            int[] results = new int[NumberOfOcurrences];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    //Threads/concurrency of posibble path
                    Action righDiagonalAction = () => { };
                    Action horizontalAction = () => { };
                    Action verticalAction = () => { };
                    Action leftDiagonalAction = () => { };
                    //horizontal path
                    if (j + NumberOfOcurrences <= size)
                    {
                        horizontalAction = () => results[HorizontalPosition] = Horizontal(dna, i, j, dna[i][j], 0);
                    }
                    //vertical path
                    if (i + NumberOfOcurrences <= size)
                    {
                        verticalAction = () => results[VerticalPosition] = Vertical(dna, i, j, dna[i][j], 0);
                    }
                    //righ diagonal path
                    if (j + NumberOfOcurrences <= size && i + NumberOfOcurrences <= size)
                    {
                        righDiagonalAction = () => results[RightDiagonalPosition] = RighDiagonal(dna, i, j, dna[i][j], 0);
                    }
                    //left diagonal path
                    if (j - NumberOfOcurrences >= -1 && i + NumberOfOcurrences <= size)
                    {
                        leftDiagonalAction = () => results[LeftDiagonalPosition] = LeftDiagonal(dna, i, j, dna[i][j], 0);
                    }
                    //multiple threads to improve the performance
                    actions.Add(righDiagonalAction);
                    actions.Add(horizontalAction);
                    actions.Add(verticalAction);
                    actions.Add(leftDiagonalAction);
                    //run 4 threads in the same time
                    Parallel.Invoke(actions.ToArray());
                    actions.Clear();
                    total += results.Sum();
                    Array.Clear(results, 0, results.Length);
                    if (total > 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static int RighDiagonal(char[][] dna, int i, int j, char value, int ocurrences)
        {
            if (dna[i][j] == value)
            {
                ocurrences = ocurrences + 1;
                if (ocurrences == NumberOfOcurrences)
                {
                    return 1;
                }
                return RighDiagonal(dna, i + 1, j + 1, value, ocurrences);
            }
            return 0;
        }

        private static int LeftDiagonal(char[][] dna, int i, int j, char value, int ocurrences)
        {
            if (dna[i][j] == value)
            {
                ocurrences = ocurrences + 1;
                if (ocurrences == NumberOfOcurrences)
                {
                    return 1;
                }
                return LeftDiagonal(dna, i + 1, j - 1, value, ocurrences);
            }
            return 0;
        }

        private static int Horizontal(char[][] dna, int i, int j, char value, int ocurrences)
        {
            if (dna[i][j] == value)
            {
                ocurrences = ocurrences + 1;
                if (ocurrences == NumberOfOcurrences)
                {
                    return 1;
                }
                return Horizontal(dna, i, j + 1, value, ocurrences);
            }
            return 0;
        }
        private static int Vertical(char[][] dna, int i, int j, char value, int ocurrences)
        {
            if (dna[i][j] == value)
            {
                ocurrences = ocurrences + 1;
                if (ocurrences == NumberOfOcurrences)
                {
                    return 1;
                }
                return Vertical(dna, i + 1, j, value, ocurrences);
            }
            return 0;
        }


    }
}
