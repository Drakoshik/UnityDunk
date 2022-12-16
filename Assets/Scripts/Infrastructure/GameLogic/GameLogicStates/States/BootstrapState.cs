using Infrastructure.Services;

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

        private void RegisterServices()
        {
            var gameData = new GameData();

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
