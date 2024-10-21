using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pripizden.Gameplay.Activity
{
    public class ActivityIcon : MonoBehaviour
    {
        [SerializeField]
        Image _activityIcon;
        [SerializeField]
        RectTransform _underlayTransform;
        [SerializeField]
        RectTransform _iconTransform;
        [SerializeField]
        RectTransform _pivotTransform;
        BaseDistraction _target;
        Camera _cam;

        RectTransform _myRect;

        private float _elevation = 0f;

        public void SetActivity(BaseDistraction source)
        {
            _cam = Camera.main;
            _activityIcon.sprite = source.IndicatorSprite;
            _target = source;
            _myRect = GetComponent<RectTransform>();
        }

        public void Click()
        {
            _target.DoAutomatically();
        }

        public void SetVisible( bool visible)
        {
            _activityIcon.gameObject.SetActive(visible);
            _underlayTransform.gameObject.SetActive(false);
        }

        public void DoUpdate(Vector2 screensize)
        {
            if(_activityIcon.gameObject.activeSelf!=_target.Visible)
            {
                _activityIcon.gameObject.SetActive(_target.Visible);
            }

            var screenposition = _cam.WorldToViewportPoint(_target.IndicatorPosition);

            bool onScreen = screenposition.x > 0.05f;
            onScreen &= screenposition.x < 0.95f;
            onScreen &= screenposition.y > 0.05f;
            onScreen &= screenposition.x < 0.95f;

            bool flip = screenposition.x > .5f;
            if (onScreen)
            {
                
            }
            else
            {
                screenposition.x = Mathf.Clamp(screenposition.x, 0.05f, 0.95f);
                screenposition.y = Mathf.Clamp(screenposition.y, 0.05f, 0.95f);
            }

            if (onScreen)
                _elevation = Mathf.Lerp(_elevation, 1f, 3f*Time.deltaTime);
            else
                _elevation = Mathf.Lerp(_elevation, 0f, 3f * Time.deltaTime);

            _activityIcon.SetNativeSize();
            _iconTransform.sizeDelta = _iconTransform.sizeDelta / 2;
            _underlayTransform.localScale = new Vector3(flip?-1:1, 1, 1);
            float verticalOffset = Mathf.Lerp(0f, 0.14f, _elevation);


            _myRect.anchoredPosition = Vector2.Scale(screenposition+Vector3.up* verticalOffset, screensize);

            float rotation = Mathf.Lerp(0f, flip ? -58f : 58f, _elevation);

            _underlayTransform.eulerAngles = new Vector3(0f, 0f, rotation);
            _iconTransform.position = _pivotTransform.position;
        }
    }
}