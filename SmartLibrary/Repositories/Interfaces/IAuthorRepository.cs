﻿using SmartLibrary.Models.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<(IEnumerable<Author> Authors, int TotalCount)> GetAuthorsAsync(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<Author>> GetAllAuthorAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}