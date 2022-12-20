using System;
using System.Collections.Generic;

namespace Infrastructure.StateMachine
{
    public abstract class StateMachine
    {
        protected IState CurrentState  { get; set; }

        protected Dictionary<Type, IState> States;

        public void EnterState<TState>() where TState : IState
        {
            CurrentState?.Exit();
            var state = States[typeof(TState)];
            state.Enter();
            CurrentState = state;
        }
    }
}
