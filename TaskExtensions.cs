using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DibbsTech.Common {
    public static class TaskExtensions {
        public static async Task Retry(this Func<Task> action, ILogger log = null) {
            await Retry(action, TimeSpan.FromSeconds(5), log:log);
        }
        public static async Task Retry(this Func<Task> action, TimeSpan backoff, int max = 10, ILogger log = null) {
            var exceptions = new List<Exception>();

            for(var ii = 0; ii<max; ii++) {
                try {
                    await action();
                    return;
                } catch (Exception e) {
                    var delay = backoff * Math.Pow(2, ii);
                    log?.LogWarning($"Failed to deploy database, will retry in {delay}");
                    log?.LogInformation(e, e.Message);
                    exceptions.Add(e);
                    await Task.Delay(delay);
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
