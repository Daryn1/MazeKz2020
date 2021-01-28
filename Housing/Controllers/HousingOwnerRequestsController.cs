using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class HousingOwnerRequestsController : ControllerBase
    {
        private readonly IHousingRequestsRepository<HousingOwnerRequest> _ownerRequests;
        private readonly IMapper _mapper;
        public HousingOwnerRequestsController(IHousingRequestsRepository<HousingOwnerRequest> ownerRequests, IMapper mapper)
        {
            _ownerRequests = ownerRequests;
            _mapper = mapper;
        }
        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddOwnerRequest(HousingOwnerRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Заполните все поля");
            var requestModel = new HousingOwnerRequest
            {
                SentAt = DateTime.Now,
                ExtraInfo = request.ExtraInfo,
                HouseId = request.HouseId,
                OwnerId = request.OwnerId
            };
            if (await _ownerRequests.HasEntity(requestModel)) return BadRequest("Запрос на покупку уже существует");
            var createdRequest = await _ownerRequests.Create(requestModel);
            if (createdRequest != null) return Ok("Отправлен запрос на покупку недвижимости");
            return BadRequest("Не удалось отправить запрос");
        }
        [HttpGet("ownerId={ownerId}/houseId={houseId}")]
        public async Task<IActionResult> HasOwnerRequest(long ownerId, long houseId)
        {
            var requestModel = new HousingOwnerRequest
            {
                HouseId = houseId,
                OwnerId = ownerId
            };
            if (await _ownerRequests.HasEntity(requestModel)) return Ok("Добавлен запрос");
            return NotFound();
        }
        [HttpGet("houseId={houseId}")]
        public async Task<ICollection<HousingOwnerRequestDto>> GetOwnersRequests(long houseId)
        {
            return _mapper.Map<ICollection<HousingOwnerRequestDto>>(await _ownerRequests.GetRequests(houseId));
        }

        [HttpDelete("ownerId={ownerId}/houseId={houseId}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteOwnerRequest(long ownerId, long houseId)
        {
            var request = await _ownerRequests.GetByIds(ownerId, houseId);
            if (request == null) return NotFound("Не найден запрос");
            if (await _ownerRequests.Delete(request)) return Ok("Удален запрос");
            return BadRequest("Не удалось удалить запрос");
        }
        [HttpPost("ownerId={ownerId}/houseId={houseId}/deletefromaction")]
        [Authorize]
        public async Task<IActionResult> DeleteOwnerRequestFromAction(long ownerId, long houseId)
        {
            var request = await _ownerRequests.GetByIds(ownerId, houseId);
            if (request == null) return RedirectToAction("ProfilePage", "Profile", new { requestDeleteError = "Запрос не найден" });
            if (await _ownerRequests.Delete(request)) return RedirectToAction("ProfilePage", "Profile");
            return RedirectToAction("ProfilePage", "Profile", new { requestDeleteError = "Не удалось удалить запрос" });
        }
        [HttpPost("ownerId={ownerId}/houseId={houseId}/apply")]
        [Authorize]
        public async Task<IActionResult> ApplyOwnerRequest(long ownerId, long houseId)
        {
            if (await _ownerRequests.ApplyRequest(ownerId, houseId)) return Redirect("/Housing/Houses/id=" + houseId);
            return Redirect("/Housing/Houses/id=" + houseId + "?requestError=Не удалось одобрить запрос");
        }
    }
}
