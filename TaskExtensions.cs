using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DibbsTech.Common {
    public static class TaskExtensions {
        public static async Task Retry(this Func<Task> action) {
            await Retry(action, TimeSpan.FromSeconds(5));
        }
        public static async Task Retry(this Func<Task> action, TimeSpan backoff, int max = 10) {
            var exceptions = new List<Exception>();

            for(var ii = 0; ii<max; ii++) {
                try {
                    await action();
                    return;
                } catch (Exception e){
                    exceptions.Add(e);
                    await Task.Delay(backoff * Math.Pow(2, ii));
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
