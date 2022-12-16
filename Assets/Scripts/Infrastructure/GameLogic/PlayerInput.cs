using System;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameLogic
{
    public class PlayerInput : MonoBehaviour, IService
    {
        public event Action OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

    }
}
