using UnityEngine;
using ProjectName.Services.Distraction;
using VContainer;
using Pripizden.Gameplay.Parameter;
using ProjectName.Gameplay.WorldObject;

namespace Pripizden.Gameplay.Activity
{
    public class BaseDistraction : MonoBehaviour
    {
        [SerializeField]
        protected Sprite _indicatorSprite;
        [SerializeField]
        protected Transform _proxyTransform;
        [SerializeField]
        protected InteractiveObject _interactiveObject;


        public bool Visible { get; protected set; } = true;
        public bool IsActive { get; protected set; } = false;

        public virtual Vector3 IndicatorPosition { get { if (_proxyTransform != null) return _proxyTransform.position; else return transform.position; }}

        public Sprite IndicatorSprite { get => _indicatorSprite; }

        protected IDistractionService _activityService;

        protected int _dayNumber = 0;

        [Inject]
        private void Construct(IDistractionService activityService)
        {
            _activityService = activityService;
        }

        public virtual void ApplyInfluence(ParameterContainer stats)
        {

        }

        public virtual void ClearInfluence(ParameterContainer stats)
        {

        }

        public void DoRestart(int dayNumber)
        {
            _dayNumber = dayNumber;
            IsActive = false;
            Restart();
        }

        protected virtual void Restart()
        {
            
        }

        public virtual bool IsAvailable()
        { 
            return true; 
        }

        public virtual void DoAutomatically()
        {
            Clear();
        }

        public virtual void Activate()
        {
            _activityService.RegisterDistraction(this);
            IsActive = true;
        }

        public virtual void Clear()
        {
            _activityService.ClearDistraction(this);
            IsActive = false;
        }


#if UNITY_EDITOR
        public void Editor_Clear()
        {
            Clear();
        }
#endif
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(BaseDistraction))]
    public class BaseActivityInspector: UnityEditor.Editor
    {


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Activate"))
                ((BaseDistraction)target).Activate();
            if (GUILayout.Button("Clear"))
                ((BaseDistraction)target).Editor_Clear();
        }
    }
#endif
}