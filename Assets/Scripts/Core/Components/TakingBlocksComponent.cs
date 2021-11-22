using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace Core.Components
{
    public class TakingBlocksComponent : MonoBehaviour
    {
        private BlockSpawnSystem _blockSpawnSystem;

        [SerializeField] private Transform _targetBlockPoint;
        [SerializeField] private float _speed;
        [SerializeField] private float _blockRemovalInterval;
        
        private WaitForSeconds _blockRemovalIntervalWait;
        
        private LinkedList<BlockCollector> _collectors;
        private LinkedList<Transform> _blockMoved;

        public event Action<int, AffiliationComponent> TookBlockEvent;

        private void Awake()
        {
            _collectors = new LinkedList<BlockCollector>();
            _blockMoved = new LinkedList<Transform>();
            
            _blockRemovalIntervalWait = new WaitForSeconds(_blockRemovalInterval);
        }

        [Inject]
        private void Construct(BlockSpawnSystem blockSpawnSystem)
        {
            _blockSpawnSystem = blockSpawnSystem;
        }

        private void Start()
        {
            StartCoroutine(PickUpBlock());
        }

        private void OnTriggerEnter(Collider other)
        {
            //TODO trriger
            //Debug.Log("Enter" + other.gameObject.name);
            var obj = other.gameObject;
            
            if (obj.TryGetComponent(out CollectingBlocksComponent collectingBlocksComponent) && 
                obj.TryGetComponent(out AffiliationComponent affiliationComponent))
            {
                _collectors.AddLast(new BlockCollector(affiliationComponent, collectingBlocksComponent));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Debug.Log("Exit" + other.gameObject.name);
            if (other.gameObject.TryGetComponent(out CollectingBlocksComponent collectingBlocksComponent))
            {
                for (var node = _collectors.First; node != null; node = node.Next)
                {
                    if (node.Value.collectingBlocksComponent.Equals(collectingBlocksComponent))
                    {
                        _collectors.Remove(node);
                        return;
                    }
                }
            }
        }

        private void Update()
        {
            for (var node = _blockMoved.First; node != null; node = node.Next)
            {
                var blockTransform = node.Value;

                var position = blockTransform.position;
                var directionMovement = (_targetBlockPoint.position - position).normalized;
                position += directionMovement * (Time.deltaTime * _speed);
                blockTransform.position = position;

                if (Vector3.Distance(_targetBlockPoint.position, blockTransform.position) < 0.3f)
                {
                    _blockMoved.Remove(node);
                    _blockSpawnSystem.ReclaimBlock(blockTransform);
                }
            }
        }

        private IEnumerator PickUpBlock()
        {
            while (true)
            {
                for (var node = _collectors.First; node != null; node = node.Next)
                {
                    var value = node.Value;

                    if (value.collectingBlocksComponent.CountBlock == 0)
                    {
                        continue;    
                    }
                    
                    TookBlockEvent?.Invoke(1, value.affiliationComponent);
                
                    var blockTransform = value.collectingBlocksComponent.GetLastBlock();
                    _blockMoved.AddLast(blockTransform);
                }
            
                yield return _blockRemovalIntervalWait;
            }
        }

        private struct BlockCollector
        {
            public AffiliationComponent affiliationComponent;
            public CollectingBlocksComponent collectingBlocksComponent;

            public BlockCollector(AffiliationComponent affiliationComponent, CollectingBlocksComponent collectingBlocksComponent)
            {
                this.affiliationComponent = affiliationComponent;
                this.collectingBlocksComponent = collectingBlocksComponent;
            }
        }
    }
}