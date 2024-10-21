using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.XR;

namespace ProjectName.Core.StateMachine
{
    public interface IStateMachine
    {
        public void Enter<TState>() where TState : class, IEnterState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    }

    
}