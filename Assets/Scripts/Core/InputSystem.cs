using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class InputSystem : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IIndicateMoveDirection
    {
        [SerializeField] private RectTransform inputField;

        public event Action<Vector3> ChangedMoveDirectionEvent;

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(inputField, eventData.position,
                eventData.pressEventCamera, out var inputVector))
            {
                inputVector.Normalize();
                ChangedMoveDirectionEvent?.Invoke(new Vector3(inputVector.x, 0, inputVector.y));
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ChangedMoveDirectionEvent?.Invoke(Vector2.zero);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }
    }
}