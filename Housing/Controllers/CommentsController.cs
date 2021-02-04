using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    [Controller]
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IHousingCommentService _comments;
        private readonly IMapper _mapper;
        public CommentsController(IMapper mapper, IHousingCommentService comments)
        {
            _comments = comments;
            _mapper = mapper;
        }

        public async Task<IActionResult> AddComment(CommentDto comment)
        {
            if (!ModelState.IsValid) return Redirect("/Housing/Houses/id=" + comment.HouseId + "?commentError=Заполните поле");
            var commentModel = _mapper.Map<Comment>(comment);
            if (await _comments.Create(commentModel) != null) return Redirect("/Housing/Houses/id=" + comment.HouseId);
            return Redirect("/Housing/Houses/id=" + comment.HouseId + "?commentError=Не удалось добавить комментарий");
        }
        [HttpGet("/Comments/houseId={houseId}")]
        [AllowAnonymous]
        public async Task<ICollection<CommentDto>> GetCommentsForHouse(long houseId)
        {
            var comments = await _comments.GetCommentsForHouse(houseId);
            if (comments == null) return null;
            return _mapper.Map<ICollection<CommentDto>>(comments);
        }
    }
}
