using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StartupProject.Data;
using StartupProject.Data.DTOs;
using StartupProject.Data.Repositories;
using System;
using System.Collections.Generic;
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
            var users = _userRepository.GetAll();

            var userDtos = _mapper.Map<List<UserDto>>(users);

            return Ok(userDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreateDto userDto)
        {
            var newUser = _mapper.Map<User>(userDto);

            newUser.Id = Guid.NewGuid();
            newUser.PasswordHash = userDto.Password; 
            newUser.CreatedAt = DateTime.Now;
            newUser.LastUpdatedAt = DateTime.Now;

            _userRepository.Add(newUser);
            await _unitOfWork.CommitAsync();

            return Ok(new { Message = "Kullanıcı başarıyla eklendi.", UserId = newUser.Id });
        }
    }
}