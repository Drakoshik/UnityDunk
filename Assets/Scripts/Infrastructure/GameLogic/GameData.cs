using System;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLogic
{
    [Serializable]
    public class GameData : IService
    {
        private int _generalScore;
        [SerializeField] private int _highScore;
        [SerializeField] private int _starScore;

        [SerializeField] private bool _isLightOn;
        [SerializeField] private bool _isSoundOn;

        public GameData(int highScore, int starScore, bool isLightOn, bool isSoundOn)
        {
            _highScore = highScore;
            _starScore = starScore;
            _isLightOn = isLightOn;
            _isSoundOn = isSoundOn;
        }
        
        public void SaveData()
        {
            LocalDataManager.Save(this);
        }
        
        public void SetGeneralScore(int value)
        {
            _generalScore = value;
            SaveData();
        }
        public int GetHighScore()
        {
            if (_generalScore > _highScore) _highScore = _generalScore;
            return _highScore;
        }

        public int GetStarScore()
        {
            return _starScore;
        }
        public void InscreaseStarScore()
        {
            _starScore++;
            SaveData();
        }
        
        public void SetLight(bool isOn)
        {
            _isLightOn = isOn;
            SaveData();
        }
        public bool GetLight()
        {
            return _isLightOn;
        }
        public void SetSound(bool isOn)
        {
            _isSoundOn = isOn;
            SaveData();
        }
        public bool GetSound()
        {
            return _isSoundOn;
        }

    }
}
