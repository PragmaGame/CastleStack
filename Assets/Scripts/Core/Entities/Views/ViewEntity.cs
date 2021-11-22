using System;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Core.Entities
{
    public abstract class ViewEntity : MonoBehaviour
    {
        [SerializeField] private ColorSettings _colorSettings;
        [SerializeField] private MeshRenderer _meshRenderer;

        protected TypeEntity _typeEntity;

        private IChangingViewAffiliation _changingAffiliation;
        
        [Inject]
        private void Construct(IChangingViewAffiliation changingAffiliation)
        {
            _changingAffiliation = changingAffiliation;
        }

        protected void OnEnable()
        {
            _changingAffiliation.ChangedViewAffiliationEvent += OnChangedAffiliation;
        }

        protected void OnDisable()
        {
            _changingAffiliation.ChangedViewAffiliationEvent -= OnChangedAffiliation;
        }

        private void OnChangedAffiliation(int colorID)
        {
            var currentMaterial = _colorSettings.GetMaterial(colorID, _typeEntity);
            _meshRenderer.material = currentMaterial;
        }
    }
}