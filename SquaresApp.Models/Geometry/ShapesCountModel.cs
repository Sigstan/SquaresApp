using SquaresApp.Models.Enums;

namespace SquaresApp.Models.Geometry
{
    public class ShapesCountModel
    {
        public List<GeometricShape> Shapes { get; set; } = new();
        public int Count { get; set; }
        public ShapeType ShapeType { get; set; }
    }
}