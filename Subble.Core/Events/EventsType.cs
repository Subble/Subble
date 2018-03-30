namespace Subble.Core.Events
{
    /// <summary>
    /// Helper for event's type
    /// </summary>
    public static class EventsType
    {
        /// <summary>
        /// Main app lifecycle
        /// </summary>
        public static class Core
        {
            private const string PRE = "EVENT_CORE_";

            /// <summary>
            /// Emit on app initialization
            /// </summary>
            public const string INIT = PRE + "INIT";

            /// <summary>
            /// Emit on closing process is started
            /// </summary>
            public const string CLOSING = PRE + "CLOSING";

            /// <summary>
            /// Emit on closing process is closed
            /// </summary>
            public const string CLOSED = PRE + "CLOSED";

            /// <summary>
            /// For logging purposes
            /// </summary>
            public const string LOG = PRE + "LOG";

            /// <summary>
            /// New plugin initialised
            /// </summary>
            public const string NEW_PLUGIN = PRE + "NEW_PLUGIN_LOADED";
        }

        /// <summary>
        /// Helper for Service container events name
        /// </summary>
        public static class ServiceContainer
        {
            private const string PRE = "EVENT_SERVICE_CONTAINER_";

            /// <summary>
            /// Service is register
            /// </summary>
            public const string REGISTER = PRE + "REGISTER";

            /// <summary>
            /// Error on service register
            /// </summary>
            public const string ERROR_REGISTER = PRE + "ERROR_REGISTER";

            /// <summary>
            /// Service is removed
            /// </summary>
            public const string REMOVE = PRE + "REMOVE";
        }
    }
}
