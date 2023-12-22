using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Common
{
    public static partial class std
    {
        [Obsolete("Change use ExeDispose")]
        public static void ExecuteDispose<TService>(this TService service, Action<TService> actionService)
        {
            using var serviceWrapper = new DisposeWrapper<TService>(service);
            actionService(serviceWrapper.Value);
        }

        public static void ExeDispose<TService>(this TService service, Action<TService> actionService)
        {
            using var serviceWrapper = new DisposeWrapper<TService>(service);
            actionService(serviceWrapper.Value);
        }

        [Obsolete("Change use ExeDispose")]
        public static TResult ExecuteDispose<TService, TResult>(this TService service, Func<TService, TResult> funcService)
        {
            using var serviceWrapper = new DisposeWrapper<TService>(service);
            return funcService(serviceWrapper.Value);
        }

        public static TResult ExeDispose<TService, TResult>(this TService service, Func<TService, TResult> funcService)
        {
            using var serviceWrapper = new DisposeWrapper<TService>(service);
            return funcService(serviceWrapper.Value);
        }

        /// <summary>
        /// Wrapper of ExeDisposeAsync
        /// </summary>
        public static async Task<TResult> DisposeAsync<TService, TResult>(this TService service, Func<TService, Task<TResult>> funcService)
            => await ExeDisposeAsync(service, funcService);

        public static async Task<TResult> ExeDisposeAsync<TService, TResult>(this TService service, Func<TService, Task<TResult>> funcService)
        {
            using var serviceWrapper = new DisposeWrapper<TService>(service);
            return await funcService(serviceWrapper.Value);
        }

        public static Task<List<T>> ListEmptyAsync<T>() => Task.FromResult(new List<T>());

        public static async Task<object> TaskAsObjectAsync<TResult>(Func<Task<TResult>> execute) => await execute();
    }
}
