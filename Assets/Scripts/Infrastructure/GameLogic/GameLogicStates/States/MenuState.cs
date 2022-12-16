using Infrastructure.Services;

namespace Infrastructure.GameLogic.GameLogicStates.States
{
    public class MenuState : GameLogicState
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public MenuState(GameLogicStateMachine stateMachine,
            ServiceLocator<IService> serviceLocator) : base(stateMachine)
        {
        }
    }
}
