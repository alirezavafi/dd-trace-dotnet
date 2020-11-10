using Datadog.Trace.DuckTyping;

namespace Datadog.Trace.ClrProfiler.AutoInstrumentation.HttpClient
{
    /// <summary>
    /// Http method struct copy target for ducktyping
    /// </summary>
    [DuckCopy]
    public struct HttpMethodStruct
    {
        /// <summary>
        /// Gets the http method in string
        /// </summary>
        public string Method;
    }
}