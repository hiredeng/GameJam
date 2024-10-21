using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace ProjectName.Core
{
    [RequireComponent(typeof(RawImage))]
    public class ViewBlocker : MonoBehaviour
    {
        [SerializeField]
        private float _transitionDuration;

        [SerializeField]
        private string _parameterName;

        private RawImage _rawImage;

        private float _opacity;

        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            _opacity = 0f;
#if UNITY_EDITOR
            _rawImage.material.SetFloat(_parameterName, 0f);
#endif
        }

        public void BlockImmediate()
        {
            _opacity = 1f;
            _rawImage.material.SetFloat(_parameterName, _opacity);
        }

        public void Block()
        {
            StopAllCoroutines();
            StartCoroutine(BlockRoutine());
        }

        public void Unblock()
        {
            StopAllCoroutines();
            StartCoroutine(UnblockRoutine());
        }

        private IEnumerator UnblockRoutine()
        {
            float frameTime = .03f;
            while (_opacity > 0f)
            {
                _opacity -= frameTime;
                _rawImage.material.SetFloat(_parameterName, _opacity);
                yield return new WaitForSecondsRealtime(frameTime);
            }
        }

        private IEnumerator BlockRoutine()
        {
            float frameTime = .03f;
            while (_opacity < 1f)
            {
                _opacity += frameTime;
                _rawImage.material.SetFloat(_parameterName, _opacity);
                yield return new WaitForSecondsRealtime(frameTime);
            }
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(ViewBlocker))]
    public class ViewBlockerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var blocker = (ViewBlocker)target;
            if (GUILayout.Button("FadeIn"))
            {
                blocker.Block();
            }
            if (GUILayout.Button("FadeOut"))
            {
                blocker.Unblock();
            }
        }
    }

#endif
}

