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
    public class HousingResidentRequestsController : ControllerBase
    {
        private readonly IHousingRequestsRepository<HousingResidentRequest, HousingResidentRequestDto> _residentRequests;
        public HousingResidentRequestsController(IHousingRequestsRepository<HousingResidentRequest, HousingResidentRequestDto> residentRequests)
        {
            _residentRequests = residentRequests;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddResidentRequest(HousingResidentRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest("Заполните все поля");
            var requestModel = new HousingResidentRequest
            {
                SentAt = DateTime.Now,
                ExtraInfo = request.ExtraInfo,
                HouseId = request.HouseId,
                ResidentId = request.ResidentId
            };
            if (await _residentRequests.HasRequest(requestModel)) return BadRequest("Запрос на жительство уже существует");
            if (await _residentRequests.AddRequest(requestModel)) return Ok("Отправлен запрос на жительство");
            return BadRequest("Не удалось отправить запрос");
        }
        [HttpGet("residentId={ownerId}/houseId={houseId}")]
        public async Task<IActionResult> HasResidentRequest(long ownerId, long houseId)
        {
            var requestModel = new HousingResidentRequest
            {
                HouseId = houseId,
                ResidentId = ownerId
            };
            if (await _residentRequests.HasRequest(requestModel)) return Ok("Добавлен запрос");
            return NotFound();
        }
        [HttpGet("houseId={houseId}")]
        public async Task<ICollection<HousingResidentRequestDto>> GetOwnersRequests(long houseId)
        {
            return await _residentRequests.GetRequests(houseId);
        }

        [HttpDelete("residentId={ownerId}/houseId={houseId}/delete")]
        public async Task<IActionResult> DeleteResidentRequest(long ownerId, long houseId)
        {
            var request = await _residentRequests.GetByIds(ownerId, houseId);
            if (request == null) return NotFound("Не найден запрос");
            if (await _residentRequests.DeleteRequest(request)) return Ok("Удален запрос");
            return BadRequest("Не удалось удалить запрос");
        }
        [HttpPost("residentId={ownerId}/houseId={houseId}/deletefromaction")]
        public async Task<IActionResult> DeleteResidentRequestFromAction(long ownerId, long houseId)
        {
            var request = await _residentRequests.GetByIds(ownerId, houseId);
            if (request == null) return RedirectToAction("ProfilePage", "Profile", new { requestDeleteError = "Запрос не найден" });
            if (await _residentRequests.DeleteRequest(request)) return RedirectToAction("ProfilePage", "Profile");
            return RedirectToAction("ProfilePage", "Profile", new { requestDeleteError = "Не удалось удалить запрос" });
        }
    }
}
