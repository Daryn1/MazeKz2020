using Housing.Core.DTOs;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Services
{
    public interface IHouseService : IModelService<House>
    {
        Task<ICollection<House>> GetFilteredHouses(FilteredHouseDto house);
        Task<double> GetMaxHousePrice();
        Task<double> GetMinHousePrice();
        Task<ICollection<House>> GetHousesByPage(int page, int countPerPage);
        Task<int> GetHousesCount();
    }
}
