using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.World
{
    [CreateAssetMenu(menuName = "__Deadline/StaticData/World")]
    [Serializable]
    public class WorldStaticData : ScriptableObject
    {
        public List<WorldSegment> Data;
    }
}