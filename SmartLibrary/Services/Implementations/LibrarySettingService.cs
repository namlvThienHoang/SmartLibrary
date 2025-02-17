using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.LibrarySetting;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Implementations
{
    public class LibrarySettingService : ILibrarySettingService
    {
        private readonly ILibrarySettingRepository _repository;
        private readonly IMapper _mapper;

        public LibrarySettingService(ILibrarySettingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LibrarySettingViewModel>> GetAllAsync()
        {
            var settings = await _repository.GetAllAsync();
            return _mapper.Map<List<LibrarySettingViewModel>>(settings);
        }

        public async Task<LibrarySettingViewModel> GetByIdAsync(int id)
        {
            var setting = await _repository.GetByIdAsync(id);
            if (setting == null) return null;

            return _mapper.Map<LibrarySettingViewModel>(setting);
        }

        public async Task AddAsync(LibrarySettingViewModel model)
        {
            var setting = _mapper.Map<LibrarySetting>(model);
            await _repository.AddAsync(setting);
        }

        public async Task UpdateAsync(LibrarySettingViewModel model)
        {
            var setting = await _repository.GetByIdAsync(model.LibrarySettingId);
            if (setting == null) return;

            setting.SettingKey = model.SettingKey;
            setting.SettingValue = model.SettingValue;
            setting.Description = model.Description;
            setting.DataType = model.DataType;

            await _repository.UpdateAsync(setting);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}