namespace InvoiceImporter.Domain.Settings
{
    public class AppSettings
    {
        public Connectionstrings? ConnectionStrings { get; set; }
        public Logging? Logging { get; set; }
        public string AllowedHosts { get; set; } = string.Empty;
    }

    public class Connectionstrings
    {
        public string SQLServer { get; set; } = string.Empty;
    }

    public class Logging
    {
        public Loglevel? LogLevel { get; set; } 
    }

    public class Loglevel
    {
        public string Default { get; set; } = string.Empty;
        public string MicrosoftAspNetCore { get; set; } = string.Empty;
    }   
}
