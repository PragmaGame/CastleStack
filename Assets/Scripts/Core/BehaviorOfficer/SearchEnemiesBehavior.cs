using System;
using System.Collections.Generic;
using Core.Components;
using Core.Entities;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class SearchEnemiesBehavior : OfficerBehavior, IOfficerBehavior
    {
        private Transform _targetEntity;
        public event Action<Vector3> ChangeMoveDirectionEvent;

        public SearchEnemiesBehavior(OfficerLogicComponent officerLogicComponent) : base(officerLogicComponent)
        {
        }

        public void Run()
        {
            var priority = officerLogicComponent.BehaviorPriorityTypes();
            var playersCollections = officerLogicComponent.EntityCollections.PlayersCollection;

            _targetEntity = FindPriorityTarget(playersCollections, priority);
        }

        public void Tick()
        {
            if (!IsToleranceZoneRadius(_targetEntity.position, officerTransform.position))
            {
                var moveDirection = (_targetEntity.position - officerTransform.position).normalized;
                ChangeMoveDirectionEvent?.Invoke(new Vector3(moveDirection.x, 0,moveDirection.z));
            }
        }
    }
}