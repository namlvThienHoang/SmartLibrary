using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Implementations
{
    public class LibrarySettingRepository : ILibrarySettingRepository
    {
        private readonly ApplicationDbContext _context;

        public LibrarySettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LibrarySetting>> GetAllAsync()
        {
            return await _context.LibrarySettings.ToListAsync();
        }

        public async Task<LibrarySetting> GetByIdAsync(int id)
        {
            return await _context.LibrarySettings.FindAsync(id);
        }

        public async Task AddAsync(LibrarySetting setting)
        {
            _context.LibrarySettings.Add(setting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LibrarySetting setting)
        {
            _context.Entry(setting).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var setting = await _context.LibrarySettings.FindAsync(id);
            if (setting != null)
            {
                _context.LibrarySettings.Remove(setting);
                await _context.SaveChangesAsync();
            }
        }
    }

}