using AutoMapper;
using Carleton.API.Models;
using Carleton.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICarletonInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICarletonInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? 
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            //Get the cities using Entity returns IEnumerable List of Entity City
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();
            //Translate above to CityWithoutPointsOfInterestDto Model
            var cityWithoutPointsOfInterestModels = Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
            //return it
            return cityWithoutPointsOfInterestModels;
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(
            int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}
