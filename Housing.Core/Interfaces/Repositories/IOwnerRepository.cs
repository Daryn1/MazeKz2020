using Housing.Core.DTOs;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IOwnerRepository : IModelRepository<Owner, OwnerDto>
    {
        Task<bool> UpdateBalance(Owner model, double balance);
        Task<bool> UpdateCurrency(Owner model, string currency);
    }
}
