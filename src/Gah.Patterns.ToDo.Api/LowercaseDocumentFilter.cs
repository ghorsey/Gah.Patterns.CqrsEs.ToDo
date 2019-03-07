namespace Gah.Patterns.ToDo.Api
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Lowercase the routes in Swagger
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    [ExcludeFromCodeCoverage]
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Applies the specified swagger document.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="context">The context.</param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(entry => LowercaseEverythingButParameters(entry.Key), entry => entry.Value);
        }

        /// <summary>
        /// Lowercase the everything but parameters.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A lowercase string</returns>
        private static string LowercaseEverythingButParameters(string key)
        {
            return string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : x.ToLower()));
        }
    }
}