using System;
using System.Collections.Generic;
using ProjectName.Core.Game.States;
using ProjectName.Core.StateMachine;
using VContainer;

namespace ProjectName.Core.Game
{
    public class GameStateMachine : IStateMachine
    {
        private IState _activeState;
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(IObjectResolver objectResolver)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(DataMigrationState)] = ResolveState<DataMigrationState>(objectResolver),
                [typeof(LoadAppState)] = ResolveState<LoadAppState>(objectResolver),

                [typeof(MainMenuState)] = ResolveState<MainMenuState>(objectResolver),
                [typeof(LoadPlayerProgressState)] = ResolveState<LoadPlayerProgressState>(objectResolver),
                [typeof(LoadLevelState)] = ResolveState<LoadLevelState>(objectResolver),
                [typeof(GameLoopState)] = ResolveState<GameLoopState>(objectResolver),
                [typeof(UnloadLevelState)] = ResolveState<UnloadLevelState>(objectResolver),

                [typeof(UnloadAppState)] = ResolveState<UnloadAppState>(objectResolver),
            };
        }


        public void Enter<TState>() where TState : class, IEnterState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }

        private IState ResolveState<TState>(IObjectResolver resolver) where TState : BaseGameState, IState
        {
            var state = resolver.Resolve<TState>();
            state.SetStateMachine(this);
            return state;
        }
    }
}
