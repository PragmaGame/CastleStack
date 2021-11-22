using System;
using UnityEngine;

namespace Core.Components
{
    public class SolderLogicComponent : MonoBehaviour,IIndicateMoveDirection
    {
        private Transform _officerTransform;
        private Vector3 _offsetLocationPoint;

        public event Action<Vector3> ChangedMoveDirectionEvent;
        
        public Vector3 OffsetLocationPoint
        {
            get => _offsetLocationPoint;
            set => _offsetLocationPoint = value;
        }

        public Transform OfficerTransform
        {
            set => _officerTransform = value;
        }

        private void Awake()
        {
            _offsetLocationPoint = Vector3.zero;
            _officerTransform = transform;
        }

        private void FixedUpdate()
        {
            var moveDirection = ((_officerTransform.position + _offsetLocationPoint) - transform.position).normalized;

            ChangedMoveDirectionEvent?.Invoke(new Vector3(moveDirection.x, 0,moveDirection.z));
        }
    }
}