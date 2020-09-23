using KBSTestTask.Server.Data;
using KBSTestTask.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KBSTestTask.Server.Hubs
{
    public class BroadcastHub : Hub
    {
        private readonly KBSTestContext _context;

        public BroadcastHub(KBSTestContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task List()
        {
            var cities = await _context.Cities.ToListAsync();
            await Clients.Caller.SendAsync("ListHub", cities);
        }

        public async Task Item(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                await Clients.Caller.SendAsync("Error", "Данного города не существует");
                return;
            }

            await Clients.Caller.SendAsync("ItemHub", city);
        }

        public async Task Create(City city)
        {
            city.Id = Guid.NewGuid();
            _context.Add(city);

            await _context.SaveChangesAsync();

            await Clients.Others.SendAsync("Created", city);
        }

        public async Task Edit(Guid id, City city)
        {
            var oldCity = await _context.Cities.FindAsync(id);

            if (oldCity == null)
            {
                await Clients.Caller.SendAsync("Error", "Данного города не существует");
                return;
            }

            if (oldCity.Name != city.Name)
                oldCity.Name = city.Name;

            if (oldCity.FoundationDate != city.FoundationDate)
                oldCity.FoundationDate = city.FoundationDate;

            if (oldCity.CitizensCount != city.CitizensCount)
                oldCity.CitizensCount = city.CitizensCount;

            await _context.SaveChangesAsync();
            await Clients.Others.SendAsync("Updated", city);
        }

        public async Task Delete(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                await Clients.Caller.SendAsync("Error", "Данного города не существует");
                return;
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            await Clients.Others.SendAsync("Deleted", id);
        }

    }
}
