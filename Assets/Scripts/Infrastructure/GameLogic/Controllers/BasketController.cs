using Infrastructure.GameLogic.Actions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.GameLogic.Controllers
{
    public class BasketController : MonoBehaviour
    {
        [SerializeField] private Transform _basketWeb;
        [SerializeField] private Transform _ballPlace;

        [SerializeField] private StarTrigger _star;
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
        private bool _isUsed;
        

        
        private void OnEnable()
        {
            _isEmpty = true;
            _isUsed = false;
        }

        private void OnDisable()
        {
            _isEmpty = true;
            _isUsed = false;
            _isActive = false;
        }

        private void Start()
        {
            _camera = Camera.main;
            _startWebScale = _basketWeb.transform.localScale;
        }
        

        private void Update()
        {
            if(!_isActive) return;
            
            if (!_isEmpty)
                obj.transform.position = Vector2.MoveTowards(obj.transform.position,
                    _ballPlace.position, Time.deltaTime * 2f);
            
// #if UNITY_ANDROID
//             if(Input.touchCount <= 0) return;
//             if (Input.GetTouch(0).phase == TouchPhase.Began)
//             {
//                 _startMousePoint = GetTouchPosition();
//                 _currentPower = 1;
//             }
//             
//             if (Input.GetTouch(0).phase == TouchPhase.Moved)
//             {
//                 Vector2 dragPosition = GetTouchPosition();
//             
//                 if(Vector2.Distance(_startMousePoint,
//                        dragPosition) == 0) return;
//                 
//                 SetBasketRotation(dragPosition);
//                 SetWebScale(dragPosition);
//             }
//             
//             if (Input.GetTouch(0).phase == TouchPhase.Ended)
//             {
//                 _basketWeb.transform.localScale = _startWebScale;
//             }
//             
// #else

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


// #endif

        }

        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            _isActive = true;
            obj = col;
            _isEmpty = false;
            if(!_isUsed)
                BallAction.OnBallCatch();
            _isUsed = true;
        }
        
        private void OnTriggerStay2D(Collider2D col)
        {
            obj = col;
            if (obj.GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.None) return;
            _isEmpty = true;
            _isActive = false;

        }

        public bool GetIsActive()
        {
            return _isActive;
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
            if (currentDistance * MinWebScale > MaxWebScale || currentDistance * MinWebScale < MinWebScale)
            {
                if (currentDistance * MinWebScale > MaxWebScale)
                {
                    ChangeWebScales(MaxWebScale);
                    return;
                }
                if (currentDistance * MinWebScale < MaxWebScale)
                {
                    ChangeWebScales(MinWebScale);
                    return;
                }
            }
            else
            {
                if (currentDistance != _currentPower)
                {
                    ChangeWebScales(currentDistance * MinWebScale);
                    _currentPower = currentDistance;
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
        
        private Vector3 GetTouchPosition()
        {
            return _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        public void ShowStar()
        {
            var chance = Random.Range(0, 100);
            if(chance > 50)
                _star.ActivateStar();
        }
    }
}
