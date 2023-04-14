using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application
{
    public static class AutoMapperMeditRDependency
    {

        public static IServiceCollection AddAutoMapperNMediatRServices(this IServiceCollection services)
        {


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


            return services;
        }

    }
}


