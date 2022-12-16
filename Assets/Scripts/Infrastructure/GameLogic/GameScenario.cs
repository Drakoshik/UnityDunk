using System;
using Infrastructure.GameLogic.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.GameLogic
{
    public class GameScenario : MonoBehaviour
    {
        [SerializeField] private GameObject _mainBallPrefab;
        
        [SerializeField] private BasketController _basketPrefab;

        [SerializeField] private GameObject[] _spawnPlaces;

        public Action OnActive;

        private BasketController _activeBasket;

        private ObjectPool<BasketController> _pool;
    
        private void Start()
        {
            _pool ??= new ObjectPool<BasketController>(_basketPrefab, 5, this.transform, true);

            _activeBasket = _pool.GetFreeElement();
            BallAction.OnBallCatch += CheckBaskets;
            // _activeBasket
        }

        private void OnDisable()
        {
            _pool.HideAllElements();
        }

        private static void CheckBaskets()
        {
            print("slfkdhjgbn");
        }
    }
}
