using System;
using UnityEngine;

namespace Core
{
    public interface IIndicateMoveDirection
    {
        public event Action<Vector3> ChangedMoveDirectionEvent;
    }
}