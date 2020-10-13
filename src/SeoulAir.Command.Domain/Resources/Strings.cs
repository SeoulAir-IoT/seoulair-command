namespace SeoulAir.Command.Domain.Resources
{
    public static class Strings
    {
        #region Swagger Documentation

        public const string OpenApiInfoProjectName = "SeoulAir.Command API";
        public const string OpenApiInfoTitle = "SeoulAir Command microservice.";
        public const string OpenApiInfoProjectVersion = "1.0";
        public const string OpenApiInfoDescription
            = "SeoulAir Command is microservice that is part of SeoulAir project.\n" +
            "For more documentation visit: URI";
        public const string SwaggerEndpoint = "/swagger/v1/swagger.json";

        #endregion

        #region Exception Middleware Handler

        public const string InternalServerErrorUri = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        public const string ConflictUri = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
        public const string InternalServerErrorTitle = "500 Internal Server Error.";
        public const string ConflictTitle = "409 Conflict.";

        #endregion

        #region Mqtt Sending\Recieving

        public const string MqttSenderClientId = "seoul-air-command-notification-sender";
        public const string InvalidTopicValueMessage = "Topic must not be null or empty string.";
        public const string MqttSenderStartingMessage = "Mqtt {0} sending service is running in the background";

        #endregion

        public const string ParameterNullOrEmptyMessage = "Parameter {0} must not be null or empty string.";
        public const string ParameterBetweenMessage = "Value of parameter {0} must be between {1} and {2}.";
    }
}
