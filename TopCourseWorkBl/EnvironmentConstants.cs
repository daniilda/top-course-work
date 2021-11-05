namespace TopCourseWorkBl
{
    public static class EnvironmentConstants
    {
        #region Database environment variables

        public const string DatabaseUser = "DB_USER";
        public const string DatabasePassword = "DB_PASSWORD";
        public const string DatabaseHost = "DB_HOST";
        public const string DatabasePort = "DB_PORT";
        public const string DatabaseName = "DB_NAME";
        public const string DatabasePooling = "DB_POOLING";
        public const string DatabaseMinPoolSize = "DB_MIN_POOL_SIZE";
        public const string DatabaseMaxPoolSize = "DB_MAX_POOL_SIZE";
        public const string DatabaseConnectionLifetime = "DB_CONNECTION_LIFETIME";

        #endregion

        #region Auth environment variables

        public const string AuthKey = "AUTH_TOKEN";

        #endregion
    }
}