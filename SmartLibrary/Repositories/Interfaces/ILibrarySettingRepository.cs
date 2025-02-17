using SmartLibrary.Models.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface ILibrarySettingRepository
    {
        Task<List<LibrarySetting>> GetAllAsync();
        Task<LibrarySetting> GetByIdAsync(int id);
        Task AddAsync(LibrarySetting setting);
        Task UpdateAsync(LibrarySetting setting);
        Task DeleteAsync(int id);
    }

}