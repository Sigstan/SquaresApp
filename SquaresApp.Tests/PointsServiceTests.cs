using Microsoft.EntityFrameworkCore;
using SquaresApp.Core;
using SquaresApp.Core.Services.Points;
using SquaresApp.Storage.Entities;
using SquaresApp.Tests.Infrastructure;
using Xunit;

namespace SquaresApp.Tests
{
    public class PointsServiceTests : ServiceTestsBase
    {
        private readonly IPointsService _pointsService;

        public PointsServiceTests()
        {
            _pointsService = new PointsService(Context);
        }

        [Fact]
        public async Task ImportList_ListIsValid_ShouldBeSaved()
        {
            var pointsBatch = await CreatePointsBatch();
            Assert.NotEqual(pointsBatch.BatchId, Guid.Empty);

            var actualPoints = await Context.Points.Where(x => x.BatchId == pointsBatch.BatchId).ToListAsync(CancellationTokenSource.Token);
            Assert.Equal(pointsBatch.Points.Count, actualPoints.Count);
        }

        [Fact]
        public async Task ImportList_ListEmpty_ShouldThrowException()
        {
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => _pointsService.ImportList(new List<Models.Points.PointModel>(), CancellationTokenSource.Token));

            Assert.Equal(DomainErrorCode.InvalidArgument, exception.ErrorCode);
        }

        [Fact]
        public async Task Add_ListDoesNotExist_ShouldThrowException()
        {
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => _pointsService.Add(Guid.NewGuid(), DataHelper.CreatePoint(), CancellationTokenSource.Token));

            Assert.Equal(DomainErrorCode.NotFound, exception.ErrorCode);
        }

        [Fact]
        public async Task Add_ListExists_ShouldBeSavedInDatabase()
        {
            var pointsBatch = await CreatePointsBatch();
            var point = DataHelper.CreatePoint();

            await _pointsService.Add(pointsBatch.BatchId, point, CancellationTokenSource.Token);

            var actualPoint = await Context.Points.FirstOrDefaultAsync(x => x.BatchId == pointsBatch.BatchId &&
                                                                      x.Xaxis == point.X && x.Yaxis == point.Y);

            Assert.NotNull(actualPoint);
            Assert.Equal(point.X, actualPoint.Xaxis);
            Assert.Equal(point.Y, actualPoint.Yaxis);
            Assert.Equal(pointsBatch.BatchId, actualPoint.BatchId);
        }

        [Fact]
        public async Task Delete_PointExists_ShouldBeDeleted()
        {
            var pointsBatch = await CreatePointsBatch();
            var pointToRemove = pointsBatch.Points.First();
            await _pointsService.Delete(pointsBatch.BatchId, pointsBatch.Points.First(), CancellationTokenSource.Token);

            var actualPoint = await GetPointByBatchIdAndCoordinates(pointsBatch.BatchId, pointToRemove);

            Assert.Null(actualPoint);
        }

        [Fact]
        public async Task Delete_PointDoNotExists_ShouldThrowException()
        {
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => _pointsService.Add(Guid.NewGuid(), DataHelper.CreatePoint(), CancellationTokenSource.Token));

            Assert.Equal(DomainErrorCode.NotFound, exception.ErrorCode);
        }

        private async Task<(Guid BatchId, List<Models.Points.PointModel> Points)> CreatePointsBatch()
        {
            var points = DataHelper.CreatePointsList();
            var batchId = await _pointsService.ImportList(points, CancellationTokenSource.Token);
            return (batchId, points);
        }

        private async Task<Point?> GetPointByBatchIdAndCoordinates(Guid batchId, Models.Points.PointModel point)
        {
            return await Context.Points.FirstOrDefaultAsync(x => x.BatchId == batchId &&
                                                                  x.Xaxis == point.X && x.Yaxis == point.Y);
        }
    }
}
