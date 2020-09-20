using KBSTestTask.Server.Data;
using KBSTestTask.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KBSTestTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly KBSTestContext _context;

        public CityController(KBSTestContext context)
        {
            _context = context;
            SeedDatabase();
        }

        [HttpGet("list")]
        public async Task<ActionResult<IList<City>>> List()
        {
            return await _context.Cities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> Item(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            city.Id = Guid.NewGuid();
            _context.Add(city);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CityExists(city.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [Route("edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CityExists(Guid id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }

        private void SeedDatabase()
        {
            if(!_context.Cities.Any())
            {
                _context.Cities.Add(new City()
                {
                    Id = Guid.NewGuid(),
                    Name = "Екатеринбург",
                    CitizensCount = 1493749,
                    FoundationDate = new DateTime(1723, 11, 18)
                });

                _context.Cities.Add(new City()
                {
                    Id = Guid.NewGuid(),
                    Name = "Челябинск",
                    CitizensCount = 1196680,
                    FoundationDate = new DateTime(1736, 9, 13)
                });

                _context.SaveChanges();
            }
        }
    }
}
