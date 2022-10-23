using SquaresApp.Models.Points;

namespace SquaresApp.Core.Services.Points
{
    public interface IPointsService
    {
        Task<Guid> ImportList(List<PointModel> points, CancellationToken cancellationToken);
        Task Add(Guid batchId, PointModel point, CancellationToken cancellationToken);
        Task Delete(Guid batchId, PointModel point, CancellationToken cancellationToken);
    }
}