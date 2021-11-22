using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] [Range(0.01f, 1f)] private float smoothFactor = 0.5f;
        
        private Vector3 _cameraOffset;

        private void Start()
        {
            _cameraOffset = transform.position - targetTransform.position;
        }

        private void LateUpdate()
        {
            var newPos = targetTransform.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        }
    }
}