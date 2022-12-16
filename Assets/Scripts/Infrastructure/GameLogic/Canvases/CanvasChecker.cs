using Infrastructure.GameLogic.Canvases;
using UnityEngine;

namespace Infrastructure.GameLogic
{
    public enum CanvasState
    {
        MenuState,
        GameState,
        SettingsState,
        LoseState
    }
    public class CanvasChecker : MonoBehaviour
    {
        private InGameCanvas _inGameCanvas;
        private MenuCanvas _menuCanvas;
        private SettingsCanvas _settingsCanvas;
        private LoseCanvas _loseCanvas;

        public void OnCreate(InGameCanvas inGameCanvas, MenuCanvas menuCanvas,
            SettingsCanvas settingsCanvas, LoseCanvas loseCanvas)
        {
            _inGameCanvas = inGameCanvas;
            _menuCanvas = menuCanvas;
            _settingsCanvas = settingsCanvas;
            _loseCanvas = loseCanvas;
        }

        public void SetState(CanvasState canvasState)
        {
            switch (canvasState)
            {
                case CanvasState.GameState:
                    _inGameCanvas.gameObject.SetActive(true);
                    _menuCanvas.gameObject.SetActive(false);
                    _settingsCanvas.gameObject.SetActive(false);
                    _loseCanvas.gameObject.SetActive(false);
                    break;
                case CanvasState.MenuState:
                    _inGameCanvas.gameObject.SetActive(false);
                    _menuCanvas.gameObject.SetActive(true);
                    _settingsCanvas.gameObject.SetActive(false);
                    _loseCanvas.gameObject.SetActive(false);
                    break;
                case CanvasState.SettingsState:
                    _settingsCanvas.gameObject.SetActive(true);
                    break;
                case CanvasState.LoseState:
                    _inGameCanvas.gameObject.SetActive(false);
                    _menuCanvas.gameObject.SetActive(false);
                    _settingsCanvas.gameObject.SetActive(false);
                    _loseCanvas.gameObject.SetActive(true);
                    break;
                
            }
            
        }
        
    }
}
