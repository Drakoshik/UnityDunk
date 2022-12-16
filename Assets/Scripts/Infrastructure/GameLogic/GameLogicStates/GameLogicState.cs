using Infrastructure.StateMachine;

namespace Infrastructure.GameLogic.GameLogicStates
{
    public class GameLogicState : IState
    {
        protected GameLogicStateMachine StateMachine;

        public GameLogicState(GameLogicStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        public virtual void Enter()
        {
        
        }

        public virtual void Exit()
        {
        
        }
    }
}
