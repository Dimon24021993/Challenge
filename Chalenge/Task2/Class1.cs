using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    [Flags]
    [Serializable]
    internal enum InternalTaskOptions
    {
        None = 0,
        InternalOptionsMask = 65280,
        ChildReplica = 256,
        ContinuationTask = 512,
        PromiseTask = 1024,
        SelfReplicating = 2048,
        LazyCancellation = 4096,
        QueuedByRuntime = 8192,
        DoNotDispose = 16384,
    }



    public static class Class1
    {
        /*  Create set of methods for Task and Task<T>, each of them will be similar to ContinueWith() from TPL,
            but will execute continuation in main(UI thread).
            Overload method with parameters:
            • Action
            • Action<Task> where parameter is a task, for which continuation is created
            • Func<T> / Func<Task<T>>
            • Func<Task, T> / Func<Task, Task<T>> where parameter is a task, for which continuation is created
            Method returns Task(for overloads with Action) or Task<T>(for overloads with Func, Result is a value,
            returned by continuation). Returned task must finish only after main Task and continuation are
            completed.
        */

        public static void ContinueWith(this Task sds, Action action)
        {
            sds.Wait();
            action.Invoke();
        }

        public static Task ContinueWith<T>(this Task<T> task, Action action)
        {
            var obj = task.Result;
            action.Invoke();
            return new Task(()=> {});
        }

        public static void ContinueWith(this Task task, Action<Task> continuationAction)
        {
            task.Wait();
            continuationAction.Invoke(task);
        }

        public static Task ContinueWith<T>(this Task<T> task, Action<Task> continuationAction)
        {
            return new Task(()=> {});
        }

       






    }
}
