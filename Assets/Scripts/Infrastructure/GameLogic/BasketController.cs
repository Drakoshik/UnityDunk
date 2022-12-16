using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.GameLogic
{
    public class BasketController : MonoBehaviour
    {
        [SerializeField] private Transform _basketWeb;
        [SerializeField] private Transform _ballPlace;
        private Vector2 _startWebScale;

        private const float MaxWebScale = .19f;
        private const float MinWebScale = .15f;
    
        private Camera _camera;
        private Vector3 _startMousePoint;
        private Vector3 _endMousePoint;

        private float _currentPower;

        private bool _isActive;

        private Collider2D obj;
        private bool _isEmpty = true;

        
        private void OnEnable()
        {
            _isEmpty = true;
        }

        private void Start()
        {
            _camera = Camera.main;
            _startWebScale = _basketWeb.transform.localScale;
        }

        private void Update()
        {
            if(!_isActive) return;
            if (Input.GetMouseButtonDown(0))
            {
                _startMousePoint = GetMousePosition();
                _currentPower = 1;
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 dragPosition = GetMousePosition();

                if(Vector2.Distance(_startMousePoint,
                       dragPosition) == 0) return;
                
                SetBasketRotation(dragPosition);
                SetWebScale(dragPosition);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _basketWeb.transform.localScale = _startWebScale;
            }

            if (!_isEmpty)
                obj.transform.position = Vector2.MoveTowards(obj.transform.position,
                    _ballPlace.position, Time.deltaTime * 2f);

        }

        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            _isActive = true;
            obj = col;
            _isEmpty = false;
            BallAction.OnBallCatch();
        }
        
        private void OnTriggerStay2D(Collider2D col)
        {
            obj = col;
            if (obj.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None) return;
            _isEmpty = true;
            _isActive = false;

        }

        #region ChangeBasketMethods

        private void ChangeWebScales(float yValue)
        {
            Vector3 localScale = _basketWeb.transform.localScale;
            localScale = new Vector3(
                localScale.x,
                yValue,
                localScale.z);
            _basketWeb.transform.localScale = localScale;
        }

        private void SetWebScale(Vector3 dragPosition)
        {
            var currentDistance = 1 + (Vector2.Distance(_startMousePoint,
                dragPosition) / 10);
                
                
            switch (currentDistance * MinWebScale)
            {
                case > MaxWebScale:
                {
                    ChangeWebScales(MaxWebScale);
                    return;
                }
                case < MinWebScale:
                {
                    ChangeWebScales(MinWebScale);
                    return;
                }
                default:
                {
                    if (currentDistance != _currentPower)
                    {
                        ChangeWebScales(currentDistance * MinWebScale);
                        _currentPower = currentDistance;
                    }
                    break;
                }
            }
        }

        private void SetBasketRotation(Vector3 currentMousePosition)
        {
            Vector3 diff = _startMousePoint - currentMousePosition;
            diff.Normalize();

            var rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
        }

        #endregion
        
        
        private Vector3 GetMousePosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
