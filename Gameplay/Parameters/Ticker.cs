using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pripizden.Gameplay.Parameter
{
    public class Ticker : MonoBehaviour
    {

        [SerializeField]
        string _paramName;
        [SerializeField]
        ParameterContainer _container;

        [SerializeField]
        float Rate = 0f;

        [SerializeField]
        float _min = 0f;
        [SerializeField]
        float _max = 1f;

        ParameterValue _target;

        private void OnValidate()
        {
            _min = Mathf.Min(_min, _max);
            _max = Mathf.Max(_min, _max);
        }

        void Start()
        {
            if (_container == null)
            {
                Debug.LogError($"Object references not set");
                enabled = false;
                return;
            }
            _target = _container.GetParameter(_paramName);
            if (_target == null)
            {
                Debug.LogError($"Parameter value with name: \"{_paramName}\" not found");
                enabled = false;
                return;
            }
        }

        float InvLerp(float a, float b, float v)
        {
            return (v - a) / (b - a);
        }

        private void Update()
        {
            if (_target.Value >= _min && _target.Value <= _max)
                _target.Value += Rate * Time.deltaTime;
        }
    }
}