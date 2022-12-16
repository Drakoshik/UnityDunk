using UnityEngine;

namespace Infrastructure.GameLogic
{
    public class RestartTrigger : MonoBehaviour
    {
        [SerializeField] private GameScenario _gameScenario;

        private void OnTriggerEnter2D(Collider2D col)
        {
            _gameScenario.Restart();
        }
    }
}
