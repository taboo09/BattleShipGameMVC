using System;
using System.Collections.Generic;
using System.Linq;
using BattleShip.Helper;
using FluentAssertions;
using Xunit;

namespace BattleShip.Tests
{
    public class CoordinatesTests : IDisposable
    {
        private const int ROWS = 10;
        int[,] matrix;
        public CoordinatesTests()
        {
            matrix =  new int[ROWS, ROWS];

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < ROWS; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        [Fact]
        public void RandomCoordinates_CheckXandY_ReturnTuple()
        {
            var tuple = Coordinates.RandomCoordinates(matrix);

            tuple.Should().BeOfType<Tuple<int, int>>();
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void RandomCoordinates_CheckXandY_AgainstData(int x, int y)
        {
            matrix[x, y] = 4;

            var tuple = Coordinates.RandomCoordinates(matrix);

            tuple.Should().NotBeEquivalentTo(new Tuple<int, int>(x, y));
        }

        [Theory]
        [InlineData(5, 9, 1)]
        [InlineData(4, 0, 5)]
        [InlineData(4, 7, 1)]
        public void CheckShipCoordinatesY_ShipCoordinatesEqual0_ReturnsTrue(int length, int x, int y)
        {
            matrix = DummyShips(matrix);

            var check = Coordinates.CheckShipCoordinatesY(matrix, length, x, y);

            check.Should().BeTrue();
        }

        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(4, 2, 6)]
        [InlineData(4, 6, 6)]
        public void CheckShipCoordinatesY_ShipCoordinatesGreaterThan0_ReturnsFalse(int length, int x, int y)
        {
            matrix = DummyShips(matrix);
            
            var check = Coordinates.CheckShipCoordinatesY(matrix, length, x, y);

            check.Should().BeFalse();
        }

        [Theory]
        [InlineData(5, 0, 0)]
        [InlineData(4, 3, 2)]
        [InlineData(4, 6, 9)]
        public void CheckShipCoordinatesX_ShipCoordinatesEqual0_ReturnsTrue(int length, int x, int y)
        {
            matrix = DummyShips(matrix);

            var check = Coordinates.CheckShipCoordinatesX(matrix, length, x, y);

            check.Should().BeTrue();
        }

        [Theory]
        [InlineData(5, 1, 5)]
        [InlineData(4, 2, 5)]
        [InlineData(4, 6, 6)]
        public void CheckShipCoordinatesX_ShipCoordinatesGreaterThan0_ReturnsFalse(int length, int x, int y)
        {
            matrix = DummyShips(matrix);

            var check = Coordinates.CheckShipCoordinatesX(matrix, length, x, y);

            check.Should().BeFalse();
        }

        [Theory]
        [InlineData(5, 1, 1, 1)]
        [InlineData(4, 2, 2, 2)]
        public void WriteShipCoordinatesY_ValueGreaterThan0_ReturnsMatrixWithShipValue(int shipLength, int x, int y, int shipValue)
        {
            matrix = Coordinates.WriteShipCoordinatesY(matrix, shipLength, x, y, shipValue);

            matrix[x, y].Should().Be(shipValue);
            matrix[x, y + shipLength - 1].Should().Be(shipValue);
        }

        [Theory]
        [InlineData(5, 5, 4, 1)]
        [InlineData(4, 7, 1, 2)]
        public void WriteShipCoordinatesY_ValueGreaterEqual0_ReturnsMatrixWithShipValue(int shipLength, int x, int y, int shipValue)
        {
            matrix = Coordinates.WriteShipCoordinatesY(matrix, shipLength, x, y, shipValue);

            matrix[x, y - 1].Should().Be(0);
            matrix[x, y + shipLength].Should().Be(0);
        }

        [Theory]
        [InlineData(5, 1, 1, 1)]
        [InlineData(4, 2, 2, 2)]
        public void WriteShipCoordinatesX_ValueGreaterThan0_ReturnsMatrixWithShipValue(int shipLength, int x, int y, int shipValue)
        {
            matrix = Coordinates.WriteShipCoordinatesX(matrix, shipLength, x, y, shipValue);

            matrix[x, y].Should().Be(shipValue);
            matrix[x + shipLength - 1, y].Should().Be(shipValue);
        }

        [Theory]
        [InlineData(5, 3, 2, 1)]
        [InlineData(4, 5, 9, 2)]
        public void WriteShipCoordinatesX_ValueGreaterEqual0_ReturnsMatrixWithShipValue(int shipLength, int x, int y, int shipValue)
        {
            matrix = Coordinates.WriteShipCoordinatesX(matrix, shipLength, x, y, shipValue);

            matrix[x - 1, y].Should().Be(0);
            matrix[x + shipLength, y].Should().Be(0);
        }

        public static IEnumerable<object[]> Data => 
            new List<object[]>() 
            {
                new object[] {1, 1},
                new object[] {2, 1},
                new object[] {3, 1},
                new object[] {4, 1},
                new object[] {5, 6},
                new object[] {5, 7},
                new object[] {5, 8}
            };

        public int[,] DummyShips(int[,] matrix)
        {
            matrix[1, 1] = 1;
            matrix[1, 2] = 1;
            matrix[1, 3] = 1;
            matrix[1, 4] = 1;
            matrix[1, 5] = 1;

            matrix[2, 3] = 2;
            matrix[2, 4] = 2;
            matrix[2, 5] = 2;
            matrix[2, 6] = 2;

            matrix[6, 6] = 3;
            matrix[7, 6] = 3;
            matrix[8, 6] = 3;
            matrix[9, 6] = 3;

            return matrix;
        }

        public void Dispose()
        {
            matrix = new int[0, 0];
        }
    }
}