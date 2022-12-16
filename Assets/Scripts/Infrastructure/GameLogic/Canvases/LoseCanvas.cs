using System.Collections;
using System.Collections.Generic;
using Infrastructure.GameLogic;
using Infrastructure.GameLogic.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseCanvas : MonoBehaviour
{
    [SerializeField] private Button _againButton;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private CanvasChecker _canvasChecker;
    private GameData _gameData;

    public void OnCreate(CanvasChecker canvasChecker, GameData gameData)
    {
        _canvasChecker = canvasChecker;
        _gameData = gameData;
    }

    private void OnEnable()
    {
        if(_gameData != null)
            _highScoreText.text = $"Highest Score\n{_gameData.GetHighScore()}";
        _againButton.onClick.AddListener(Again);
    }

    private void Again()
    {
        _canvasChecker.SetState(CanvasState.MenuState);
        RestartAction.OnRestart?.Invoke();
    }

    private void OnDisable()
    {
        _gameData.SaveData();
        _againButton.onClick.RemoveAllListeners();
    }
}
