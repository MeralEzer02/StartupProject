using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StartupProject.Data;
using StartupProject.Data.DTOs;
using StartupProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IRepository<User> userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll().Where(u => !u.IsDeleted).ToList();
            var userDtos = _mapper.Map<List<UserDto>>(users);

            return Ok(new ApiResponse<List<UserDto>>
            {
                Success = true,
                Message = "Kullanıcılar başarıyla listelendi.",
                Data = userDtos
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreateDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Password))
            {
                userDto.Password = "Password.123!";
            }

            var newUser = _mapper.Map<User>(userDto);
            newUser.Id = Guid.NewGuid();
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            newUser.CreatedAt = DateTime.Now;
            newUser.LastUpdatedAt = DateTime.Now;
            newUser.IsDeleted = false;

            _userRepository.Add(newUser);
            await _unitOfWork.CommitAsync();

            var createdUserDto = _mapper.Map<UserDto>(newUser);

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "Kullanıcı başarıyla eklendi.",
                Data = createdUserDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDto userDto)
        {
            var users = _userRepository.GetAll();
            var existingUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);

            if (existingUser == null)
            {
                return NotFound(new ApiResponse<UserDto> { Success = false, Message = "Kullanıcı bulunamadı veya silinmiş." });
            }

            existingUser.Name = userDto.Name;
            existingUser.Surname = userDto.Surname;
            existingUser.Email = userDto.Email;
            existingUser.Role = userDto.Role;
            existingUser.LastUpdatedAt = DateTime.Now;

            _userRepository.Update(existingUser);
            await _unitOfWork.CommitAsync();

            return Ok(new ApiResponse<UserDto> { Success = true, Message = "Kullanıcı başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var users = _userRepository.GetAll();
            var existingUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);

            if (existingUser == null)
            {
                return NotFound(new ApiResponse<bool> { Success = false, Message = "Kullanıcı bulunamadı veya zaten silinmiş." });
            }

            if (existingUser.Role == "SuperAdmin")
            {
                return BadRequest(new ApiResponse<bool> { Success = false, Message = "SuperAdmin hesabı silinemez!" });
            }

            existingUser.IsDeleted = true;
            existingUser.LastUpdatedAt = DateTime.Now;

            _userRepository.Update(existingUser);
            await _unitOfWork.CommitAsync();

            return Ok(new ApiResponse<bool> { Success = true, Message = "Kullanıcı başarıyla silindi (Soft Delete)." });
        }
    }
}