using Infrastructure.GameLogic.Canvases;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLogic.GameLogicStates.States
{
    public class InitializeState : GameLogicState
    {
        private readonly ServiceLocator<IService> _serviceLocator;
        public override void Enter()
        {
            base.Enter();
            
            var mainBall = Object.Instantiate(Resources.Load("Prefabs/MainBall",
                    typeof(GameObject))) as GameObject;
            var cameras = Object.Instantiate(Resources.Load("Prefabs/Cameras",
                typeof(CameraController))) as CameraController;
            var gameScenario = Object.Instantiate(Resources.Load("Prefabs/GameScenario",
                typeof(GameScenario))) as GameScenario;
            var inGameCanvas = Object.Instantiate(Resources.Load("Prefabs/InGameCanvas",
                typeof(InGameCanvas))) as InGameCanvas;
            var menuCanvas = Object.Instantiate(Resources.Load("Prefabs/MenuCanvas",
                typeof(MenuCanvas))) as MenuCanvas;
            var settingsCanvas = Object.Instantiate(Resources.Load("Prefabs/SettingsCanvas",
                typeof(SettingsCanvas))) as SettingsCanvas;
            var loseCanvas = Object.Instantiate(Resources.Load("Prefabs/LoseCanvas",
                typeof(LoseCanvas))) as LoseCanvas;
            var canvasChecker = Object.Instantiate(Resources.Load("Prefabs/CanvasChecker",
                typeof(CanvasChecker))) as CanvasChecker;
            
            
            if (canvasChecker != null)
                canvasChecker.OnCreate(inGameCanvas, menuCanvas, settingsCanvas, loseCanvas);
            
            if (menuCanvas != null)
                menuCanvas.OnCreate(_serviceLocator.Get<GameData>(),
                    canvasChecker);

            if (settingsCanvas != null) settingsCanvas.OnCreate(_serviceLocator.Get<GameData>());
            if (loseCanvas != null) loseCanvas.OnCreate(canvasChecker, _serviceLocator.Get<GameData>());
            inGameCanvas.OnCreate(canvasChecker);

            if (gameScenario != null)
                gameScenario.OnCreate(mainBall,
                    _serviceLocator.Get<GameData>(),inGameCanvas, canvasChecker, cameras);
            
            StateMachine.EnterState<GameState>();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public InitializeState(GameLogicStateMachine stateMachine,
            ServiceLocator<IService> serviceLocator) : base(stateMachine)
        {
            _serviceLocator = serviceLocator;
        }
    }
}
