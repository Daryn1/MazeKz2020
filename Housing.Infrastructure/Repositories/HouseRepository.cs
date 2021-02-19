using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Enums;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Housing.Infrastructure.Repositories
{
    public class HouseRepository : ModelRepository<House>, IHouseRepository
    {

        public HouseRepository(HousingContext context) : base(context)
        {
        }

        public override async Task<ICollection<House>> GetAll()
        {
            return await Context.Houses.
                Where(h => h.IsSelling).ToListAsync();
        }

        public async Task<ICollection<House>> GetFilteredHouses(FilteredHouseDto house)
        {
            var filteredHouses = Context.Houses.AsQueryable();
            if (!string.IsNullOrEmpty(house.Name))
            {
                filteredHouses = filteredHouses.
                    Where(h => h.Name.Contains(house.Name.ToLower())).
                    Union(filteredHouses.Where(h => h.Name.Contains(house.Name)));
            }
            else
            {
                double bound = 5000000;
                bool hasPrice = house.Price != default, hasStreet = !string.IsNullOrEmpty(house.Street),
                hasType = house.Type != HouseType.Ничего;
                if (hasPrice && hasStreet && hasType)
                {
                    filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                    (h.Price >= house.Price - bound && h.Price <= house.Price + bound) && h.Type == house.Type && h.IsSelling);
                }
                else if (hasPrice && hasType)
                {
                    filteredHouses = filteredHouses.Where(h => (h.Price >= house.Price - bound && h.Price <= house.Price + bound) &&
                    h.Type == house.Type && h.IsSelling);
                }
                else if (hasStreet && hasType)
                {
                    filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                    h.Type == house.Type && h.IsSelling);
                }
                else if (hasPrice && hasStreet)
                {
                    filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                    (h.Price >= house.Price - bound && h.Price <= house.Price + bound) && h.IsSelling);
                }
                else if (hasPrice)
                {
                    filteredHouses = filteredHouses.Where(h =>
                    (h.Price >= house.Price - bound && h.Price <= house.Price + bound) && h.IsSelling);
                }
                else if (hasStreet)
                {
                    filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) && h.IsSelling);
                }
                else if (hasType)
                {
                    filteredHouses = filteredHouses.Where(h => h.Type == house.Type && h.IsSelling);
                }
                else
                {
                    filteredHouses = filteredHouses.Where(h => h.IsSelling);
                }
            }
           return await filteredHouses.ToListAsync();
        }

        public async Task<ICollection<House>> GetHousesByName(string name)
        {
            return await Context.Houses.Where(h => h.Name == name).ToListAsync();
        }

        public async Task<ICollection<House>> GetHousesByPage(int page, int countPerPage)
        {
            return await Context.Houses.Where(h => h.IsSelling).OrderBy(h => h.HouseId).
                Skip((page - 1) * countPerPage).Take(countPerPage).ToListAsync();
        }

        public async Task<ICollection<House>> GetHousesByPrice(double price)
        {

            return await Context.Houses.Where(h => h.Price == price).ToListAsync();
        }

        public async Task<ICollection<House>> GetHousesByStreet(string street)
        {

            return await Context.Houses.Where(h => h.Street == street).ToListAsync();
        }

        public async Task<ICollection<House>> GetHousesByType(HouseType type)
        {
            return await Context.Houses.Where(h => h.Type == type).ToListAsync();
        }

        public async Task<int> GetHousesCount()
        {
            return await Context.Houses.CountAsync();
        }

        public async Task<double> GetMaxHousePrice()
        {
            return await Context.Houses.MaxAsync(h => h.Price);
        }

        public async Task<double> GetMinHousePrice()
        {
            return await Context.Houses.MinAsync(h => h.Price);
        }
    }
}