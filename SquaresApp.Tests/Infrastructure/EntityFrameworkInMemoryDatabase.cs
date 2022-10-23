using Microsoft.EntityFrameworkCore;
using SquaresApp.Storage.Data;

namespace SquaresApp.Tests.Infrastructure
{
    public static class EntityFrameworkInMemoryDatabase
    {
        public static ApplicationDbContext CreateNewDbContext()
        {
            var contextOption = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            return new ApplicationDbContext(contextOption.Options);
        }
    }
}