using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaMAV.DataAccess.Data {

    public static class InitContext {
        public static void AddDbContextToService(IServiceCollection services, IConfiguration configuration, bool flgSqlServer = false) {

            if (flgSqlServer) {
                services.AddDbContext<SistemaMAV.DataAccess.Data.MavDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("MavDbContext")));
            }
            else {
                // Adds SQLite objects
                services.AddDbContext<SistemaMAV.DataAccess.Data.MavDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("MavDbContext_Sqlite")));
            }
        }
    }
}
