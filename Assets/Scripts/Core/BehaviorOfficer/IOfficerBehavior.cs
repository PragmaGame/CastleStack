using System;
using System.Collections.Generic;
using Core.Components;
using Core.Entities;
using UnityEngine;

namespace Core
{
    public interface IOfficerBehavior
    {
        public event Action<Vector3> ChangeMoveDirectionEvent;
        
        public void Run();

        public void Tick();
    }
}