using Housing.Core.DTOs;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Repositories
{
    public interface ICommentRepository : IModelRepository<Comment>
    {
        Task<ICollection<Comment>> GetCommentsForHouse(long id);
    }
}
