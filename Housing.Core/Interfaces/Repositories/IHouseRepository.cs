using System.Collections.Generic;
using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Models;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IHouseRepository : IModelRepository<House>
    {
        Task<ICollection<House>> GetFilteredHouses(FilteredHouseDto house);
        Task<double> GetMaxHousePrice();
        Task<double> GetMinHousePrice();
    }
}