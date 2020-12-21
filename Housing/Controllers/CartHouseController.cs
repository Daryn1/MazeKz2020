using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    [Route("{controller}/")]
    public class CartHouseController : Controller
    {
        private readonly ICartHouseRepository _carts;
        public CartHouseController(ICartHouseRepository carts)
        {
            _carts = carts;
        }
        [HttpPost("ownerId={ownerId}/houseId={houseId}/add")]
        public async Task<IActionResult> AddHouseToCart(long ownerId, long houseId)
        {
            if (await _carts.AddToCart(ownerId, houseId)) return Redirect("/Housing/Houses/id=" + houseId);
            return Redirect("/Housing/Houses/id=" + houseId + "?cartError=Не удалось добавить в избранное");
        }
        [HttpPost("ownerId={ownerId}/houseId={houseId}/delete")]
        public async Task<IActionResult> DeleteHouseToCart(long ownerId, long houseId)
        {
            if (await _carts.DeleteFromCart(ownerId, houseId)) return Redirect("/Housing/Houses/id=" + houseId);
            return Redirect("/Housing/Houses/id=" + houseId + "?cartError=Не удалось удалить из избранного");
        }

        [HttpGet("ownerId={ownerId}/houseId={houseId}")]
        public async Task<IActionResult> HasHouseInCart(long ownerId, long houseId)
        {
            if (await _carts.HasHouseInCart(ownerId, houseId)) return Ok();
            return NotFound();
        }
    }
}
