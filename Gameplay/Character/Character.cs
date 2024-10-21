using Pripizden.Gameplay.Parameter;
using Pripizden.InputSystem;
using ProjectName.Extensions;
using ProjectName.Gameplay.Interactive;
using ProjectName.Gameplay.Interactive.Types;
using ProjectName.Gameplay.WorldObject;
using ProjectName.Services.Distraction;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Pripizden.Gameplay.Character
{
    public class Character : Pawn
    {
        [SerializeField]
        private Speech _speech;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Vector2 _movementVelocity = new Vector2(5f, 3.5f);

        [SerializeField]
        private ParameterContainer _stats;
        private Vector2 _movementDelta = Vector2.zero;
        
        private Vector3 _initialPosition;
        private Camera _mainCam;




        public ParameterContainer Stats { get => _stats; }


        [SerializeField]
        private LayerMask _movementObstacleLayer;
        [SerializeField]
        private LayerMask _interactionLayer;

        private HashSet<IHighlightable> _highlightables = new HashSet<IHighlightable>();
        private HashSet<IHighlightable> _removableHighlights = new HashSet<IHighlightable>();

        private Vector3? _targetPosition;
        private IRequireChoice<Character> _currentRequireChoice;
        private ICaptive<Character> _currentCaptive;

        private InteractiveObject _currentInteractive;
        private InteractiveObject _hoveredInteractive;

        private bool _isLookingUp = false;
        private bool _isAvoidingObstacle = false;



        private IDistractionService _distractionService;


        [Inject]
        public void Construct(IDistractionService distractionService)
        {
            _distractionService = distractionService;
            _distractionService.DistractionAppeared += OnDistraction;
            _distractionService.DistractionCleared += OnDistractionEnd;
        }


        private void OnDistraction(Activity.BaseDistraction distraction)
        {
            distraction.ApplyInfluence(Stats);
        }
        private void OnDistractionEnd(Activity.BaseDistraction distraction)
        {
            distraction.ClearInfluence(Stats);
        }



        protected override void SetupInputComponent()
        {
            base.SetupInputComponent();

            InputComponent.BindAction("MouseClick", ActionEvent.Pressed, HandleMouseDown);
            InputComponent.BindVector2("Move", ReceiveMovementDelta);
        }


        public void RequestChoice(IRequireChoice<Character> requireChoice)
        {
            _currentRequireChoice = requireChoice;
            var options = _currentRequireChoice.GetOptions();

        }

        public void Bark(string conversation)
        {
            _speech.ShowBark(conversation, 3f);
        }



        public void EnterCaptive(ICaptive<Character> captive)
        {
            _currentCaptive = captive;
            _currentCaptive.Enter(this);
        }

        public void ExitCaptive()
        {
            ICaptive<Character> tempCaptive = _currentCaptive;
            _currentCaptive = null;
            tempCaptive.Leave(this);
        }





        private void HandleMouseDown()
        {
            if (_hoveredInteractive.NotExists()) return;
            StartInteraction(_hoveredInteractive);
        }

        private void StartInteraction(InteractiveObject interactive)
        {
            _currentInteractive = interactive;
            if(_currentInteractive.InteractionPoint.NotExists())
            {
                _currentInteractive.Interact(this);
                _currentInteractive = null;
            }
            else
            {
                _targetPosition = _currentInteractive.InteractionPoint.Position;
            }
        }


        



















        private void Awake()
        {
            _mainCam = Camera.main;
        }



        public override void Restart()
        {
            _movementDelta = Vector2.zero;
            foreach(var high in _highlightables)
            {
                high.SetHighlight(false);
            }
            _highlightables.Clear();
            _removableHighlights.Clear();
            transform.position = _initialPosition;
        }
        

        private void Update()
        {
            
            transform.position = transform.position + (Vector3)(_movementVelocity * _movementDelta * Time.deltaTime);

            if (_isAvoidingObstacle)
            {
                _movementDelta = Vector2.zero;
            }

            var hit = Physics2D.Raycast(transform.position, _movementDelta, _movementDelta.magnitude, _movementObstacleLayer);
            if (hit.collider != null)
            {
                _movementDelta = (hit.point - (Vector2)transform.position);
                if(_movementDelta.sqrMagnitude<0.1f)
                {
                    _isAvoidingObstacle = true;
                    if (hit.collider is PolygonCollider2D)
                    {
                        _movementDelta = Vector2.down;
                    }
                    else
                    {
                        _movementDelta = Vector2.up;
                    }
                }
            }

            float vel = Mathf.Abs(_movementDelta.y);
            if (vel > 0.001)
            {
                _isLookingUp = (_movementDelta.y > 0.1f);
            }


            if (_targetPosition != null)
            {
                Vector3 delta = _targetPosition.Value - transform.position;
                if(delta.sqrMagnitude < _movementVelocity.sqrMagnitude*Time.deltaTime)
                {
                    _movementDelta = Vector3.zero;
                    _targetPosition = null;
                    if(_currentInteractive.Exists())
                    {
                        _isLookingUp = true;
                        _currentInteractive.Interact(this);

                        switch (_currentInteractive.InteractionPoint.Direction)
                        {
                            case InteractionDirection.Down:
                            case InteractionDirection.DownLeft:
                            case InteractionDirection.DownRight:
                                {
                                    _isLookingUp = false;
                                    break;
                                }
                        }
                        _currentInteractive = null;
                    }
                }
                else
                {
                    _movementDelta = (delta).normalized;
                }
            }


            _removableHighlights.Clear();
            _removableHighlights.AddRange(_highlightables);

            _hoveredInteractive = null;
            var ray = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            var o = Physics2D.OverlapCircleAll(ray, 0.2f, _interactionLayer);
            foreach (var c in o)
            {
                var comp = c.GetComponent<InteractiveObject>();
                if (comp.NotExists() || !comp.Active) continue;
                if (_hoveredInteractive.NotExists())
                    _hoveredInteractive = comp;

                var highlight = comp.GetComponent<IHighlightable>();
                if (highlight.NotExists()) continue;

                _removableHighlights.Remove(highlight);
                if (!_highlightables.Contains(highlight))
                {
                    highlight.SetHighlight(true);
                    _highlightables.Add(highlight);
                }
            }

            foreach (var h in _removableHighlights)
            {
                h.SetHighlight(false);
                _highlightables.Remove(h);
            }

            _animator.SetBool("isReversed", _isLookingUp);
            _animator.SetFloat("HorizontalSpeed", _movementDelta.x);
            _animator.SetFloat("VerticalSpeed", _movementDelta.y);
            _animator.SetFloat("TotalSpeed", _movementDelta.magnitude);
        }

        private void ReceiveMovementDelta(Vector2 delta)
        {          
            StopMovement();

            _isAvoidingObstacle = false;
            if (_currentCaptive.Exists())
            {
                if (_currentCaptive.IsMovementAllowed())
                    ExitCaptive();
                else
                    return;
            }

            var hit = Physics2D.Raycast(transform.position, delta, delta.magnitude, _movementObstacleLayer);
            if (hit.collider!=null)
            {
                delta = (hit.point - (Vector2)transform.position);
            }
            _movementDelta = delta;
        }

        protected void StopMovement()
        {
            _targetPosition = null;
        }

        public void GoTo(Vector3 position)
        {
            position.z = 0f;
            _targetPosition = new Vector3?(position);
        }
    }
}