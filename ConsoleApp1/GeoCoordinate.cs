// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// Company:............. J.H. Kelly
// Department:.......... BIM/VC
// Website:............. http://www.jhkelly.com
// Repository:.......... https://github.com/jhkweb/VCS-Kelly-Tools-For-Revit
// Solution:............ ConsoleApp1
// Project:............. ConsoleApp1
// File:................ GeoCoordinate.cs
// Edited By:........... Chris Hildebran ✓✓
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

namespace ConsoleApp1
{
	public struct GeoCoordinate
	{
		#region Constructors (All)

		public GeoCoordinate(double latitude, double longitude)
		{
			this.Latitude  = latitude;
			this.Longitude = longitude;
			this.Name      = "Unknown";
		}

		public GeoCoordinate(double latitude, double longitude, string name)
		{
			this.Latitude  = latitude;
			this.Longitude = longitude;
			this.Name      = name;
		}

		#endregion

		#region Properties (Non-Private)

		public double Latitude{get;}

		public double Longitude{get;}

		public string Name{get;}

		#endregion
	}
}