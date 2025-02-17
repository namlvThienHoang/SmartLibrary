using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.LibrarySetting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface ILibrarySettingService
    {
        Task<List<LibrarySettingViewModel>> GetAllAsync();
        Task<LibrarySettingViewModel> GetByIdAsync(int id);
        Task AddAsync(LibrarySettingViewModel model);
        Task UpdateAsync(LibrarySettingViewModel model);
        Task DeleteAsync(int id);
    }

}
