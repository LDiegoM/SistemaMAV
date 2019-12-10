using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaMAV.DataAccess.Data {

    public static class InitContext {
        public static void AddDbContextToService(IServiceCollection services, IConfiguration configuration) {

            string strDatabaseType = configuration.GetValue<string>(Constants.CONFIG_DATABASE_TYPE);
            bool flgSqlServer = strDatabaseType == Constants.DATABASE_TYPE_SQL_SERVER;
            
            if (flgSqlServer) {
                services.AddDbContext<SistemaMAV.DataAccess.Data.MavDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString(Constants.CONFIG_CONNECTION_STRING_SQL_SERVER)));
            }
            else {
                // Adds SQLite objects
                services.AddDbContext<SistemaMAV.DataAccess.Data.MavDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString(Constants.CONFIG_CONNECTION_STRING_SQLITE)));
            }
        }
    }
}
