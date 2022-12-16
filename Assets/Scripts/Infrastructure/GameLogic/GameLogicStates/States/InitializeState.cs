using Infrastructure.Services;

namespace Infrastructure.GameLogic.GameLogicStates.States
{
    public class InitializeState : GameLogicState
    {
        public override void Enter()
        {
            base.Enter();
            
            StateMachine.EnterState<MenuState>();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public InitializeState(GameLogicStateMachine stateMachine,
            ServiceLocator<IService> serviceLocator) : base(stateMachine)
        {
            
        }
    }
}
