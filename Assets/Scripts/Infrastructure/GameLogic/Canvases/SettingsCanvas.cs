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

        private AudioSource _audioSource;

        private GameData _gameData;

        public void OnCreate(GameData gameData)
        {
            _gameData = gameData;
            var isOn = _gameData.GetLight();
            CheckSound(isOn);
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void OnEnable()
        {
            _lightButton.onClick.AddListener(ChangeLight);
            _soundButton.onClick.AddListener(ChangeSound);
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
        private void ChangeSound()
        {
            var isOn = _gameData.GetLight();
            CheckSound(isOn);
            _gameData.SetLight(!isOn);
        }

        private void CheckSound(bool isOn)
        {
            AudioListener.volume = isOn ? 0 : 1;
            _sound.SetActive(!isOn);
        }
    }
}
