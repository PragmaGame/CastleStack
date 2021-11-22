using System;
using Core.Components;
using UnityEngine;
using Zenject;

namespace Core.Entities
{
    [RequireComponent(typeof(ViewPlayerEntity))]
    public class PlayerEntity : Entity
    {
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private CollectingBlocksComponent _collectingBlocksComponent;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private OfficerComponent _officerComponent;
        
        private ViewPlayerEntity _viewPlayerEntity;

        public event Action<PlayerEntity> DeadEvent;
        public Transform TransformPlayer => transform;
        public OfficerComponent OfficerComponent => _officerComponent;
        

        [Inject]
        private void Construct(EntityCollections entityCollections)
        {
            entityCollections.PlayersCollection.AddEntity(this);
        }

        private void Awake()
        {
            _viewPlayerEntity = GetComponent<ViewPlayerEntity>();
        }
    }
}