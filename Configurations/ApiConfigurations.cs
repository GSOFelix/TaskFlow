namespace TaskFlow.Configurations
{
    public static class ApiConfigurations
    {
        public static string? DbConnection { get;} = Environment.GetEnvironmentVariable("DB_CONNECTION");
    }
}
