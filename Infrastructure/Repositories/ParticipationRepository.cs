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
    public class ParticipationRepository : IParticipationRepository
    {
        private readonly AppDbContext _context;

        public ParticipationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Participation> CreateParticipationAsync(Participation participation)
        {
            _context.Participations.Add(participation);
            await _context.SaveChangesAsync();
            return participation;
        }

        public async Task<Participation?> GetParticipationByIdAsync(int id)
        {
            return await _context.Participations
                .Include(p => p.User)
                .Include(p => p.Garden)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Participation>> GetParticipationsByGardenAsync(int gardenId)
        {
            return await _context.Participations
                .AsNoTracking()
                .Include(p => p.User)
                .Where(p => p.GardenId == gardenId && p.IsActive)
                .OrderBy(p => p.JoinedDate)
                .ToListAsync();
        }

        public async Task<List<Participation>> GetParticipationsByUserAsync(int userId)
        {
            return await _context.Participations
                .AsNoTracking()
                .Include(p => p.Garden)
                .Where(p => p.UserId == userId && p.IsActive)
                .OrderBy(p => p.JoinedDate)
                .ToListAsync();
        }

        public async Task<Participation?> GetActiveParticipationAsync(int userId, int gardenId)
        {
            return await _context.Participations
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Garden)
                .FirstOrDefaultAsync(p => p.UserId == userId &&
                                        p.GardenId == gardenId &&
                                        p.IsActive);
        }

        public async Task<Participation> UpdateParticipationAsync(Participation participation)
        {
            _context.Participations.Update(participation);
            await _context.SaveChangesAsync();
            return participation;
        }

        public async Task<bool> DeleteParticipationAsync(int id)
        {
            var participation = await _context.Participations.FindAsync(id);
            if (participation == null) return false;

            participation.IsActive = false;
            participation.LeftDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserParticipatesInGardenAsync(int userId, int gardenId)
        {
            return await _context.Participations
                .AnyAsync(p => p.UserId == userId &&
                             p.GardenId == gardenId &&
                             p.IsActive);
        }
    }
}
