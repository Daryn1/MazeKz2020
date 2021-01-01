using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class HousingOwnerRequestsController : ControllerBase
    {
        private readonly IHousingRequestsRepository<HousingOwnerRequest, HousingOwnerRequestDto> _ownerRequests;
        public HousingOwnerRequestsController(IHousingRequestsRepository<HousingOwnerRequest, HousingOwnerRequestDto> ownerRequests)
        {
            _ownerRequests = ownerRequests;
        }
        [HttpPost("add")]
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
            if (await _ownerRequests.HasRequest(requestModel)) return BadRequest("Запрос на покупку уже существует");
            if (await _ownerRequests.AddRequest(requestModel)) return Ok("Отправлен запрос на покупку недвижимости");
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
            if (await _ownerRequests.HasRequest(requestModel)) return Ok("Добавлен запрос");
            return NotFound();
        }
        [HttpGet("houseId={houseId}")]
        public async Task<ICollection<HousingOwnerRequestDto>> GetOwnersRequests(long houseId)
        {
            return await _ownerRequests.GetRequests(houseId);
        }

        [HttpDelete("ownerId={ownerId}/houseId={houseId}/delete")]
        public async Task<IActionResult> DeleteOwnerRequest(long ownerId, long houseId)
        {
            var request = await _ownerRequests.GetByIds(ownerId, houseId);
            if (request == null) return NotFound("Не найден запрос");
            if (await _ownerRequests.DeleteRequest(request)) return Ok("Удален запрос");
            return BadRequest("Не удалось удалить запрос");
        }
        [HttpPost("ownerId={ownerId}/houseId={houseId}/deletefromaction")]
        public async Task<IActionResult> DeleteOwnerRequestFromAction(long ownerId, long houseId)
        {
            var request = await _ownerRequests.GetByIds(ownerId, houseId);
            if (request == null) return RedirectToAction("ProfilePage", "Profile", new { requestDeleteError = "Запрос не найден" });
            if (await _ownerRequests.DeleteRequest(request)) return RedirectToAction("ProfilePage", "Profile");
            return RedirectToAction("ProfilePage", "Profile", new { requestDeleteError = "Не удалось удалить запрос" });
        }
        [HttpPost("ownerId={ownerId}/houseId={houseId}/apply")]
        public async Task<IActionResult> ApplyOwnerRequest(long ownerId, long houseId)
        {
            if (await _ownerRequests.ApplyRequest(ownerId, houseId)) return Redirect("/Housing/Houses/id=" + houseId);
            return Redirect("/Housing/Houses/id=" + houseId + "?requestError=Не удалось одобрить запрос");
        }
    }
}
