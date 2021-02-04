using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Services
{
    public interface IHousingCommentService : IModelService<Comment>
    {
        Task<ICollection<Comment>> GetCommentsForHouse(long id);
    }
}
