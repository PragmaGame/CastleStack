using Core.Components;
using UnityEngine;

namespace Core.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private AffiliationComponent _affiliationComponent;
        
        public AffiliationComponent AffiliationComponent => _affiliationComponent;
        public int ID => _affiliationComponent.ID;
    }
}