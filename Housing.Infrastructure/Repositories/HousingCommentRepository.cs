using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Repositories
{
    public class HousingCommentRepository : ModelRepository<Comment>, IHousingCommentRepository
    {
        public HousingCommentRepository(HousingContext context) : base(context)
        {
        }

        public async Task<ICollection<Comment>> GetCommentsForHouse(long id)
        {
            return await Context.HouseAdvertisementComments.
                Where(c => c.HouseId == id).
                OrderByDescending(o => o.LeavedAt).
                ToListAsync();
        }
    }
}
