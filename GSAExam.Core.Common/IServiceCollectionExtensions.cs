using AutoMapper;
using GSAExam.Core.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace GSAExam.Core.Common
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
           
            services.AddMediatR(Assembly.GetExecutingAssembly()); 
            return services;
        }
    }
}
