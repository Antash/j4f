using System.Collections.Generic;

namespace railway
{
    /// <summary>
    /// Railway class.
    /// </summary>
    class Railway
    {
        private readonly Mesh _railwayMesh;
        private readonly List<uint[]> _paths;
        // K/V pairs contains (Paths station)/(Distance to it from the beginning of the path)
        private readonly List<KeyValuePair<uint, uint>[]> _pathCalculations;

        private Railway()
        {
            _railwayMesh = new Mesh();
            _paths = new List<uint[]>();
            _pathCalculations = new List<KeyValuePair<uint, uint>[]>();
        }

        public Railway(Mesh mesh)
            : this()
        {
            _railwayMesh = mesh;
        }

        /// <summary>
        /// Adds new path or nothing if new path is invalid
        /// </summary>
        /// <param name="newPath">New path to add</param>
        /// <returns>Addition success result</returns>
        public bool AddPath(uint[] newPath)
        {
            var readyToAdd = _railwayMesh.IsPathValid(newPath);
            if (readyToAdd)
            {
                _paths.Add(newPath);
                _pathCalculations.Add(_railwayMesh.CalcPath(newPath));
            }
            return readyToAdd;
        }

        /// <summary>
        /// Clears all added paths
        /// </summary>
        public void ClearPaths()
        {
            _paths.Clear();
            _pathCalculations.Clear();
        }

        /// <summary>
        /// Function to detect crash availability
        /// </summary>
        /// <returns>Returns true if there is no chash detected</returns>
        public bool IfCrashExists()
        {
            // Consider each distinct pair of paths
            for (int pi1 = 0; pi1 < _pathCalculations.Count; pi1++)
            {
                var pathCalc1 = _pathCalculations[pi1];
                for (int pi2 = pi1 + 1; pi2 < _pathCalculations.Count; pi2++)
                {
                    var pathCalc2 = _pathCalculations[pi2];
                    for (int i = 0, j = 0; i < pathCalc1.Length && j < pathCalc2.Length; )
                    {
                        // paths pass through one station
                        if (pathCalc1[i].Key == pathCalc2[j].Key)
                        {
                            // trains are on the station together - crash
                            if (pathCalc1[i].Value == pathCalc2[j].Value)
                                return true;
                            // check for the crash on the same line
                            if (Comp(pathCalc1, i, pathCalc2, j) ||
                                Comp(pathCalc2, j, pathCalc1, i))
                                return true;
                        }

                        // extend the path that is shorter
                        if (pathCalc1[i].Value <= pathCalc2[j].Value)
                            i++;
                        else
                            j++;
                    }
                }
            }

            return false;
        }

        private static bool Comp(IList<KeyValuePair<uint, uint>> pathCalc1, int i, IList<KeyValuePair<uint, uint>> pathCalc2, int j)
        {
            return
                // changed indexes must not be out of range
                i + 1 < pathCalc1.Count && j - 1 >= 0 &&
                // previous station of path 1 must be a next station of path2
                pathCalc1[i + 1].Key == pathCalc2[j - 1].Key &&
                // the first train must reach station before the second leaves it 
                pathCalc1[i].Value <= pathCalc2[j].Value;
        }
    }
}
