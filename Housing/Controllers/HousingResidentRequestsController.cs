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
    public class HousingResidentRequestsController : ControllerBase
    {
        private readonly IHousingRequestsRepository<HousingResidentRequest> _residentRequests;
        private readonly IMapper _mapper;
        public HousingResidentRequestsController(IHousingRequestsRepository<HousingResidentRequest> residentRequests, IMapper mapper)
        {
            _residentRequests = residentRequests;
            _mapper = mapper;
        }
        [HttpPost("add")]
        [Authorize]
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
            if (await _residentRequests.HasEntity(requestModel)) return BadRequest("Запрос на жительство уже существует");
            var createdRequest = await _residentRequests.Create(requestModel);
            if (createdRequest != null) return Ok("Отправлен запрос на жительство");
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
            if (await _residentRequests.HasEntity(requestModel)) return Ok("Добавлен запрос");
            return NotFound();
        }
        [HttpGet("houseId={houseId}")]
        public async Task<ICollection<HousingResidentRequestDto>> GetOwnersRequests(long houseId)
        {
            return _mapper.Map<ICollection<HousingResidentRequestDto>>(await _residentRequests.GetRequests(houseId));
        }

        [HttpDelete("residentId={ownerId}/houseId={houseId}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteResidentRequest(long ownerId, long houseId)
        {
            var request = await _residentRequests.GetByIds(ownerId, houseId);
            if (request == null) return NotFound("Не найден запрос");
            if (await _residentRequests.Delete(request)) return Ok("Удален запрос");
            return BadRequest("Не удалось удалить запрос");
        }
        [HttpPost("residentId={ownerId}/houseId={houseId}/deletefromaction")]
        [Authorize]
        public async Task<IActionResult> DeleteResidentRequestFromAction(long ownerId, long houseId)
        {
            var request = await _residentRequests.GetByIds(ownerId, houseId);
            if (request == null) return RedirectToAction("ProfilePage", "HousingProfile", new { requestDeleteError = "Запрос не найден" });
            if (await _residentRequests.Delete(request)) return RedirectToAction("ProfilePage", "HousingProfile");
            return RedirectToAction("ProfilePage", "HousingProfile", new { requestDeleteError = "Не удалось удалить запрос" });
        }
        [HttpPost("residentId={residentId}/houseId={houseId}/apply")]
        [Authorize]
        public async Task<IActionResult> ApplyResidentRequest(long residentId, long houseId)
        {
            if (await _residentRequests.ApplyRequest(residentId, houseId)) return Redirect("/Housing/Houses/id=" + houseId);
            return Redirect("/Housing/Houses/id=" + houseId + "?requestError=Не удалось одобрить запрос");
        }
    }
}
