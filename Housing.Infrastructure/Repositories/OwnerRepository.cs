using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Repositories
{
    public class OwnerRepository : ModelRepository<Owner, OwnerDto>, IOwnerRepository
    {
        public OwnerRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<bool> UpdateBalance(Owner model, double balance)
        {
            model.Balance = balance;
            return Update(model);
        }

        public Task<bool> UpdateCurrency(Owner model, string currency)
        {
            model.Currency = currency;
            return Update(model);
        }
    }
}
