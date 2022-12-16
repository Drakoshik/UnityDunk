using Infrastructure.GameLogic.Objects;
using UnityEngine;

namespace Infrastructure.GameLogic.Pool
{
    public class Poller : MonoBehaviour
    {
        [SerializeField] private Ball prefab;

        private ObjectPool<Ball> _pool;
    
        private void Start()
        {
            _pool ??= new ObjectPool<Ball>(prefab, 70, this.transform, true);
        }
        
        private void OnDisable()
        {
            _pool.HideAllElements();
        }
    }
}