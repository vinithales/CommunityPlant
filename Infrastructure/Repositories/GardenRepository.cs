using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPlant.Domain.Entities;
using CommunityPlant.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityPlant.Infrastructure.Repositories
{
    public class GardenRepository : IGardenRepository
    {
        private readonly AppDbContext _context;

        public GardenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Garden> CreateGardenAsync(Garden garden)
        {
            _context.Gardens.Add(garden);
            await _context.SaveChangesAsync();
            return garden;
        }

        public async Task<Garden?> GetGardenByIdAsync(int id)
        {
            return await _context.Gardens
                .AsSplitQuery()
                .Include(g => g.CreatedByUser)
                .Include(g => g.Tasks)
                .Include(g => g.Participations)
                .ThenInclude(p => p.User)
                .Include(g => g.PlantedCrops)
                .ThenInclude(pc => pc.Plant)
                .FirstOrDefaultAsync(g => g.Id == id && g.IsActive);
        }

        public async Task<List<Garden>> GetAllGardensAsync()
        {
            return await _context.Gardens
                .AsNoTracking()
                .Include(g => g.CreatedByUser)
                .Include(g => g.Participations)
                .Where(g => g.IsActive)
                .OrderBy(g => g.Name)
                .ToListAsync();
        }

        public async Task<List<Garden>> GetPublicGardensAsync()
        {
            return await _context.Gardens
                .AsNoTracking()
                .Include(g => g.CreatedByUser)
                .Include(g => g.Participations)
                .Where(g => g.IsActive && g.IsPublic)
                .OrderBy(g => g.Name)
                .ToListAsync();
        }

        public async Task<List<Garden>> GetGardensByUserAsync(int userId)
        {
            return await _context.Gardens
                .AsNoTracking()
                .Include(g => g.CreatedByUser)
                .Include(g => g.Participations)
                .Where(g => g.IsActive &&
                           (g.CreatedByUserId == userId ||
                            g.Participations.Any(p => p.UserId == userId && p.IsActive)))
                .OrderBy(g => g.Name)
                .ToListAsync();
        }

        public async Task<Garden> UpdateGardenAsync(Garden garden)
        {
            _context.Gardens.Update(garden);
            await _context.SaveChangesAsync();
            return garden;
        }

        public async Task<bool> DeleteGardenAsync(int id)
        {
            var garden = await _context.Gardens.FindAsync(id);
            if (garden == null) return false;

            garden.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Gardens
                .AnyAsync(g => g.Id == id && g.IsActive);
        }
    }
}