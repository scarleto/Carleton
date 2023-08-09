using AutoMapper;
using Carleton.API.Models;
using Carleton.API.Services;
using Carleton.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Carleton.API.Helpers;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace CityInfo.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMailService _mailService;
        private readonly ICarletonInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public UsersController(ILogger<UsersController> logger,
            IMailService mailService,
            ICarletonInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ??
                throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("{userid}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            if (!await _cityInfoRepository.UserExistsAsync(id))
            {
                return NotFound();
            }

            var user = await _cityInfoRepository
                .GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(
            UserForCreationDto user
            )
        {
            if (await _cityInfoRepository.UserExistsAsync(user.Email))
            {
                return Conflict();
            }

            var finalUser = _mapper.Map<User>(user);

            await _cityInfoRepository.AddUserAsync(finalUser);

            await _cityInfoRepository.SaveChangesAsync();

            var createdUserToReturn =
                _mapper.Map<UserDto>(finalUser);

            return CreatedAtRoute("GetUser",
                 new
                 {
                     userId = createdUserToReturn.Id
                 },
                 createdUserToReturn);
        }

        //Update overrides the entire object. Its a full update
        [HttpPut("{userid}")]
        public async Task<ActionResult> UpdateUser(int userId,
            UserForUpdateDto user)
        {
            if (!await _cityInfoRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var userEntity = await _cityInfoRepository
                .GetUserAsync(userId);

            if (userEntity == null)
            {
                return NotFound();
            }

            user.Password = HashPassword.Create(user.Password);
            user.Email = user.Email.ToLower();

            _mapper.Map(user, userEntity);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{userid}")]
        public async Task<ActionResult> PartiallyUpdateUser(
            int userId,
            JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            if (!await _cityInfoRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var userEntity = await _cityInfoRepository
                .GetUserAsync(userId);

            if (userEntity == null)
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<UserForUpdateDto>(
                userEntity);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(userToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(userToPatch, userEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{userid}")]
        public async Task<ActionResult> DeleteUser(
    int userId)
        {
            if (!await _cityInfoRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var userEntity = await _cityInfoRepository
                .GetUserAsync(userId);

            if (userEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeleteUser(userEntity);
            await _cityInfoRepository.SaveChangesAsync();

            _mailService.Send(
                "User deleted.",
                $"User {userEntity.FirstName} {userEntity.LastName} with id {userEntity.Id} was deleted.");

            return NoContent();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetCities(
    string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }

            var (userEntities, paginationMetadata) = await _cityInfoRepository
                .GetUsersAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<UserDto>>(userEntities));
        }
    }
}
