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

        public static bool IsMutant(String[] dna)
        {
            char[][] matrix = dna.Select(item => item.ToArray()).ToArray();
            return IsMutant(matrix);
        }

        private static bool IsMutant(char[][] dna)
        {
            int total = 0;
            int size = dna.GetLength(0);

            List<Action> actions = new List<Action>();
            int[] results = new int[NumberOfOcurrences];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Action righDiagonalAction = () => { };
                    Action horizontalAction = () => { };
                    Action verticalAction = () => { };
                    Action leftDiagonalAction = () => { };
                    if (i == 0 && j == 0)
                    {
                        righDiagonalAction = () => results[RightDiagonalPosition] = RighDiagonal(dna, i, j, dna[i][j], 0);
                        horizontalAction = () => results[HorizontalPosition] = Horizontal(dna, i, j, dna[i][j], 0);
                        verticalAction = () => results[VerticalPosition] = Vertical(dna, i, j, dna[i][j], 0);
                    }
                    else if (i == dna.Length - 1)
                    {
                        horizontalAction = () => results[HorizontalPosition] = Horizontal(dna, i, j, dna[i][j], 0);
                    }
                    else if (j == size - 1 && i == size - 1)
                    {
                        verticalAction = () => results[VerticalPosition] = Vertical(dna, i, j, dna[i][j], 0);
                        leftDiagonalAction = () => results[LeftDiagonalPosition] = LeftDiagonal(dna, i, j, dna[i][j], 0);
                    }
                    else
                    {
                        righDiagonalAction = () => results[RightDiagonalPosition] = RighDiagonal(dna, i, j, dna[i][j], 0);
                        horizontalAction = () => results[HorizontalPosition] = Horizontal(dna, i, j, dna[i][j], 0);
                        verticalAction = () => results[VerticalPosition] = Vertical(dna, i, j, dna[i][j], 0);
                        leftDiagonalAction = () => results[LeftDiagonalPosition] = LeftDiagonal(dna, i, j, dna[i][j], 0);
                    }
                    actions.Add(righDiagonalAction);
                    actions.Add(horizontalAction);
                    actions.Add(verticalAction);
                    actions.Add(leftDiagonalAction);
                    Parallel.Invoke(actions.ToArray());
                    actions.Clear();
                    total = total + results.Sum();
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
                bool canContinue = ValidateNextPosiccion(dna, i + 1, j + 1);
                if (canContinue)
                {
                    return RighDiagonal(dna, i + 1, j + 1, value, ocurrences);
                }
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
                bool canContinue = ValidateNextPosiccion(dna, i + 1, j - 1);
                if (canContinue)
                {
                    return LeftDiagonal(dna, i + 1, j - 1, value, ocurrences);
                }
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
                bool canContinue = ValidateNextPosiccion(dna, i, j + 1);
                if (canContinue)
                {
                    return Horizontal(dna, i, j + 1, value, ocurrences);
                }
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
                bool canContinue = ValidateNextPosiccion(dna, i + 1, j);
                if (canContinue)
                {
                    return Vertical(dna, i + 1, j, value, ocurrences);
                }
            }
            return 0;
        }

        private static bool ValidateNextPosiccion(char[][] dna, int i, int j)
        {
            int size = dna.GetLength(0);

            if (i < size && j < size && i >= 0 && j >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
