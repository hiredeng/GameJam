using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.Service.UI
{
    [CreateAssetMenu(menuName = "FindingRuby/Static Data/Window static data", fileName = "WindowStaticData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowData> Data;
    }
}
