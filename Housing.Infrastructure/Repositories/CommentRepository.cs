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
    public class CommentRepository : ModelRepository<Comment, CommentDto>, ICommentRepository
    {
        public CommentRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<Comment> GetById(long id)
        {
           return await Context.HouseAdvertisementComments.AsNoTracking().Include(c => c.User).
                FirstOrDefaultAsync(c => c.CommentId == id);
        }

        public override async Task<ICollection<CommentDto>> GetAll()
        {
            return await Context.HouseAdvertisementComments.AsNoTracking().Select(c => Mapper.Map<CommentDto>(c)).ToListAsync();
        }

        public async Task<ICollection<CommentDto>> GetCommentsForHouse(long id)
        {
            return await Context.HouseAdvertisementComments.AsNoTracking().
                Where(c => c.HouseId == id).Include(c => c.User).ThenInclude(u => u.Owner).ThenInclude(o => o.User).
                Select(c => Mapper.Map<CommentDto>(c)).ToListAsync();
        }
    }
}
