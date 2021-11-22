using System;
using Core.Components;
using UnityEngine;
using Zenject;

namespace Core.Entities
{
    [RequireComponent(typeof(ViewBarackEntity))]
    public class BarrackEntity : Entity
    {
        [SerializeField] private TakingBlocksComponent _takingBlocksComponent;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private CaptureComponent _captureComponent;
        [SerializeField] private SpawnUnitComponent _spawnUnitComponent;
        
        private ViewBarackEntity _viewBarackEntity;
        
        private BlockSpawnSystem _blockSpawnSystem;
        private EntityCollections _entityCollections;
        private PoolManager _poolManager;
        
        [Inject]
        private void Construct(BlockSpawnSystem blockSystem, PoolManager poolManager, EntityCollections entityCollections)
        {
            _blockSpawnSystem = blockSystem;
            _poolManager = poolManager;
            _entityCollections = entityCollections;
        }

        protected void Awake()
        {
            _viewBarackEntity = GetComponent<ViewBarackEntity>();
        }
    }
}