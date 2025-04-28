using AI_102.Helper;

namespace AI_102
{
    public static class Dependency
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            services.AddScoped<AzLanguageServiceHelper>();
            services.AddScoped<AzVisionServiceHelper>();
            services.AddScoped<AzSpeechServiceHelper>();
            services.AddScoped<AzContentSafetyServiceHelper>();
        }
    }
}
