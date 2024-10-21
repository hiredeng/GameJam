using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pripizden.Gameplay
{
    public class BaseHighlightable : MonoBehaviour, IHighlightable
    {
        public UnityEvent<bool> HighlightStateChanged;

        public UnityEvent Highlighted;

        public UnityEvent Cleared;

        private bool _highlightState = false;

        public void SetHighlight(bool highlight)
        {
            bool changed = _highlightState ^ highlight;
            if (changed)
            {
                _highlightState = highlight;
                HighlightStateChanged?.Invoke(_highlightState);

                if (_highlightState) Highlighted?.Invoke();
                else Cleared?.Invoke();
            }
        }
    }
}