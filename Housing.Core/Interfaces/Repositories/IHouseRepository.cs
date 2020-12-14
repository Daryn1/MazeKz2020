using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Models;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IHouseRepository : IModelRepository<House, HouseDto>
    {
        Task<bool> UpdateName(House model, string name);
        Task<bool> UpdateStreet(House model, string street);
        Task<bool> UpdateInfo(House model, string info);
        Task<bool> UpdatePrice(House model, double price);
        Task<bool> UpdateStatus(House model, bool isBought);
    }
}