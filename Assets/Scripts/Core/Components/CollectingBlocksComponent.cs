using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entities;
using UnityEngine;
using Zenject;

namespace Core.Components
{
    public class CollectingBlocksComponent : MonoBehaviour
    {
        [SerializeField] private Transform _blockStoragePoint;
        [SerializeField] private float _rateClimb;

        private AffiliationComponent _affiliationComponent;
        
        private LinkedList<Transform> _motionBlocks;
        private LinkedList<Transform> _storageBlocks;

        private bool _isGiveBlock;

        public int CountBlock => _storageBlocks.Count;

        private void Awake()
        {
            _motionBlocks = new LinkedList<Transform>();
            _storageBlocks = new LinkedList<Transform>();
        }

        [Inject]
        private void Construct(AffiliationComponent affiliationComponent)
        {
            _affiliationComponent = affiliationComponent;
        }

        private void Update()
        {
            for(var node = _motionBlocks.First; node != null; node = node.Next)
            {
                var blockTransform = node.Value;
                var position = blockTransform.position;
                var directionMovement = (_blockStoragePoint.position - position).normalized;
                position += directionMovement * (Time.deltaTime * _rateClimb);
                blockTransform.position = position;

                if (Vector3.Distance(_blockStoragePoint.position, blockTransform.position) < 0.1f)
                {
                    _motionBlocks.Remove(node);
                    _storageBlocks.AddLast(blockTransform);
                    blockTransform.SetParent(_blockStoragePoint);
                    blockTransform.position = _blockStoragePoint.position + new Vector3(0, blockTransform.localScale.y * _storageBlocks.Count, 0);
                    blockTransform.rotation = _blockStoragePoint.rotation;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BlockEntity block))
            {
                block.EnableCollider = false;
                block.AffiliationComponent.SetAffiliation(_affiliationComponent.ID, true);
                _motionBlocks.AddLast(block.transform);
            }
        }

        public Transform GetLastBlock()
        {
            var node = _storageBlocks.Last;
            _storageBlocks.RemoveLast();
            node.Value.SetParent(null);
            return node.Value;
        }

        private IEnumerator MovementBlocks()
        {
            while (true)
            {
                if (_storageBlocks.First != null)
                {
                    for (var node = _storageBlocks.First; node != null; node = node.Next)
                    {
                        var blockTransform = node.Value;
                    
                        var directionMovement = (_blockStoragePoint.position - blockTransform.position).normalized;
                        directionMovement *= Time.deltaTime * _rateClimb;
                        blockTransform.transform.position += new Vector3(directionMovement.x,0, directionMovement.z);
                    
                        blockTransform.rotation = _blockStoragePoint.rotation;
                        yield return null;;
                    }
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}