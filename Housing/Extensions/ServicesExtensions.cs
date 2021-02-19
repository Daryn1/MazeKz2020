using AutoMapper;
using Housing.Core.Helpers;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using Housing.Infrastructure.Repositories;
using Housing.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Housing.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Add data repositories for housing system
        /// </summary>
        /// <param name="services"></param>
        public static void AddHousingRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IHousingCommentRepository, HousingCommentRepository>();
            services.AddScoped<IHousingResidentRepository, HousingResidentRepository>();
            services.AddScoped<IHousingOwnerRepository, HousingOwnerRepository>();
            services.AddScoped<ICitizenUserRepository, CitizenUserRepository>();
            services.AddScoped<IHousingCartsRepository, HousingCartsRepository>();
            services.AddScoped<IHousingRequestsRepository<HousingOwnerRequest>,
                HousingOwnerRequestsRepository>();
            services.AddScoped<IHousingRequestsRepository<HousingResidentRequest>,
                HousingResidentRequestsRepository>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperProfiles()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        /// <summary>
        /// Add interactor services for housing system
        /// </summary>
        /// <param name="services"></param>
        public static void AddHousingInteractors(this IServiceCollection services)
        {
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IHousingCommentService, HousingCommentService>();
            services.AddScoped<IHousingOwnerService, HousingOwnerService>();
            services.AddScoped<IHousingCartService, HousingCartService>();
            services.AddScoped<IHousingRequestService<HousingOwnerRequest>,
                HousingOwnerRequestService>();
            services.AddScoped<IHousingRequestService<HousingResidentRequest>,
                HousingResidentRequestService>();
        }
    }
}
