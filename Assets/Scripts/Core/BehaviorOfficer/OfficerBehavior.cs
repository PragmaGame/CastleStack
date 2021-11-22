using System.Collections.Generic;
using Core.Components;
using Core.Entities;
using UnityEngine;

namespace Core
{
    public abstract class OfficerBehavior
    {
        protected OfficerLogicComponent officerLogicComponent;
        protected Transform officerTransform;

        protected OfficerBehavior(OfficerLogicComponent officerLogicComponent)
        {
            this.officerLogicComponent = officerLogicComponent;
            officerTransform = officerLogicComponent.transform;
        }
        
        protected Transform FindPriorityTarget<T>(EntityListCollection<T> collection, List<BehaviorPriorityTypes> priority) where T : Entity
        {
            for (var index = 0; index < priority.Count; index++)
            {
                if (collection.TryGetSelectionByType(out var barrackEntities, priority[index],
                    officerLogicComponent.AffiliationComponent.ID))
                {
                    return SearchNearest(officerTransform,barrackEntities);
                }
            }
            
            return null;
        }
        
        protected static Transform SearchNearest(Transform startSearch, IEnumerable<Entity> entities)
        {
            var minDistance = float.MaxValue;
            Transform nearestTransform = null;
            var startSearchPosition = startSearch.position;
                    
            foreach (var entity in entities)
            {
                var currentDistance =
                    Vector3.Distance(startSearchPosition, entity.transform.position);
                        
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    nearestTransform = entity.transform;
                }
            }
        
            return nearestTransform;
        }

        protected bool IsToleranceZoneRadius(Vector3 a, Vector3 b)
        {
            var aPos = new Vector2(a.x, a.z);
            var bPos = new Vector2(b.x, b.z);

            return Vector2.Distance(aPos, bPos) < officerLogicComponent.ToleranceZoneRadius;
        }
    }
}