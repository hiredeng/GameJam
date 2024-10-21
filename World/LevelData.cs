using System;
using System.Collections.Generic;

namespace ProjectName.World
{
    [Serializable]
    public class LevelData
    {
        public int LevelID;
        public List<string> SegmentGuids;
    }
}