using System;
using UnityEngine;

namespace Infrastructure.GameLogic
{
    public class BoardChecker : MonoBehaviour
    {
        [SerializeField] private GameObject _leftBoard;
        [SerializeField] private GameObject _rightBoard;

        private Camera _mainCamera;
        
        private int lastScreenWidth = 0;
        private int lastScreenHeight = 0;


        private void Start()
        {
            _mainCamera = Camera.main;
            CheckPositionToScreen();
        }

        private void FixedUpdate()
        {
            if (lastScreenWidth == Screen.width && lastScreenHeight == Screen.height) return;
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            CheckPositionToScreen();
        }

        private void CheckPositionToScreen()
        {
            Vector2 min = _mainCamera.ViewportToWorldPoint (new Vector2 (0,0)); // bottom-left corner
            Vector2 max = _mainCamera.ViewportToWorldPoint (new Vector2 (1,1));

            _leftBoard.transform.position = new Vector3(
                min.x, _rightBoard.transform.position.y, 0f);
            _rightBoard.transform.position = new Vector3(
                max.x, _rightBoard.transform.position.y, 0f);
        }
    }
}
