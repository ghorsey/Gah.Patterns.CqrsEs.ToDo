namespace Gah.Blocks.CqrsEs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>RedirectToWhen</c>.
    /// </summary>
    internal static class RedirectToWhen
    {
        /// <summary>
        /// The cache
        /// </summary>
        private static readonly Dictionary<Type, Dictionary<Type, MethodInfo>> Cache =
            new Dictionary<Type, Dictionary<Type, MethodInfo>>();

        /// <summary>
        /// The internal preserve stack trace method
        /// </summary>
        private static readonly MethodInfo InternalPreserveStackTraceMethod =
                    typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// Invokes the event optional.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="event">The event.</param>
        ////[DebuggerNonUserCode]
        public static void InvokeEventOptional(this object instance, object @event)
        {
            var type = @event.GetType();
            if (!TryGetWhenMethod(instance, type, out var info))
            {
                return;
            }

            try
            {
                info.Invoke(instance, new[] { @event });
            }
            catch (TargetInvocationException ex)
            {
                if (InternalPreserveStackTraceMethod != null)
                {
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                }

                throw ex.InnerException ?? ex;
            }
        }

        /// <summary>
        /// Tries the get when method.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="info">The information.</param>
        /// <returns>A/an <c>MethodInfo</c>.</returns>
        private static bool TryGetWhenMethod(object instance, Type eventType, out MethodInfo info)
        {
            CacheWhenMethods(instance.GetType());

            return Cache[instance.GetType()].TryGetValue(eventType, out info);
        }

        /// <summary>
        /// Caches the when methods.
        /// </summary>
        /// <param name="aggregateType">Type of the aggregate.</param>
        private static void CacheWhenMethods(Type aggregateType)
        {
            if (Cache.ContainsKey(aggregateType))
            {
                return;
            }

            Cache[aggregateType] = aggregateType
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1)
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }
    }
}