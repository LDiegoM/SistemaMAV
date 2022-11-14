using System;
using Microsoft.EntityFrameworkCore;

namespace SistemaMAV.Web.Internal;

public static class InitDb {
    public static void AddDbContextToService(IServiceCollection services, IConfiguration configuration) {

        string strDatabaseType = configuration.GetValue<string>(Constants.CONFIG_DATABASE_TYPE);
        bool flgSqlServer = strDatabaseType == Constants.DATABASE_TYPE_SQL_SERVER;

        if (flgSqlServer) {
            services.AddDbContext<SistemaMAV.Web.Data.ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(Constants.CONFIG_CONNECTION_STRING_SQL_SERVER)));
        } else {
            // Adds SQLite objects
            services.AddDbContext<SistemaMAV.Web.Data.ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString(Constants.CONFIG_CONNECTION_STRING_SQLITE)));
        }
    }
}
