using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.UseCases.Implementations;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Configurations;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Domain.Interfaces.Services;
using TaskFlow.Infra.Context;
using TaskFlow.Infra.Repositories;
using TaskFlow.Infra.Services;

namespace TaskFlow.Extensions
{
    public static class ApiExtension
    {
        public static IServiceCollection AddDependeceInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicionar configuraçao com postgres
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(ApiConfigurations.DbConnection,
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            // injetar dependencia de repositorios
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IMainTaskRepository, MainTaskRepository>();
            services.AddScoped<ISubTaskRepository, SubTaskRepository>();
            services.AddScoped<ITaskAssigneeRepository, TaskAssigneeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // injetar dependencia de serviços 
            services.AddScoped<IPassWordService, PassWordService>();

            // Injetar dependencia de UserCase
            services.AddScoped<IUserUseCase, UserUseCase>();

            return services;
        }
    }
}
