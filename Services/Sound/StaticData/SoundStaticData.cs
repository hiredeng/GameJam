using System.Collections.Generic;
using UnityEngine;

namespace ProjectName.Services.Sound
{

    [CreateAssetMenu(menuName = "Deadline/Static Data/Sound static data", fileName = "SoundStaticData")]
    public class SoundStaticData : ScriptableObject
    {
        public List<SoundData> Data;
    }
}