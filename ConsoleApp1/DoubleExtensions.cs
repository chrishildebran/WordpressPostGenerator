// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// Company:............. J.H. Kelly
// Department:.......... BIM/VC
// Website:............. http://www.jhkelly.com
// Repository:.......... https://github.com/jhkweb/VCS-Kelly-Tools-For-Revit
// Solution:............ ConsoleApp1
// Project:............. ConsoleApp1
// File:................ DoubleExtensions.cs
// Edited By:........... Chris Hildebran ✓✓
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

using System;

namespace ConsoleApp1
{
	public static class DoubleExtensions
	{
		#region Methods (Non-Private)

		public static double ToDegrees(this double d)
		{
			return d * (180 / Math.PI); // convert radians to degrees
		}

		public static double ToRadians(this double d)
		{
			return d * (Math.PI / 180); // convert degrees to radians
		}

		#endregion
	}
}