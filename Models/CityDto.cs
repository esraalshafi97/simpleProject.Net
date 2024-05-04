using System;
namespace cityInfo.api.Models
{
	public class CityDto
	{
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        int NumOfPointsOfInterest
        {
            get {
                return

                    PointsOfInterest.Count;
                    }
        }


        public ICollection<PointsOfDto> PointsOfInterest { get; set; } = new List<PointsOfDto>();



    }
}

