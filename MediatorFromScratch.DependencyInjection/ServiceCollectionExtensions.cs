using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime lifetime, params Type[] markers)
        {
            var handlerInfo = new Dictionary<Type, Type>();

            foreach (var marker in markers)
            {
                var assembly = marker.Assembly;
                var requests = GetClassesImplementingInterface(assembly, typeof(IRequest<>));
                var handlers = GetClassesImplementingInterface(assembly, typeof(IHandler<,>));

                requests.ForEach(x =>
                {
                    handlerInfo[x] = handlers.SingleOrDefault(xx => x == xx.GetInterface("IHandler`2")!.GetGenericArguments()[0]);
                });
                var serviceDescriptors = handlers.Select(x => new ServiceDescriptor(x, x, lifetime));
                services.TryAdd(serviceDescriptors);
            }

            services.AddSingleton<IMediator>(x => new Mediator(x.GetRequiredService, handlerInfo));
            return services;
        }
        /// <summary>
        /// All implementations are automatically searched using Reflection
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeToMatch"></param>
        /// <returns></returns>
        private static List<Type> GetClassesImplementingInterface(Assembly assembly, Type typeToMatch)
        {
            return assembly.ExportedTypes
                            .Where(type =>
                            {
                                var genericInterfaceTypes = type.GetInterfaces().Where(x => x.IsGenericType).ToList();
                                var implementRequestType = genericInterfaceTypes.Any(x => x.GetGenericTypeDefinition() == typeToMatch);
                                return !type.IsInterface && !type.IsAbstract && implementRequestType;
                            }).ToList();
        }
    }
}
