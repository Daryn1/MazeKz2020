using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Services
{
    public class HousingCommentService : ModelService<Comment>, IHousingCommentService
    {
        private readonly IHousingCommentRepository _comments;
        private readonly IHousingResidentRepository _residents;
        public HousingCommentService(IHousingCommentRepository comments, IHousingResidentRepository residents) : base(comments)
        {
            _comments = comments;
            _residents = residents;
        }
        public override async Task<Comment> Create(Comment model)
        {
            var user = await _residents.GetByOwnerId(model.UserId);
            model.UserId = user.Id;
            model.LeavedAt = DateTime.Now;
            return await base.Create(model);
        }
        public async Task<ICollection<Comment>> GetCommentsForHouse(long id)
        {
            if (id < 0) return null;
            return await _comments.GetCommentsForHouse(id);
        }
    }
}
