using HeatGames.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeatGames.Tests.Helpers
{
    public static class DbContextHelper
    {
        public static HeatGamesDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<HeatGamesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new HeatGamesDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
