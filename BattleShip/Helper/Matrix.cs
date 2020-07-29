namespace BattleShip.Helper
{
    public static class Matrix
    {
        public static int[,] Setup(int rows)
        {
            var matrix = new int[rows, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            // ship 1 - "Battleship", 5 squares, number = 1
            matrix = RandomShipAllocation(matrix, rows, 5, 1);

            // ship 2 - "Destroyer", 4 squares, number = 2
            matrix = RandomShipAllocation(matrix, rows, 4, 2);

            // ship 3 - "Destroyer", 4 squares, number = 3
            matrix = RandomShipAllocation(matrix, rows,4, 3);

            return matrix;
        }

        private static int[,] RandomShipAllocation(int[,] matrix, int rows, int shipLength, int shipNumber)
        {
            var coordinates = Coordinates.RandomCoordinates(matrix);

            var x = coordinates.Item1;
            var y = coordinates.Item2;

            // run till ship gets allocated coordinates
            while(true) 
            {
                // horizontal
                // right
                if (y + shipLength <= rows)
                {
                    if (Coordinates.CheckShipCoordinatesY(matrix, shipLength, x, y))
                    {
                        Coordinates.WriteShipCoordinatesY(matrix, shipLength, x, y, shipNumber);

                        return matrix;
                    }
                }

                // left
                if (y - (shipLength - 1) >= 0) 
                {
                    if (Coordinates.CheckShipCoordinatesY(matrix, shipLength, x, y - (shipLength - 1)))
                    {
                        Coordinates.WriteShipCoordinatesY(matrix, shipLength, x, y - (shipLength - 1), shipNumber);

                        return matrix;
                    }
                }

                // vertically
                // down
                if (x + shipLength <= rows)
                {
                    if (Coordinates.CheckShipCoordinatesX(matrix, shipLength, x, y))
                    {
                        Coordinates.WriteShipCoordinatesX(matrix, shipLength, x, y, shipNumber);

                        return matrix;
                    }
                }

                // up
                if (x - (shipLength - 1) >= 0)
                {
                    if (Coordinates.CheckShipCoordinatesX(matrix, shipLength, x - (shipLength - 1), y))
                    {
                        Coordinates.WriteShipCoordinatesX(matrix, shipLength, x - (shipLength - 1), y, shipNumber);

                        return matrix;
                    }
                }

                // new coordinates need to be set up
                var newCoordinates = Coordinates.RandomCoordinates(matrix);

                x = newCoordinates.Item1;
                y = newCoordinates.Item2;
            }            
        }
    }
}