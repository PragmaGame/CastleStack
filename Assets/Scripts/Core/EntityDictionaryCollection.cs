using System.Collections.Generic;
using Core.Entities;

namespace Core
{
    public class EntityDictionaryCollection<T> where T : Entity
    {
        private Dictionary<int, List<T>> _entities;

        public EntityDictionaryCollection()
        {
            _entities = new Dictionary<int, List<T>>();
        }

        public void AddEntity(T entity)
        {
            var id = entity.ID;
            
            if (_entities.ContainsKey(id))
            {
                _entities[id].Add(entity);
            }
            else
            {
                var tempList = new List<T> {entity};
                
                _entities.Add(id, tempList);
            }
        }

        public void RemoveEntity(T entity)
        {
            var id = entity.ID;

            if (_entities.ContainsKey(id))
            {
                _entities[id].Remove(entity);
            }
        }

        public bool TryGetFirstEntity(int id, out T entity)
        {
            if (_entities.ContainsKey(id))
            {
                entity = _entities[id][0];
                return true;
            }

            entity = null;
            return false;
        }

        public bool TryGetEntities(int id, out List<T> entities)
        {
            if (_entities.ContainsKey(id))
            {
                entities = _entities[id];
                return true;
            }

            entities = null;
            return false;
        }

        public bool TryGetSelection(out List<T> entities, bool exclusionSelection = true,params int[] selection)
        {
            entities = new List<T>();
            
            foreach (var item in _entities)
            {
                var isCondition = true;
                
                foreach (var sample in selection)
                {
                    if (item.Key == sample)
                    {
                        isCondition = false;
                        break;
                    }
                }

                if (isCondition)
                {
                    entities.AddRange(item.Value);       
                }
            }

            return entities.Count != 0;
        }
    }
}