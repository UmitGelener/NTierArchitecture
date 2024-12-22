using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.Business.Mapping.AutoMapper;
using Udemy.TodoAppNTier.Business.Services;
using Udemy.TodoAppNTier.DataAccess.Contexts;
using Udemy.TodoAppNTier.DataAccess.UnitofWork;

namespace Udemy.TodoAppNTier.Business.DependencyResolvers.Microsoft
{
	public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
            {
                opt.UseSqlServer("Server=DESKTOP-CHBBTJD;Database=TodoDb;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");
				opt.LogTo(Console.WriteLine, LogLevel.Information);
			});

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });

            var mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();
        }
    }
}
