using System;

namespace BattleShip.Helper
{
    public static class Coordinates
    {
        public static Tuple<int, int> RandomCoordinates(int[,] matrix)
        {
            Random random = new Random(); 

            bool coordinates_taken = true;

            int x = -1;
            int y = -1;

            while (coordinates_taken)
            {
                x = random.Next(0, 10);
                y = random.Next(0, 10);

                if (matrix[x, y] < 4) 
                {
                    coordinates_taken = false;
                }
            }

            return new Tuple<int, int>(x, y);            
        }

        public static bool CheckShipCoordinatesY(int[,] matrix, int length, int x, int y)
        {
            for (int i = y; i < y + length; i++)
            {
                if (matrix[x, i] > 0) return false;
            }

            return true;
        }

        public static bool CheckShipCoordinatesX(int[,] matrix, int length, int x, int y)
        {
            for (int i = x; i < x + length; i++)
            {
                if (matrix[i, y] > 0) return false;
            }

            return true;
        }

        public static int[,] WriteShipCoordinatesY(int[,] matrix, int length, int x, int y, int ship)
        {
            for (int i = y; i < y + length; i++)
            {
                matrix[x, i] = ship;
            }

            return matrix;
        }

        public static int[,] WriteShipCoordinatesX(int[,] matrix, int length, int x, int y, int ship)
        {
            for (int i = x; i < x + length; i++)
            {
                matrix[i, y] = ship;
            }

            return matrix;
        }
    }
}