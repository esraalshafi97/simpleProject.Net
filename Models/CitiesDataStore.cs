using System;
using System.Xml.Linq;

namespace cityInfo.api.Models
{
	public class CitiesDataStore
	{ 
		public List<CityDto> cityDtos { get; set; }
		public static CitiesDataStore current = new CitiesDataStore();
		public CitiesDataStore()
		{
			cityDtos = new List<CityDto>(){
				new CityDto()
				{
					Name="esra",
					Id=1,
					Description="esra city",
					PointsOfInterest = new List<PointsOfDto>(){
						new PointsOfDto()
						{
							Id=1,
							Name="esra alshafi",
							Description="any thing"
						}

                    }
				},
                new CityDto(){
                    Name="mariam",
                    Id=2,
                    Description="mariam city",
                    PointsOfInterest = new List<PointsOfDto>(){
                        new PointsOfDto()
                        {
                            Id=2,
                            Name="marima alshafi",
                            Description="any thing"
                        }

                    }
                }

            };

        }
    }
}

