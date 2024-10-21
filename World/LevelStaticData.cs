using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.World
{
    [CreateAssetMenu(menuName = "__Deadline/StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public List<LevelData> Data;
    }
}