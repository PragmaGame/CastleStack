using System;
using Core.Components;
using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(ViewSolderEntity))]
    public class SolderEntity : Entity
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private SolderLogicComponent _solderLogicComponent;

        private ViewSolderEntity _viewSolderEntity;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _viewSolderEntity = GetComponent<ViewSolderEntity>();
        }
    }
}