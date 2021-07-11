using System;
using System.Reflection;
using FluentValidation;
using ItemTrader.Application.Common.Mappings;
using ItemTrader.Application.Common.PipelineBehaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ItemTrader.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(mc => mc.AddProfile(new MappingProfile()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SetOwnerBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateRequestBehaviour<,>));

            return services;
        }
    }
}
