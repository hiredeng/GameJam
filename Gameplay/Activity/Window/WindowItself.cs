using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Pripizden.Gameplay.Activity
{
    public class WindowItself : MonoBehaviour
    {
        [SerializeField]
        Animator _anim;
        [SerializeField]
        public CallbackInteractive _interactive;

        [SerializeField]
        SoundEvent _openSound;
        [SerializeField]
        SoundEvent _closeSound;

        public UnityEvent Opened;
        public UnityEvent Closed;

        public Openness State { get; private set; } = Openness.Open;

        public enum Openness
        {
            Closed,
            Open,
            Noizy
        }

        private void Awake()
        {
            _interactive.Active = true;
            _interactive.Construct(Interact, null);
        }

        public IInteractive GetInteractive()
        {
            return _interactive;
        }

        public void Interact()
        {
            if(State == Openness.Closed)
            {
                _openSound?.Invoke();
                SetState(Openness.Open);
                Opened?.Invoke();
            }
            else
            {
                _closeSound?.Invoke();
                SetState(Openness.Closed);
                Closed?.Invoke();
            }
        }

        public void SetState(Openness state)
        {
            if(state != Openness.Closed && State != Openness.Closed)
            {
                if(state == Openness.Closed)
                {
                    Closed?.Invoke();
                }
                else
                {
                    Opened?.Invoke();
                }
            }
            State = state;
            _anim.SetBool("isClosed", state == Openness.Closed);
        }
    }
}