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
        
        public void SetGeneralScore(int value)
        {
            _generalScore = value;
        }
        public int GetHighScore()
        {
            if (_generalScore > _highScore) _highScore = _generalScore;
            return _highScore;
        }

        public void SetStarScore(int valueToPlus)
        {
            _starScore += valueToPlus;
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
