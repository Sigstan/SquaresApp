using SquaresApp.Models.Points;

namespace SquaresApp.Tests.Infrastructure
{
    public static class DataHelper
    {
        public static PointModel CreatePoint()
        {
            return new PointModel(GetRandomNumber(), GetRandomNumber());
        }

        public static List<PointModel> CreatePointsList(int? listLength = null)
        {
            var points = new List<PointModel>();
            var length = listLength ?? Math.Abs(GetRandomNumber());

            for (var i = 0; i < length; i++)
            {
                points.Add(CreatePoint());
            }

            return points;
        }

        public static int GetRandomNumber()
        {
            return new Random().Next(-200, 200);
        }
    }
}
