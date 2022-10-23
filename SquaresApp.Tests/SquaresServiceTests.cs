using SquaresApp.Core.Services.Geometry.Squares;
using SquaresApp.Models.Points;
using Xunit;

namespace SquaresApp.Tests
{
    public class SquaresServiceTests : ServiceTestsBase
    {
        private readonly SquareService _squareService;

        public SquaresServiceTests()
        {
            _squareService = new SquareService(Context);
        }

        [Fact]
        public void SquareCount_ArrayIsValid_ShouldReturnSquareList()
        {
            var points = new Point[]
            {
                new Point(1,1),
                new Point(-1,-1),
                new Point(-1,1),
                new Point(1,-1),
                new Point(7,-1),
                new Point(1,5),
                new Point(4,5)
            };

            var actualSquares = _squareService.SquareCount(points.ToArray());

            Assert.Single(actualSquares);
        }
    }
}