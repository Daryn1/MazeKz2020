﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Repositories
{
    public class CartHouseRepository : ModelRepository<CartHouse>, ICartHouseRepository
    {
        private readonly ModelContext _context;
        public CartHouseRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CartHouse> GetFromCartByIds(long ownerId, long houseId)
        {
            return await _context.HouseCarts.Include(c => c.House).FirstOrDefaultAsync(c => c.OwnerId == ownerId
            && c.HouseId == houseId);
        }
        /*public async Task<bool> DeleteFromCart(long ownerId, long houseId)
        {
            var cartHouse = await _context.HouseCarts.FirstOrDefaultAsync(c => c.OwnerId == ownerId && c.HouseId == houseId);
            _context.HouseCarts.Remove(cartHouse);
            return await _context.SaveChangesAsync() > 0;
        }*/

        /*public async Task<bool> HasHouseInCart(long ownerId, long houseId)
        {
            return await _context.HouseCarts.AnyAsync(c => c.OwnerId == ownerId && c.HouseId == houseId);
        }*/
        public override async Task<bool> HasEntity(CartHouse model)
        {
            return await _context.HouseCarts.AnyAsync(c => c.OwnerId == model.OwnerId && c.HouseId == model.HouseId);
        }
    }
}
