using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Entities;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core
{
    public class BlockSpawnSystem : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float radius;

        private GameObject _container;
        private PoolManager _poolManager;
        
        private EntityListCollection<BlockEntity> _blocks;
        
        private float _leftBorder;
        private float _rightBorder;
        private float _topBorder;
        private float _downBorder;

        private void Awake()
        {
            _container = new GameObject("BlockContainer");

            var position = spawnPoint.position;
            
            _leftBorder = position.x - radius;
            _rightBorder = position.x + radius;
            _topBorder = position.z + radius;
            _downBorder = position.z - radius;
        }

        [Inject]
        private void Construct(PoolManager poolManager, EntityCollections entityCollections)
        {
            _poolManager = poolManager;
            _blocks = entityCollections.BlocksCollection;
        }
        
        public void Initialize()
        {
            var amount = _poolManager.GetAmountObject(TypeEntity.Block);
            
            for (var i = 0; i < amount; i++)
            {
                var pos = GetRandomPosition();
                var rotation = GetRandomRotation();
                var objBlock = _poolManager.GetObject(TypeEntity.Block);
                objBlock.transform.SetPositionAndRotation(pos,rotation);
                _blocks.AddEntity(objBlock.GetComponent<BlockEntity>());
            }
        }

        private Vector3 GetRandomPosition()
        {
            var x = Random.Range(_leftBorder, _rightBorder);
            var z = Random.Range(_downBorder, _topBorder);

            return new Vector3(x, 0.1f, z);
        }

        private Quaternion GetRandomRotation()
        {
            var y = Random.Range(1, 359);
            return Quaternion.Euler(0, y, 0);
        }
        

        public void ReclaimBlock(Transform block)
        {
            block.SetParent(_container.transform);
            block.position = GetRandomPosition();
            block.rotation = GetRandomRotation();
            
            var blockEntity = block.GetComponent<BlockEntity>();
            blockEntity.EnableCollider = true;
            blockEntity.AffiliationComponent.SetAffiliation(0,true);
        }
    }
}