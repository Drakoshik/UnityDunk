using System;
using Infrastructure.GameLogic.Actions;
using Infrastructure.GameLogic.Canvases;
using Infrastructure.GameLogic.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Infrastructure.GameLogic
{
    public class GameScenario : MonoBehaviour
    {
        [SerializeField] private GameObject _mainBall;
        [SerializeField] private GameData _gameData;
        
        
        [SerializeField] private BasketController _basketPrefab;

        [SerializeField] private Transform[] _spawnPlaces;
        [SerializeField] private GameObject _spawnPlacesObject;

        [SerializeField] private Transform _startSpawn;

        private BasketController _activeBasket;
        private int _activeSpawnIndex;

        private ObjectPool<BasketController> _pool;

        private InGameCanvas _inGameCanvas;
        private int _score = 0;

        private CanvasChecker _canvasChecker;

        private Vector3 _startPlaceSpawn;

        private CameraController _cameraController;
    
        private void Start()
        {
            _startPlaceSpawn = _spawnPlacesObject.transform.position;
            RestartAction.OnRestart += PrepareGame;
        }

        public void OnCreate(GameObject mainBall, GameData gameData, InGameCanvas inGameCanvas,
            CanvasChecker canvasChecker, CameraController cameraController)
        {
            _pool ??= new ObjectPool<BasketController>(_basketPrefab, 5, this.transform, true);
            
            _gameData = gameData;
            _mainBall = mainBall;
            _cameraController = cameraController;
            BallAction.OnBallCatch += CheckBaskets;
            

            _inGameCanvas = inGameCanvas;
            _canvasChecker = canvasChecker;
            
            PrepareGame();
        }

        private void PrepareGame()
        {
            _cameraController.SetFollow(_mainBall);
            _canvasChecker.SetState(CanvasState.MenuState);
            _score = 0;
            _pool.HideAllElements();
            _activeBasket = _pool.GetFreeElement();
            var startPosition = _startSpawn.position;
            _mainBall.transform.position = startPosition;
            _activeBasket.transform.position = startPosition;
            _spawnPlacesObject.transform.position = _startPlaceSpawn;
            _activeSpawnIndex = _spawnPlaces.Length - 1;
        }

        private void OnDisable()
        {
            _pool.HideAllElements();
        }

        private void CheckBaskets()
        {
            foreach (var basket in _pool.GetAllElements())
            {
                if (basket.isActiveAndEnabled && !basket.GetIsActive())
                    basket.gameObject.SetActive(false);
            }
            _activeBasket = _pool.GetFreeElement();
            _activeBasket.transform.position = _spawnPlaces[_activeSpawnIndex].transform.position;
            _activeBasket.transform.rotation = Quaternion.identity;
            var newSpawnIndex = 0;

            while (newSpawnIndex == _activeSpawnIndex)
            {
                newSpawnIndex = Random.Range(0, _spawnPlaces.Length);
            }
            _activeSpawnIndex = newSpawnIndex;
            var upSpawn = Random.Range(.7f, 3f);
            _spawnPlacesObject.transform.position =
                new Vector3(_spawnPlacesObject.transform.position.x,
                    _spawnPlacesObject.transform.position.y + upSpawn,
                    _spawnPlacesObject.transform.position.y);
            _score++;
            _gameData.SetGeneralScore(_score-1);
            _inGameCanvas.SetScore(_score-1);
        }
        

        public void Restart()
        {
            _canvasChecker.SetState(CanvasState.LoseState);
            _cameraController.ResetFollow();
        }
    }
}
