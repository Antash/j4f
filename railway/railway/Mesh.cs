using System;
using System.Collections.Generic;

namespace railway
{
	/// <summary>
	/// Railway mesh. Contains list of railway lines
	/// </summary>
	internal class Mesh
	{
		private readonly List<Line> _meshLines;

		public Mesh()
		{
			_meshLines = new List<Line>();
		}

		/// <summary>
		/// Addition of the new line.
		/// </summary>
		/// <param name="newLine">New railway line</param>
		/// <returns>Returns true if addition was successfully completed</returns>
        /// <remarks>You can't add degenerate line or line witch was added earlier</remarks>
		public bool Add(Line newLine)
		{
			bool readyToAdd = newLine.StationID1 != newLine.StationID2 && !_meshLines.Contains(newLine);
			if (readyToAdd)
				_meshLines.Add(newLine);
			return readyToAdd;
		}

		/// <summary>
		/// Checking mesh path
		/// </summary>
		/// <param name="path">Path to check</param>
        /// <returns>Returns true if the path is connected</returns>
		/// <remarks>If path contains only one station it is exists.</remarks>
		public bool IsPathValid(uint[] path)
		{
			var result = true;

			for (var i = 0; i < path.Length - 1; i++)
			{
				var tempMesh = _meshLines.FindAll(line => line.StationID1 == path[i] || line.StationID2 == path[i]);
				if (tempMesh.Count != 0)
					tempMesh = tempMesh.FindAll(line => line.StationID2 == path[i + 1] || line.StationID1 == path[i + 1]);
				if (tempMesh.Count == 0)
				{
					result = false;
					break;
				}
			}

			return result;
		}

		/// <summary>
		/// Counts distance to each station of the path from the beginning to end
		/// </summary>
		/// <param name="path">Path to calculate</param>
		/// <exception cref="ArgumentException">Occures if input path is invalid (empty or unconnected)</exception>
		/// <returns>K/V pairs contains Station/Distance to it</returns>
		public KeyValuePair<uint, uint>[] CalcPath(uint[] path)
		{
			if (path.Length == 0)
				throw new ArgumentException("Path contains no stations!");
			if (IsPathValid(path) == false)
				throw new ArgumentException("Path does not valid in the current mesh!");

			var result = new KeyValuePair<uint, uint>[path.Length];
			result[0] = new KeyValuePair<uint, uint>(path[0], 0);

			for (var i = 0; i < path.Length - 1; i++)
			{
				var line = _meshLines.Find(l =>
					l.StationID1 == path[i] && l.StationID2 == path[i + 1] ||
					l.StationID2 == path[i] && l.StationID1 == path[i + 1]);
				result[i + 1] = new KeyValuePair<uint, uint>(path[i + 1], result[i].Value + line.Length);
			}

			return result;
		}
	}
}
