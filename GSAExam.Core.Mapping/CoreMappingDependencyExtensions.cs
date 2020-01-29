using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GSAExam.Core.Mapping
{
    public static class CoreMappingDependencyExtensions
    {
        public static IServiceCollection AddDomainAutoMapper(this IServiceCollection serviceCollection)
        {
           return serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
