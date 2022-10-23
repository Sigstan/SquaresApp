using System.ComponentModel.DataAnnotations;

namespace SquaresApp.Models.Points
{
    public class PointModel
    {
        [Required]
        public int X { get; }
        [Required]
        public int Y { get; }

        public PointModel(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}