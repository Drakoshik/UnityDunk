using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLogic.GameLogicStates.States
{
    public class BootstrapState : GameLogicState
    {
        private readonly ServiceLocator<IService> _serviceLocator;

        public BootstrapState(GameLogicStateMachine stateMachine,
            ServiceLocator<IService> serviceLocator) : base(stateMachine)
        {
            _serviceLocator = serviceLocator;

            RegisterServices();
        }

        private bool IntToBool (int n) 
            => n == 1;
        
        private void RegisterServices()
        {

            var highScore = 1;
            if (PlayerPrefs.HasKey("HighScore"))
                highScore = PlayerPrefs.GetInt("HighScore");
            var starScore = 1;
            if (PlayerPrefs.HasKey("StarScore"))
                starScore = PlayerPrefs.GetInt("StarScore");
            var isLightOn = false;
            if (PlayerPrefs.HasKey("IsLightOn"))
                isLightOn = IntToBool(PlayerPrefs.GetInt("IsLightOn"));
            var isSoundOn = false;
            if (PlayerPrefs.HasKey("IsSoundOn"))
                isSoundOn = IntToBool(PlayerPrefs.GetInt("IsSoundOn"));
                
            
            var gameData = new GameData(highScore,starScore, isLightOn,isSoundOn);

            _serviceLocator.Register(gameData);
        }
        
        public override void Enter()
        {
            base.Enter();
            StateMachine.EnterState<InitializeState>();
        }

        public override void Exit()
        {
            base.Exit();
        }

    }
}
