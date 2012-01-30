using System.Collections.Generic;

namespace railway
{
	/// <summary>
	/// Contains information about lines between railway stations
	/// </summary>
	internal struct Line : IEqualityComparer<Line>
	{
		public uint
			StationID1,
			StationID2,
			Length;

		/// <summary>
		/// Creates new line instance
		/// </summary>
		/// <param name="id1">First station ID</param>
		/// <param name="id2">Second station ID</param>
		/// <param name="length">Line length</param>
		/// <remarks>Stations' id order has no matter</remarks>
		public Line(uint id1, uint id2, uint length)
		{
			StationID1 = id1;
			StationID2 = id2;
			Length = length;
		}

		public bool Equals(Line x, Line y)
		{
			return x.StationID1 == y.StationID1 && x.StationID2 == y.StationID2 ||
				x.StationID1 == y.StationID2 && x.StationID2 == y.StationID1;
		}

		public int GetHashCode(Line obj)
		{
			return (int)(StationID1 ^ StationID2 ^ Length);
		}
	}
}
