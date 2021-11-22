using System;
using ScriptableObjects;
using UnityEngine;

namespace Core.Components
{
    public class AffiliationComponent : MonoBehaviour, IChangingViewAffiliation
    {
        [SerializeField] private int _colorId;
        [SerializeField] private int _id;

        public event Action<int> ChangedAffiliationEvent;
        public event Action<int> ChangedViewAffiliationEvent;
        
        public int ID => _id;
        public int ColorID => _colorId;
        
        public void Start()
        {
            ChangedAffiliationEvent?.Invoke(_id);
            ChangedViewAffiliationEvent?.Invoke(_colorId);
        }

        public void SetAffiliation(int id)
        {
            _id = id;
            ChangedAffiliationEvent?.Invoke(id);
        }
        
        public void SetAffiliation(int id, bool isColor)
        {
            _id = id;
            
            if (isColor)
            {
                _colorId = id;
            }
            
            ChangedAffiliationEvent?.Invoke(id);
            ChangedViewAffiliationEvent?.Invoke(_colorId);
        }
        
        public void SetAffiliation(int id, int colorId)
        {
            _id = id;
            _colorId = colorId;
            ChangedAffiliationEvent?.Invoke(id);
            ChangedViewAffiliationEvent?.Invoke(colorId);
        }
    }
}