using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Helpers;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
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
            services.AddScoped<IHousingResidentRepository, HousingResidentRepository>();
            services.AddScoped<IHousingOwnerRepository, HousingOwnerRepository>();
            services.AddScoped<ICitizenUserRepository, CitizenUserRepository>();
            services.AddScoped<ICartHouseRepository, CartHouseRepository>();
            services.AddScoped<IHousingRequestsRepository<HousingOwnerRequest, HousingOwnerRequestDto>,
                HousingOwnerRequestsRepository>();
            services.AddScoped<IHousingRequestsRepository<HousingResidentRequest, HousingResidentRequestDto>,
                HousingResidentRequestsRepository>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperProfiles()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
