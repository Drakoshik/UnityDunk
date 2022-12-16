using Infrastructure.GameLogic.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.GameLogic.Canvases
{
    public class InGameCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _menuButton;

        private CanvasChecker _canvasChecker;

        public void OnCreate(CanvasChecker canvasChecker)
        {
            _canvasChecker = canvasChecker;
        }
            
        
        public void SetScore(int value)
        {
            _scoreText.text = value.ToString();
        }

        private void OnEnable()
        {
            Continue();
            _pauseButton.onClick.AddListener(Pause);
            _continueButton.onClick.AddListener(Continue);
            _menuButton.onClick.AddListener(ToMenu);
        }

        private void ToMenu()
        {
            _canvasChecker.SetState(CanvasState.MenuState);
            RestartAction.OnRestart?.Invoke();
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
        }

        private void Pause()
        {
            _continueButton.gameObject.SetActive(true);
            _menuButton.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        
        private void Continue()
        {
            _continueButton.gameObject.SetActive(false);
            _menuButton.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(true);
            Time.timeScale = 1;
        }


        
    }
}
