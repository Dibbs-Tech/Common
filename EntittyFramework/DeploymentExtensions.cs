using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DibbsTech.Common.EntittyFramework {
    public static class DeploymentExtensions {
        public static async Task TryDeployment(this DbContext database, ILogger log = null) {
            var deploy = () => {
                database.Database.Migrate();
                return Task.CompletedTask;
            };
            try {
                await deploy.Retry(log);
            } catch (Exception ex) {
                log?.LogCritical("Failed to deploy the database");
                log?.LogInformation(ex, ex.Message);
            }
        }
    }
}
