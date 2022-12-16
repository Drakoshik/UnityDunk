using Infrastructure.Services;

namespace Infrastructure.GameLogic.GameLogicStates.States
{
    public class GameState : GameLogicState
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public GameState(GameLogicStateMachine stateMachine,
            ServiceLocator<IService> serviceLocator) : base(stateMachine)
        {
        }
    }
}
