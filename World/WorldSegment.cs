using ProjectName.Services.Timing;
using System;
using UnityEngine;
using VContainer;

namespace ProjectName.World
{
    public class WorldSegment : MonoBehaviour
    {
        [SerializeField]
        public string GUID = "";
        [SerializeField]
        private float _width = 9.6f;
        public float Width { get => _width; }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(WorldSegment))]
    public class WorldSegmentEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("GenerateGUID"))
            {
                ((WorldSegment)target).GUID = Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty((WorldSegment)target);
            }
        }
    }
#endif
}