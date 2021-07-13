using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ItemTrader.Api.Swagger
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize =
                context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                if (!operation.Responses.ContainsKey("200"))
                    operation.Responses.Add("200", new OpenApiResponse{Description = "OK"});
                if (!operation.Responses.ContainsKey("201"))
                    operation.Responses.Add("201", new OpenApiResponse { Description = "Created" });
                if (!operation.Responses.ContainsKey("204"))
                    operation.Responses.Add("204", new OpenApiResponse { Description = "NoContent" });
                if (!operation.Responses.ContainsKey("401"))
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                if (!operation.Responses.ContainsKey("404"))
                    operation.Responses.Add("404", new OpenApiResponse { Description = "NotFound" });


                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme {Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"}
                            }
                        ] = new[] {"ItemTraderAPI"}
                    }
                };

            }
        }
    }
}
