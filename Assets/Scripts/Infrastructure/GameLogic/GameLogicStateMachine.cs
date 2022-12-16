using System;
using System.Collections.Generic;
using Infrastructure.GameLogic.GameLogicStates.States;
using Infrastructure.Services;
using Infrastructure.StateMachine;

namespace Infrastructure.GameLogic
{
    public class GameLogicStateMachine : StateMachine.StateMachine
    {
        public GameLogicStateMachine(ServiceLocator<IService> serviceLocator)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, serviceLocator),
                [typeof(InitializeState)] = new InitializeState(this, serviceLocator),
                [typeof(MenuState)] = new MenuState(this, serviceLocator),
                [typeof(GameState)] = new GameState(this, serviceLocator),
                [typeof(LoseState)] = new LoseState(this, serviceLocator)
            };
        }
    }
}
