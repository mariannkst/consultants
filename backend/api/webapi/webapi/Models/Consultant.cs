using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Consultant
    {
		public int ConsultantId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int CityId { get; set; }

		public int HourlyRateShortProject { get; set; }

		public int HourlyRateMediumProject { get; set; }

		public int HourlyRateLongProject { get; set; }

		public string ProfilePicture { get; set; }

	}
}
