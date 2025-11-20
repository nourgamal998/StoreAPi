namespace StoreAPi.Extentions
{
    public static class SwaggerMiddleware
    {
        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
