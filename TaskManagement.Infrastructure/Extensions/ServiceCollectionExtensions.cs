﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Infrastructure.Persistence.Repositories;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2) Repository kaydı
            services.AddScoped<ITaskRepository, EfTaskRepository>();
            services.AddScoped<ITaskAssignmentRepository, EfTaskAssignmentRepository>();
            services.AddScoped<IAppUserRepository, EfAppUserRepository>();

            return services;
        }
    }
}
