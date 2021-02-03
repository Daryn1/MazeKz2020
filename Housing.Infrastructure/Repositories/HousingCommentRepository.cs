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

        public override async Task<Comment> GetById(long id)
        {
           return await Context.HouseAdvertisementComments.
               // AsNoTracking().
               // Include(c => c.User).
                FirstOrDefaultAsync(c => c.CommentId == id);
        }

        public async Task<ICollection<Comment>> GetCommentsForHouse(long id)
        {
            return await Context.HouseAdvertisementComments.
                //AsNoTracking().
                Where(c => c.HouseId == id).
                //Include(c => c.User).ThenInclude(u => u.Owner).ThenInclude(o => o.User).
                OrderByDescending(o => o.LeavedAt).
                ToListAsync();
        }
    }
}
