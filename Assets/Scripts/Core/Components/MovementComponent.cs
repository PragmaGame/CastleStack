using System;
using Core.Entities;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Core.Components
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private IIndicateMoveDirection _indicateMoveDirection;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Vector3 _directionMovement;

        [Inject]
        private void Construct(IIndicateMoveDirection indicateMoveDirection)
        {
            _indicateMoveDirection = indicateMoveDirection;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _directionMovement = Vector3.zero;
        }

        private void OnEnable()
        {
            _indicateMoveDirection.ChangedMoveDirectionEvent += CorrectionMovementDirection;
        }

        private void OnDisable()
        {
            _indicateMoveDirection.ChangedMoveDirectionEvent -= CorrectionMovementDirection;
        }

        public void FixedUpdate()
        {
            Move();
            Rotation();
        }
        
        private void Move()
        {
            _rigidbody.MovePosition(_transform.position + _directionMovement * (Time.deltaTime * _speed));
        }
        
        private void Rotation()
        {
            var angle = Vector3.Angle(Vector3.forward, _directionMovement);
                    
            if (angle > 1f || angle == 0f)
            {
                var directionRotation = Vector3.RotateTowards(_transform.forward, _directionMovement, _speed, 0f);
                _rigidbody.MoveRotation(Quaternion.LookRotation(directionRotation));
            }
        }
        
        private void CorrectionMovementDirection(Vector3 direction)
        {
            _directionMovement = direction;
        }
    }
}