using Infrastructure.Services;

namespace Infrastructure.GameLogic.GameLogicStates.States
{
    public class LoseState : GameLogicState
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public LoseState(GameLogicStateMachine stateMachine,
            ServiceLocator<IService> serviceLocator) : base(stateMachine)
        {
        }
    }
}
