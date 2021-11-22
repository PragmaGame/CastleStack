using System;
using Core.Components;
using UnityEngine;

namespace Core
{
    public class PatrollingBehavior : IOfficerBehavior
    {
        public event Action<Vector3> ChangeMoveDirectionEvent;

        public void Run()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public PatrollingBehavior(OfficerLogicComponent officerLogicComponent)
        {
            
        }
    }
}