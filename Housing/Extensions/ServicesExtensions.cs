﻿using Housing.Core.Interfaces.Repositories;
using Housing.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Housing.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Добавление зависимостей репозиториев жилищной системы
        /// </summary>
        /// <param name="services"></param>
        public static void AddHousingRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IHousingUserRepository, HousingUserRepository>();
            services.AddScoped<IHousingOwnerRepository, HousingOwnerRepository>();
            services.AddScoped<ICitizenUserRepository, CitizenUserRepository>();
        }
    }
}
