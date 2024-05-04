using System;
using System.ComponentModel.DataAnnotations;

namespace cityInfo.api.Models
{
	public class PointsOfInterestOfCreation
	{
        [Required(ErrorMessage ="you must provide a Name ")]
        [MaxLength(50)]
        public String Name { get; set; }
        [MaxLength(200)]
        public String Description { get; set; }

    }
}

