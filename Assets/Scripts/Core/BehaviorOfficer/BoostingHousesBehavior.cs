using System;
using Core.Components;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class BoostingHousesBehavior : OfficerBehavior, IOfficerBehavior
    {
        private Transform _targetEntity;
        private bool _isGiveBrick;
        public event Action<Vector3> ChangeMoveDirectionEvent;

        public BoostingHousesBehavior(OfficerLogicComponent officerLogicComponent) : base(officerLogicComponent)
        {
            _isGiveBrick = false;
        }
        
        public void Run()
        {
            var priority = officerLogicComponent.BehaviorPriorityTypes();
            var barrackCollection = officerLogicComponent.EntityCollections.BarrackCollection;

            _targetEntity = FindPriorityTarget(barrackCollection, priority);
        }

        public void Tick()
        {
            if (IsToleranceZoneRadius(_targetEntity.position, officerTransform.position) && !_isGiveBrick)
            {
                
            }
        }
    }
}