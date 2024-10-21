using Pripizden.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectName.Services.Distraction;
using VContainer;

namespace Pripizden.Gameplay.Activity
{
    public class ActivityView : MonoBehaviour
    {
        [SerializeField]
        ActivityIcon _template;

        [SerializeField]
        RectTransform _viewRect;

        Dictionary<BaseDistraction, ActivityIcon> _activityIcons = new Dictionary<BaseDistraction, ActivityIcon>();

        IDistractionService _activityService;

        private Vector2 _screenSize;

        [SerializeField]
        RectTransform _parentRect;

        [Inject]
        public void Construct(IDistractionService activityService)
        {
            _activityService = activityService;
            Init();
        }

        private void Init()
        {
            _activityService.DistractionAppeared += ActivityService_ActivityCreated;
            _activityService.DistractionCleared += ActivityService_ActivityCleared;
            _screenSize = _parentRect.sizeDelta;
        }

        private void ActivityService_ActivityCreated(BaseDistraction obj)
        {
            var icon = Instantiate(_template, _viewRect);
            _activityIcons[obj] = icon;
            icon.SetActivity(obj);
        }

        private void ActivityService_ActivityCleared(BaseDistraction obj)
        {
            if (_activityIcons.ContainsKey(obj))
            {
                Destroy(_activityIcons[obj].gameObject);
                _activityIcons.Remove(obj);
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            _screenSize = _parentRect.sizeDelta;
        }

        private void Update()
        {
            foreach(var activity in _activityIcons.Values)
            {
                activity.DoUpdate(_screenSize);
            }
        }
    }
}