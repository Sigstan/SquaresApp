using SquaresApp.Models.Points;

namespace SquaresApp.Models.Geometry
{
    public abstract class GeometricShape
    {
        public Point[] Points { get; }

        protected GeometricShape(Point[] points)
        {
            Points = points;
        }
    }
}