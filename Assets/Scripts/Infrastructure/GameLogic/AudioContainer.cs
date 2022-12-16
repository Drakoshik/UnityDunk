using UnityEngine;

namespace Infrastructure.GameLogic
{
    public class AudioContainer : MonoBehaviour
    {
        private GameData _gameData;

        public void OnCreate(GameData gameData)
        {
            _gameData = gameData;
            SoundsAction.OnSound += CheckSound;
            CheckSound(_gameData.GetSound());
        }

        private void CheckSound(bool isOn)
        {
            AudioListener.volume = isOn ? 0 : 1;
        }
    }
}
