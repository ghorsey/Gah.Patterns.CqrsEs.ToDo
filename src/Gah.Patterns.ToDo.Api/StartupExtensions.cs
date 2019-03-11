namespace Gah.Patterns.ToDo.Api
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using Swashbuckle.AspNetCore.Swagger;

    /// <summary>
    /// Class <c>StartupExtensions</c>.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Adds the swagger.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                    {
                        c.IncludeXmlComments(GetXmlCommentsPath("Gah.Patterns.ToDo.Api.xml"));
                        c.IncludeXmlComments(GetXmlCommentsPath("Gah.Patterns.ToDo.Query.Domain.xml"));
                        c.DescribeAllEnumsAsStrings();
                        c.DocumentFilter<LowercaseDocumentFilter>();
                        c.SwaggerDoc(
                            "v1",
                            new Info
                                {
                                    Version = "v1",
                                    Title = "Simple CQRS/ES project",
                                    Description = "I wanted to build a really simple demonstration of a CQRS/ES architecture",
                                    ////Contact = new Contact { Name = "Geoff Horsey", Email = "geoff.horsey@outlook.com" }
                                });
                    });

            string GetXmlCommentsPath(string xmlName)
            {
                var app = AppContext.BaseDirectory;
                return System.IO.Path.Combine(app, xmlName);
            }
        }

        /// <summary>
        /// Uses the application swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(setup => setup.RouteTemplate = "{documentName}/swagger.json");
            app.UseSwaggerUI(
                c =>
                    {
                        c.RoutePrefix = string.Empty;
                        c.SwaggerEndpoint("v1/swagger.json", "Simple CQRS/ES project");
                    });
        }
    }
}