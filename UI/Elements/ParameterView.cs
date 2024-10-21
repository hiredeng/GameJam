using Pripizden.Gameplay.Parameter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pripizden
{
    public class ParameterView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private string _paramName;
        [SerializeField]
        private ParameterContainer _container;

        [SerializeField]
        private float _min = 0f;
        [SerializeField]
        private float _max = 1f;

        private ParameterValue _target;

        private void OnValidate()
        {
            _min = Mathf.Min(_min, _max);
            _max = Mathf.Max(_min, _max);
        }

        public void Init(ParameterContainer parameterContainer)
        {
            enabled = false;
            _container = parameterContainer;
            _container.Restarted += this.Restart;
        }

        void Restart()
        {
            enabled = true;
            _target = _container.GetParameter(_paramName);
        }

        float InvLerp(float a, float b, float v)
        {
            return (v - a) / (b - a);
        }

        private void Update()
        {
            if (_image != null)
                _image.fillAmount = InvLerp(_min, _max, _target.Value);
        }
    }
}