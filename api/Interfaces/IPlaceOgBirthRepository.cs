using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.PlaceOfBirth;
using api.Models;

namespace api.Interfaces
{
    public interface IPlaceOgBirthRepository
    {
        Task<List<PlaceOfBirth>> GetAllAsync();
        Task<PlaceOfBirth?> GetByIdAsync(int id);
        Task<PlaceOfBirth> CreateAsync(PlaceOfBirth placeOfBirth);
        Task<bool> PlaceOfBirthExist(int id);
        Task<PlaceOfBirth?> UpdateAsync(int userId, UpdatePlaceOfBirthRequestDto placeOfBirth);
    }
}