using Microsoft.EntityFrameworkCore;
using SquaresApp.Storage.Data;
using SquaresApp.Storage.Entities;

namespace SquaresApp.Core.Services.Points
{
    public class PointsService : IPointsService
    {
        private readonly ApplicationDbContext _context;

        public PointsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> ImportList(List<Models.Points.PointModel> points, CancellationToken cancellationToken)
        {
            if (!points.Any())
                throw new DomainException(DomainErrorCode.InvalidArgument, "List of points is empty");

            var batchId = Guid.NewGuid();

            var entities = points.Select(x => ConvertToEntity(batchId, x));

            _context.Points.AddRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
            return batchId;
        }

        public async Task Add(Guid batchId, Models.Points.PointModel point, CancellationToken cancellationToken)
        {
            var batchExists = await _context.Points.AnyAsync(x => x.BatchId == batchId, cancellationToken);

            if (!batchExists)
                throw NotFound();

            _context.Add(ConvertToEntity(batchId, point));
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Guid batchId, Models.Points.PointModel point, CancellationToken cancellationToken)
        {
            var entity = await _context.Points.FirstOrDefaultAsync(x => x.BatchId == batchId
                                                                             && x.Xaxis == point.X && x.Yaxis == point.Y, cancellationToken);
            if (entity == null)
                throw NotFound();

            _context.Points.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

        }

        private static Point ConvertToEntity(Guid batchId, Models.Points.PointModel point)
        {
            return new Point()
            {
                BatchId = batchId,
                Xaxis = point.X,
                Yaxis = point.Y,
                Id = Guid.NewGuid()
            };
        }

        private static DomainException NotFound()
        {
            return new DomainException(DomainErrorCode.NotFound, "List of point was not found");
        }
    }
}
