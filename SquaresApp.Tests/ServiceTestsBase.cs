using SquaresApp.Storage.Data;
using SquaresApp.Tests.Infrastructure;

namespace SquaresApp.Tests
{
    public class ServiceTestsBase : IDisposable
    {
        protected ApplicationDbContext Context;
        protected CancellationTokenSource CancellationTokenSource;

        public ServiceTestsBase()
        {
            Context = EntityFrameworkInMemoryDatabase.CreateNewDbContext();
            CancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
            CancellationTokenSource.Dispose();
        }
    }
}
