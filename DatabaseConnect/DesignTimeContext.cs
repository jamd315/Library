using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DatabaseConnect
{
    public class DesignTimeContext : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var CnString = @"Server=tcp:lizardswimmer-dbserver.database.windows.net,1433;Initial Catalog=lizardswimmer-db;Persist Security Info=False;User ID=jamd315;Password=ALongCreativePasswordForTheDatabase:);MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var CntxtBuilder = new DbContextOptionsBuilder<Context>()
                    .UseSqlServer(CnString)
                    .Options;
            return new Context(CntxtBuilder);
        }
    }
}
