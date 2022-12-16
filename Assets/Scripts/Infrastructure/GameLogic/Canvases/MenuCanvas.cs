using System;
using Infrastructure.GameLogic.Actions;
using Infrastructure.GameLogic.GameLogicStates.States;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

namespace Infrastructure.GameLogic
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private Button _lightButton;
        [SerializeField] private Button _hideMenu;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameObject _lightOnImage;
        [SerializeField] private GameObject _lightOffImage;

        
        private GameData _gameData;
        private CanvasChecker _canvasChecker;

        public void OnCreate(GameData gameData, CanvasChecker canvasChecker)
        {
            _gameData = gameData;
            _canvasChecker = canvasChecker;
            var isOn = _gameData.GetLight();
            CheckLight(isOn);
        }
        
        private void OnEnable()
        {
            _lightButton.onClick.AddListener(ChangeLight);
            _hideMenu.onClick.AddListener(HideMenu);
            _settingsButton.onClick.AddListener(ShowMenu);
        }

        private void ShowMenu()
        {
            _canvasChecker.SetState(CanvasState.SettingsState);
        }

        private void OnDisable()
        {
            _lightButton.onClick.RemoveAllListeners();
            _hideMenu.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }


        private void HideMenu()
        {
            gameObject.SetActive(false);
            _canvasChecker.SetState(CanvasState.GameState);
        }
        private void ChangeLight()
        {
            var isOn = _gameData.GetLight();
            CheckLight(isOn);
            _gameData.SetLight(!isOn);
        }

        private void CheckLight(bool isOn)
        {
            if (isOn)
            {
                _lightOnImage.SetActive(false);
                _lightOffImage.SetActive(true);
            }
            else
            {
                _lightOnImage.SetActive(true);
                _lightOffImage.SetActive(false);
            }

            LightAction.OnLight(isOn);
        }
    }
}
