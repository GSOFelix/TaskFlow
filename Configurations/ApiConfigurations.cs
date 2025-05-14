namespace TaskFlow.Configurations
{
    public static class ApiConfigurations
    {
        public static string DbConnection { get; } = Environment.GetEnvironmentVariable("DB_CONNECTION")
            ?? throw new InvalidOperationException("String de conexão não esta definida nas variáves de ambiente.");

        public static string JwtKey { get; } = Environment.GetEnvironmentVariable("JWTKEY")
            ?? throw new InvalidOperationException("JWTKEY não esta definida nas variáves de ambiente.");

        public const string JwtIssuer = "TaskFlowAPI";
        public const string JwtAudience = "TaskFlowClient";
    }
}
