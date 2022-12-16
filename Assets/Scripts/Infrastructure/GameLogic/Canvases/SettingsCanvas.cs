using Infrastructure.GameLogic.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.GameLogic
{
    public class SettingsCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _lightOnImage;
        [SerializeField] private GameObject _lightOffImage;
        [SerializeField] private GameObject _sound;

        [SerializeField] private Button _lightButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _backButton;

        private GameData _gameData;

        public void OnCreate(GameData gameData)
        {
            _gameData = gameData;
            CheckSound();
        }
        
        private void OnEnable()
        {
            _lightButton.onClick.AddListener(ChangeLight);
            _soundButton.onClick.AddListener(CheckSound);
            _backButton.onClick.AddListener(Hide);
            
        }

        private void OnDisable()
        {
            _lightButton.onClick.RemoveAllListeners();
            _soundButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }

    
        private void ChangeLight()
        {
            var isOn = _gameData.GetLight();
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
            _gameData.SetLight(!isOn);
        }
        private void CheckSound()
        {
            var isOn = _gameData.GetLight();
            _sound.SetActive(isOn);

            LightAction.OnLight(isOn);
            _gameData.SetLight(!isOn);
        }
    }
}
