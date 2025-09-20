﻿using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services )
        {
            Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            Services.AddScoped<IServiceManger, ServiceManger>();
            return Services;
        }
    }
}
