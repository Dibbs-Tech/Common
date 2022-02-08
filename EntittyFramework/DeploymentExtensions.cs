using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DibbsTech.Common.EntittyFramework {
    public static class DeploymentExtensions {
        public static async Task TryDeployment(this DbContext database) {
            Func<Task> deploy = () => {
                database.Database.Migrate();
                return Task.CompletedTask;
            };

            await deploy.Retry();
        }
    }
}
