using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Services
{
    public class HouseService : ModelService<House>, IHouseService
    {
        private readonly IHouseRepository _houses;
        public HouseService(IHouseRepository repos) : base(repos)
        {
            _houses = repos;
        }
        public async Task<ICollection<House>> GetFilteredHouses(FilteredHouseDto house)
        {
            if (house.HasAllDefaultValues())
            {
                return await GetAll();
            }
            return await _houses.GetFilteredHouses(house);
        }

        public async Task<ICollection<House>> GetHousesByPage(int page, int countPerPage)
        {
            return await _houses.GetHousesByPage(page, countPerPage);
        }

        public async Task<int> GetHousesCount()
        {
            return await _houses.GetHousesCount();
        }

        public async Task<double> GetMaxHousePrice()
        {
            return await _houses.GetMaxHousePrice();
        }

        public async Task<double> GetMinHousePrice()
        {
            return await _houses.GetMinHousePrice();
        }
    }
}
