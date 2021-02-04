using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Enums;
using Housing.Core.Models;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IHouseRepository : IModelRepository<House>
    {
        Task<ICollection<House>> GetFilteredHouses(FilteredHouseDto house);
        Task<double> GetMaxHousePrice();
        Task<double> GetMinHousePrice();
        Task<ICollection<House>> GetHousesByPage(int page, int countPerPage);
        Task<int> GetHousesCount();
        Task<ICollection<House>> GetHousesByStreet(string street);
        Task<ICollection<House>> GetHousesByName(string name);
        Task<ICollection<House>> GetHousesByType(HouseType type);
        Task<ICollection<House>> GetHousesByPrice(double price);
    }
}