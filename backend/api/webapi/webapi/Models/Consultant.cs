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

		public string CityName { get; set; }

		public string StateAbbreviation { get; set; }

		public string CountryAbbreviation { get; set; }

		public int HourlyRateShortProject { get; set; }

		public int HourlyRateMediumProject { get; set; }

		public int HourlyRateLongProject { get; set; }

		public string ProfilePicture { get; set; }

		public List<string> Titles { get; set; }
		public List<WorkExperienceDetails> WorkExperience { get; set; }



    }

	public class WorkExperienceDetails
	{
		public string JobTitle { get; set; }

		public string CompanyName { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Description { get; set; }

		public string IndustryName { get; set; }
	}
}
