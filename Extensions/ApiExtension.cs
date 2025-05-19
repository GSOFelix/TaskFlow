using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TaskFlow.Application.UseCases.Implementations;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Configurations;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Domain.Interfaces.Services;
using TaskFlow.Infra.Context;
using TaskFlow.Infra.Repositories;
using TaskFlow.Infra.Services;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Application.Auth;
using Microsoft.OpenApi.Models;

namespace TaskFlow.Extensions
{
    public static class ApiExtension
    {
        public static IServiceCollection AddDependeceInjection(this IServiceCollection services)
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
            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpContextAccessor();

            // Injetar dependencia de UserCase
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddScoped<IMainTaskUseCase, MainTaskUseCase>();
            services.AddScoped<ITaskAssigneeUseCase, TaskAssigneeUseCase>();
            services.AddScoped<ICommentsUseCase, CommentsUseCase>();
            services.AddScoped<IAuthorizationUser,AuthorizationUser>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(ApiConfigurations.JwtKey);
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ApiConfigurations.JwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = ApiConfigurations.JwtAudience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            return services;
        }

        public static IServiceCollection AddInfraSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "TaskFlow",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;    
        }
    }
}
