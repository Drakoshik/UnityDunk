using System;
using Infrastructure.GameLogic.Actions;
using UnityEngine;

namespace Infrastructure.GameLogic
{
    public class StarTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _star;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void ActivateStar()
        {
            _star.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_star.activeInHierarchy) return;
            StarCollectAction.OnStarCollect?.Invoke();
            _audioSource.Play(); 
            _star.SetActive(false);

        }
    }
}
