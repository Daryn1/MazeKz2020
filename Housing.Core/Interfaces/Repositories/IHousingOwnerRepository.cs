using Housing.Core.DTOs;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IHousingOwnerRepository : IModelRepository<HousingOwner, HousingOwnerDto>
    {
    }
}
