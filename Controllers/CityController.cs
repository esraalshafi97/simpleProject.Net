using System;
using cityInfo.api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace cityInfo.api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CityController: ControllerBase
	{
		public CityController()
		{
		}
		[HttpGet("getCitiies")]
		public ActionResult<IEnumerable<CitiesDataStore>> GetCities()
		{
			var result= new JsonResult(
                CitiesDataStore.current.cityDtos
                );
		
			return Ok(result);
		}

		[HttpGet("{id}")]
		public ActionResult<CityDto> getCity(int id)
		{
			var result = CitiesDataStore.current.cityDtos.FirstOrDefault(e => e.Id == id);


            if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}
	}
}

