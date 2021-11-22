using System.Collections.Generic;
using Core.Entities;
using UnityEngine;

namespace Core
{
    public class EntityCollections
    {
        private EntityListCollection<PlayerEntity> _playersCollection;
        private EntityListCollection<BlockEntity> _blocksCollection;
        private EntityListCollection<BarrackEntity> _barrackCollection;

        public EntityListCollection<PlayerEntity> PlayersCollection => _playersCollection;

        public EntityListCollection<BlockEntity> BlocksCollection => _blocksCollection;

        public EntityListCollection<BarrackEntity> BarrackCollection => _barrackCollection;

        public EntityCollections()
        {
            _playersCollection = new EntityListCollection<PlayerEntity>();
            _barrackCollection = new EntityListCollection<BarrackEntity>();
            _blocksCollection = new EntityListCollection<BlockEntity>();
        }
    }
}