using BattleShip.Helper;
using FluentAssertions;
using Xunit;

namespace BattleShip.Tests
{
    public class MatrixTests
    {
        private const int ROWS = 10;

        [Fact]
        public void Matrix_ReturnsIntInt()
        {
            var matrix = Matrix.Setup(ROWS);

            matrix.Should().BeOfType<int[,]>();
        }

        [Fact]
        public void Matrix_ReturnsMatrixOfLength100()
        {
            var matrix = Matrix.Setup(ROWS);

            matrix.Length.Should().Be(100);
        }

        [Fact]
        public void Matrix_Only87SquaresAreEqualWith0()
        {
            var matrix = Matrix.Setup(ROWS);

            int countShips = 0;

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < ROWS; j++)
                {
                    if (matrix[i, j] > 0) countShips++;
                }
            }

            countShips.Should().Be(13);
        }
    }
}