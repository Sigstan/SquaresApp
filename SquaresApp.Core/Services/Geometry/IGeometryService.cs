using SquaresApp.Models.Geometry;

namespace SquaresApp.Core.Services.Geometry
{
    public interface IGeometryService
    {
        public Task<ShapesCountModel> ShapeCount(Guid batchId, CancellationToken cancellationToken);
    }
}