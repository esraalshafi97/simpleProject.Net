using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cityInfo.api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cityInfo.api.Controllers
{
    [ApiController]
    [Route("city/{cityId}/PointsOfInterest")]
    public class PointsOfInterestController : ControllerBase
    {
        readonly ILogger<PointsOfInterestController> _logger;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<List<PointsOfDto>> getPointsOfInterest(int cityId)
        {
            var result = CitiesDataStore.current.cityDtos.FirstOrDefault((e) => e.Id == cityId);
            //Console.WriteLine("result" );
            if (result == null)
            {
                _logger.LogInformation($"not Found the city id of {cityId}");
                //Console.WriteLine("NotFound");

                return NotFound();

            }
            return Ok(result.PointsOfInterest);

        }

        [HttpGet("{id}",Name = "GetPointOfInterest"), ]
        public ActionResult<PointsOfDto> getPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.current.cityDtos.FirstOrDefault((e) => e.Id == cityId);
            if (city == null)
            {
                return NotFound();

            }

            var result = city.PointsOfInterest.FirstOrDefault((e) => e.Id == id);
            if (result == null)
            {
                return NotFound();

            }
            return Ok(result);

        }

        [HttpPost]
        public ActionResult<PointsOfDto> AddPointOfInterest(int cityId, PointsOfInterestOfCreation data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.current.cityDtos.FirstOrDefault((e) => e.Id == cityId);
            if (city == null)
            {
                return NotFound();

            }
            int maximamNumber = CitiesDataStore.current.cityDtos.SelectMany(e => e.PointsOfInterest).Max(e => e.Id);
            PointsOfDto pointsOfInterest = new PointsOfDto()
            {
                Id=++maximamNumber,
                Name=data.Name,
                Description=data.Description
            };
            city.PointsOfInterest.Add(pointsOfInterest);
           
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId=cityId,
                    id= pointsOfInterest.Id
                }
                , pointsOfInterest);

        }


        [HttpPut("{id}")]
        public ActionResult UpdatePointOfInterest(int cityId, int id, PointsOfInterestOfCreation pointsOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city=CitiesDataStore.current.cityDtos.FirstOrDefault(e => e.Id == cityId);
            if (city==null)
            {
                return BadRequest();
            }
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(e=> e.Id==id);
            pointOfInterest.Name = pointsOfInterest.Name;
            pointOfInterest.Description = pointsOfInterest.Description;

            return NoContent();
        }


        [HttpPatch("{id}")]
        public ActionResult PartialyUpdatePointOfInterest(
            int cityId, int id,
            JsonPatchDocument<PointsOfInterestOfCreation> patchDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var city = CitiesDataStore.current.cityDtos.FirstOrDefault(e => e.Id == cityId);
            if (city == null)
            {
                return BadRequest(ModelState);
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(e => e.Id == id);

            if (pointOfInterest == null)
            {
                return BadRequest(ModelState);
            }
            var toPatch = new PointsOfInterestOfCreation() {
                Name= pointOfInterest.Name,
                Description= pointOfInterest.Description };

            patchDocument.ApplyTo(toPatch,ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(toPatch))
            {
                return BadRequest(ModelState);
            }
            pointOfInterest.Name = toPatch.Name;
            pointOfInterest.Description = toPatch.Description;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var city = CitiesDataStore.current.cityDtos.FirstOrDefault(e => e.Id == cityId);
            if (city == null)
            {
                return BadRequest(ModelState);
            }
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(e => e.Id == id);
            if (pointOfInterest == null)
            {
                return BadRequest(ModelState);
            }
            city.PointsOfInterest.Remove(pointOfInterest);

            return NoContent();
        }

    }

    

}

