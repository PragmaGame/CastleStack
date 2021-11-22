using System;
using Core.Components;
using UnityEngine;
using Zenject;

namespace Core.Entities
{
    public class ViewBlockEntity : ViewEntity
    {
        private void Awake()
        {
            _typeEntity = TypeEntity.Block;
        }

        [Inject]
        public void Construct(IChangingViewAffiliation changingAffiliation)
        {
        }
    }
}