﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepThingsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace KeepThingsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MarketplaceItemController : ControllerBase
    {
        private readonly KTDBContext _context;
        //private SqlConnectionController sql = new SqlConnectionController();

        public MarketplaceItemController(KTDBContext context)
        {
            _context = context;

            //if (_context.Marketplace_Items.Count() == 0)
            //{
            //    _context.SaveChanges();
            //}
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketplaceItem>>> GetMarketplaceItems()
        {
            return await _context.Marketplace_Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarketplaceItem>> GetMarketplaceItem(int id)
        {
            var MarketplaceItem = await _context.Marketplace_Items.FindAsync(id);

            if (MarketplaceItem == null)
            {
                return NotFound();
            }

            return MarketplaceItem;
        }
        [HttpPost]
        public async Task<ActionResult<MarketplaceItem>> PostMarketplaceItem(MarketplaceItem marketplaceItem)
        {
            _context.Marketplace_Items.Add(marketplaceItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMarketplaceItem), new { id = marketplaceItem.id }, marketplaceItem);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarketplaceItem(int id, MarketplaceItem marketplaceItem)
        {
            if (id != marketplaceItem.id)
            {
                return BadRequest();
            }

            _context.Entry(marketplaceItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var marketplaceItem = await _context.Marketplace_Items.FindAsync(id);

            if (marketplaceItem == null)
            {
                return NotFound();
            }

            _context.Marketplace_Items.Remove(marketplaceItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}