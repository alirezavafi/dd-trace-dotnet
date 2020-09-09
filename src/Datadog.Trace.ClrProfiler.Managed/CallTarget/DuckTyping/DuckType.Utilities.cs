using System;
using System.ComponentModel;

namespace Datadog.Trace.ClrProfiler.CallTarget.DuckTyping
{
    /// <summary>
    /// Duck Type
    /// </summary>
    public partial class DuckType
    {
        /// <summary>
        /// Checks and ensures the arguments for the Create methods
        /// </summary>
        /// <param name="duckType">Duck type</param>
        /// <param name="instance">Instance value</param>
        /// <exception cref="ArgumentNullException">If the duck type or the instance value is null</exception>
        private static void EnsureArguments(Type duckType, object instance)
        {
            if (duckType is null)
            {
                throw new ArgumentNullException(nameof(duckType), "The duck type can't be null");
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance), "The object instance can't be null");
            }

            if (!duckType.IsPublic && !duckType.IsNestedPublic)
            {
                throw new DuckTypeTypeIsNotPublicException(duckType, nameof(duckType));
            }
        }

        /// <summary>
        /// Get inner DuckType
        /// </summary>
        /// <param name="field">Field reference</param>
        /// <param name="duckType">Duck type</param>
        /// <param name="value">Property value</param>
        /// <returns>DuckType instance</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IDuckType GetInnerDuckType(ref IDuckType field, Type duckType, object value)
        {
            if (value is null)
            {
                field = null;
                return null;
            }

            var valueType = value.GetType();
            if (field is null || field.Type != valueType)
            {
                CreateTypeResult result = GetOrCreateProxyType(duckType, valueType);
                result.ExceptionInfo?.Throw();
                field = (IDuckType)Activator.CreateInstance(result.Type);
            }

            field.SetInstance(value);
            return field;
        }

        /// <summary>
        /// Set inner DuckType
        /// </summary>
        /// <param name="field">Field reference</param>
        /// <param name="value">DuckType instance</param>
        /// <returns>Property value</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object SetInnerDuckType(ref IDuckType field, DuckType value)
        {
            field = value;
            return field?.Instance;
        }
    }
}