using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.GameLogic
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private GameObject _ballPrediction;
        [SerializeField] private float _powerIncrease;

        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private int _pointsCount;

        [SerializeField] private  float _blindZone = 1f;
        [SerializeField] private  float _maxTensionZone = 2.5f;
        
        private Camera _camera;
        private Vector2 _startMousePoint;
        private Vector2 _endMousePoint;

        private Rigidbody2D _rigidbody;


        private GameObject[] _points;
        private List<SpriteRenderer> _activePoints;

        private GameObject _predictionBall;
        private Rigidbody2D _predictionBallPhysics;

        private bool _inFlight = false;


        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        private void Start()
        {
            _camera = Camera.main;

            CreatePredictionPointsPool();

            _predictionBall = Instantiate(_ballPrediction);
            _predictionBall.SetActive(false);
            _predictionBallPhysics = _predictionBall.GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.GetComponent<BasketController>()) return;
            _inFlight = false;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }


        private void CreatePredictionPointsPool()
        {
            var container =  Instantiate(new GameObject());
            container.name = _pointPrefab.name;
            
            _points = new GameObject[_pointsCount];
            _points[0] = Instantiate(_pointPrefab, container.transform);
            _points[0].SetActive(false);
            
            _activePoints = new List<SpriteRenderer> { _points[0].GetComponent<SpriteRenderer>() };
            
            for (var counter = 1; counter < _pointsCount; counter++)
            {
                _points[counter] = Instantiate(_pointPrefab, container.transform);
                _points[counter].SetActive(false);
                if (counter % 7 == 0)
                {
                    _activePoints.Add(_points[counter].GetComponent<SpriteRenderer>());
                }
            }
        }

        private void Update()
        {
            OnInput();
        }

        private void OnInput()
        {
            
            if (_inFlight) return;
            if (Input.GetMouseButtonDown(0))
            {
                _startMousePoint = GetMousePosition();
                foreach (var point in _activePoints)
                {
                    point.gameObject.SetActive(true);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 dragPosition = GetMousePosition();
                TrajectoryControlCheck(dragPosition);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _endMousePoint = GetMousePosition();

                var power = _startMousePoint - _endMousePoint;
                var currentDistance = Vector2.Distance(_startMousePoint, _endMousePoint);
                if (currentDistance > _maxTensionZone) 
                    power = GetPowerInBounds(power, _maxTensionZone);
                foreach (var point in _activePoints)
                {
                    point.gameObject.SetActive(false);
                    SetPointAlpha(point, 0);
                }
                if (currentDistance < _blindZone) return;
                _inFlight = true;
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.AddForce(power * _powerIncrease, ForceMode2D.Force);
            }
        }

        private void TrajectoryControlCheck(Vector2 dragPosition)
        {
            var power = _startMousePoint - dragPosition;

            var currentDistance = Vector2.Distance(_startMousePoint, dragPosition);
            if (currentDistance > _maxTensionZone)
                power = GetPowerInBounds(power, _maxTensionZone);

            foreach (var point in _activePoints)
            {
                SetPointAlpha(point, 0);
            }

            if (currentDistance > _blindZone)
            {
                foreach (var point in _activePoints)
                {
                    SetPointAlpha(point, currentDistance - _blindZone);
                }

                _predictionBall.SetActive(true);
                _predictionBall.transform.position = transform.position;
                _predictionBallPhysics.AddForce(power * _powerIncrease,
                    ForceMode2D.Force);

                Physics2D.simulationMode = SimulationMode2D.Script;
                foreach (var point in _points)
                {
                    Physics2D.Simulate(Time.fixedDeltaTime);
                    point.transform.position =
                        _predictionBall.transform.position;
                }

                Physics2D.simulationMode = SimulationMode2D.FixedUpdate;

                _predictionBall.SetActive(false);
            }
        }

        private static void SetPointAlpha(SpriteRenderer point, float alphaLevel)
        {
            var tmp = point.color;
            tmp.a = alphaLevel;
            point.color = tmp;
        }
        
        private static Vector2 GetPowerInBounds(Vector2 b, float maxBounds)
        {
            var a = Vector2.zero;
            var x = a.x + ((b.x - a.x) * maxBounds) / 
                Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
            var y = a.y + ((b.y - a.y) * maxBounds) / 
                Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
            return new Vector2(x, y);
        }

        private Vector3 GetMousePosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
