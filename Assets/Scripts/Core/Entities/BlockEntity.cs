using System;
using Core.Components;
using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(ViewBlockEntity))]
    public class BlockEntity : Entity
    {
        private ViewBlockEntity _viewBlockEntity;
        
        private Collider _collider;

        public bool EnableCollider
        {
            set => _collider.enabled = value;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _viewBlockEntity = GetComponent<ViewBlockEntity>();
        }
        
    }
}