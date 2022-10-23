using Microsoft.EntityFrameworkCore;
using SquaresApp.Core.Services.Points;
using SquaresApp.Models.Enums;
using SquaresApp.Models.Geometry;
using SquaresApp.Models.Points;
using SquaresApp.Storage.Data;

namespace SquaresApp.Core.Services.Geometry.Squares
{
    public class SquareService : IGeometryService
    {
        private readonly ApplicationDbContext _context;

        public SquareService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShapesCountModel> ShapeCount(Guid batchId, CancellationToken cancellationToken)
        {
            var points = await _context.Points.Where(x => x.BatchId == batchId)
                .Select(x => new Point(x.Xaxis, x.Yaxis)).ToArrayAsync(cancellationToken);

            if (points.Length < 4)
                throw new DomainException(DomainErrorCode.InvalidOperation,
                    $"Not enough points for a square. List contains {points.Length} points");

            var squares = SquareCount(points);

            return new ShapesCountModel()
            {
                Count = squares.Count,
                Shapes = squares,
                ShapeType = ShapeType.Square
            };
        }

        public List<GeometricShape> SquareCount(Point[] points)
        {
            var squares = new List<GeometricShape>();

            var pointsSet = new HashSet<Point>();
            var squareIdsSet = new HashSet<SquareIdentifier>();

            foreach (var point in points)
                pointsSet.Add(point);

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    if (PointsHelper.EqualsPoints(points[i], points[j]))
                        continue;
                    
                    var diagonalsPoints = CreateDiagonalsPoints(points[i], points[j]);
                    
                    if (pointsSet.Contains(diagonalsPoints[0]) && pointsSet.Contains(diagonalsPoints[1]))
                    {
                        var squarePoints = new Point[] { points[i], points[j], diagonalsPoints[0], diagonalsPoints[1] };
                        var squareOrdered = squarePoints.OrderBy(x => x.X).ThenBy(x => x.Y).ToArray();
                        
                        var squareId = new SquareIdentifier(squareOrdered[0].X, squareOrdered[0].Y,
                            squareOrdered[1].X,squareOrdered[1].Y, squareOrdered[2].X, squareOrdered[2].Y);

                        if (squareIdsSet.Contains(squareId))
                            continue;

                        squareIdsSet.Add(squareId);
                        squares.Add(new Square(squareOrdered));
                    }
                }
            }
            return squares;
        }

        public Point[] CreateDiagonalsPoints(Point a, Point c)
        {
            int midX = (a.X + c.X) / 2;
            int midY = (a.Y + c.Y) / 2;

            int bX = midX - (a.Y - midY);
            int bY = midY + (a.X - midX);
            
            int dX = midX - (c.Y - midY);
            int dY = midY + (c.X - midX);
            
            return new[] {new Point(bX, bY), new Point(dX, dY)};
        }
    }
}
