using System;
using Core.Components;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class BattleBehavior : IOfficerBehavior
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
    }
}