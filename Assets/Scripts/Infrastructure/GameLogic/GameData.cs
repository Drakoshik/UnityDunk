using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLogic
{
    public class GameData : IService
    {
        private int _generalScore;
        private int _highScore;
        private int _starScore;

        private bool _isLightOn = false;
        private bool _isSoundOn = false;

        public GameData(int highScore, int starScore, bool isLightOn, bool isSoundOn)
        {
            _highScore = highScore;
            _starScore = starScore;
            _isLightOn = isLightOn;
            _isSoundOn = isSoundOn;
        }

        private int BoolToInt (bool b) 
            => (b ? 1 : 0);
        public void SaveData()
        {
            PlayerPrefs.SetInt("HighScore",_highScore);
            PlayerPrefs.SetInt("StarScore",_starScore);
            PlayerPrefs.SetInt("IsLightOn",BoolToInt(_isLightOn));
            PlayerPrefs.SetInt("IsSoundOn",BoolToInt(_isSoundOn));
        }
        
        public void SetGeneralScore(int value)
        {
            _generalScore = value;
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
        }
        
        public void SetLight(bool isOn)
        {
            _isLightOn = isOn;
        }
        public bool GetLight()
        {
            return _isLightOn;
        }
        public void SetSound(bool isOn)
        {
            _isLightOn = isOn;
        }
        public bool GetSound()
        {
            return _isSoundOn;
        }

    }
}
