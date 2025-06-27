namespace CleanArc.Web.Api
{
    public class SerilogSettings
    {
        public bool EnableLogging { get; set; } = true;
        public bool EnableConsoleLogging { get; set; } = true;
        public bool EnableFileLogging { get; set; } = true;
        public bool EnableDatabaseLogging { get; set; } = true;
    }
}
