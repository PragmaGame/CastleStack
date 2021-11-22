using System;
using System.Collections.Generic;
using Core.Components;
using Core.Entities;
using UnityEngine;

namespace Core
{
    public class CollectingBricksBehavior : OfficerBehavior, IOfficerBehavior
    {
        private Vector3 _targetEntityPosition;
        public event Action<Vector3> ChangeMoveDirectionEvent;

        public CollectingBricksBehavior(OfficerLogicComponent officerLogicComponent) : base(officerLogicComponent)
        {
        }

        public void Run()
        {
            var priority = officerLogicComponent.BehaviorPriorityTypes();
            var blockCollection = officerLogicComponent.EntityCollections.BlocksCollection;

            _targetEntityPosition = FindPriorityTarget(blockCollection, priority).position;
        }

        public void Tick()
        {
            if (IsToleranceZoneRadius(_targetEntityPosition, officerTransform.position))
            {
                Run();
            }
            else
            {
                var moveDirection = (_targetEntityPosition - officerTransform.position).normalized;
                ChangeMoveDirectionEvent?.Invoke(new Vector3(moveDirection.x, 0,moveDirection.z));
            }
        }
    }
}