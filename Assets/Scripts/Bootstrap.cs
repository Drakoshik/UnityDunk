using Infrastructure.GameLogic;
using Infrastructure.GameLogic.GameLogicStates.States;
using Infrastructure.Services;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Awake() => Init();

    private IService qwe;
    private void Init()
    {
        DontDestroyOnLoad(gameObject);
        var gameLogicStateMachine = new GameLogicStateMachine(
            new ServiceLocator<IService>());
        gameLogicStateMachine.EnterState<BootstrapState>();
    }
}