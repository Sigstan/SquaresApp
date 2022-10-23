using SquaresApp.Models.Points;

namespace SquaresApp.Core.Services.Points
{
    public static class PointsHelper
    {
        public static bool EqualsPoints(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
    }
}
