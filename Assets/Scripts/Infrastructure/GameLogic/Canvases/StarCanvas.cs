using Infrastructure.GameLogic.Actions;
using TMPro;
using UnityEngine;

namespace Infrastructure.GameLogic.Canvases
{
    public class StarCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _starText;

        private GameData _gameData;

        public void OnCreate(GameData gameData)
        {
            _gameData = gameData;
            StarCollectAction.OnStarCollect += SetScore;
            _starText.text = _gameData.GetStarScore().ToString();
        }

        private void SetScore()
        {
            _gameData.InscreaseStarScore();
            _gameData.SaveData();
            _starText.text = _gameData.GetStarScore().ToString();
        }
    }
}
