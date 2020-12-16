using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    [Route("{controller}/")]
    public class ProfileController : Controller
    {
        private readonly IHousingOwnerRepository _owners;
        private readonly IMapper _mapper;
        public ProfileController(IHousingOwnerRepository owners, IMapper mapper)
        {
            _owners = owners;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> ProfilePageAsync(string houseCreateErrorMessage)
        {
            ViewBag.ErrorMessage = houseCreateErrorMessage;
            var owner = await _owners.GetById(long.Parse(HttpContext.Session.GetString("Id")));
            return View(_mapper.Map<HousingOwnerDto>(owner));
        }
    }
}
