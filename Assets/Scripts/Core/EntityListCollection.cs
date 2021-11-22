using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core
{
    public class EntityListCollection<T> where T : Entity
    {
        private List<T> _entities;

        public EntityListCollection()
        {
            _entities = new List<T>();
        }

        public void AddEntity(T entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(T entity)
        {
            _entities.Remove(entity);
        }
        
        public bool TryGetFirstEntity(int id, out T entity)
        {
            foreach (var item in _entities)
            {
                if (item.ID == id)
                {
                    entity = item;
                    return true;
                }
            }

            entity = null;
            return false;
        }
        
        public bool TryGetEntities(int id, out List<T> entities)
        {
            entities = new List<T>();
            
            foreach (var item in _entities)
            {
                if (item.ID == id)
                {
                    entities.Add(item);
                }
            }
            
            return entities.Count != 0;
        }
        
        public bool TryGetSelection(out List<T> entities, bool exclusionSelection = true,params int[] selection)
        {
            entities = new List<T>();
            
            foreach (var item in _entities)
            {
                var isCondition = true;
                
                foreach (var sample in selection)
                {
                    if (item.ID == sample)
                    {
                        isCondition = false;
                        break;
                    }
                }

                if (isCondition)
                {
                    entities.Add(item);       
                }
            }

            return entities.Count != 0;
        }

        public bool TryGetSelectionByType(out List<T> entities, BehaviorPriorityTypes types, int idRequested)
        {
            entities = new List<T>();
            
            switch (types)
            {
                case BehaviorPriorityTypes.Neutral:
                {
                    TryGetEntities(0, out _entities);
                    break;   
                }
                case BehaviorPriorityTypes.Mine:
                {
                    TryGetEntities(idRequested, out _entities);
                    break;   
                }
                case BehaviorPriorityTypes.Hostile:
                {
                    TryGetSelection(out _entities, true, 0, 1, idRequested);
                    break;   
                }
                case BehaviorPriorityTypes.Player:
                {
                    TryGetEntities(1, out _entities);
                    break;
                }
            }
            
            return entities.Count != 0;
        }
    }
}