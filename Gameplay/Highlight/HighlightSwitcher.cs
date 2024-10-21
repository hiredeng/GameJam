using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pripizden.Gameplay
{
    public class HighlightSwitcher : MonoBehaviour
    {
        [SerializeField] BaseHighlightable highlight;

        [SerializeField] GameObject _unlit;
        [SerializeField] GameObject _lit;

        private void Reset()
        {
            for (int i = 0, c = transform.childCount; i<c ; i++)
            {
                var child = transform.GetChild(i);
                if(child.name.Contains("_ob"))
                {
                    _lit = child.gameObject;
                }
                else
                {
                    _unlit = child.gameObject;
                }

                if (_lit != null && _unlit != null) break;
            }
            highlight = GetComponentInParent<BaseHighlightable>();
            if (_unlit != null) _unlit.SetActive(true);
            if (_lit != null) _lit.SetActive(false);
        }

        private void Awake()
        {
            if(_lit!=null) _lit.SetActive(false);
            if(highlight!=null) highlight.HighlightStateChanged.AddListener(Switch);
        }

        private void Switch(bool state)
        {
            if (_unlit != null) _unlit.SetActive(!state);
            if (_lit != null) _lit.SetActive(state);
        }
    }
}