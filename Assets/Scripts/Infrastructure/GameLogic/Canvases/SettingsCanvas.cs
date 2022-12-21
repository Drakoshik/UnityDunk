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
            CheckSound(_gameData.GetSound());
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
            var isOn = !_gameData.GetLight();
            
            CheckLight(isOn);

            LightAction.OnLight(isOn);
            _gameData.SetLight(isOn);
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
        }

        private void ChangeSound()
        {
            var isOn = !_gameData.GetSound();
            CheckSound(isOn);
            _gameData.SetSound(isOn);
        }

        private void CheckSound(bool isOn)
        {
            AudioListener.volume = isOn ? 1 : 0;
            _sound.SetActive(isOn);
        }
    }
}
