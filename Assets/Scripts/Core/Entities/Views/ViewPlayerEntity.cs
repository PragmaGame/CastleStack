using System;
using UnityEngine;
using Zenject;

namespace Core.Entities
{
    public class ViewPlayerEntity : ViewEntity
    {
        [SerializeField] private TextMesh _healthText;
        
        private IChangingHealth _changingHealth;

        private void Awake()
        {
            _typeEntity = TypeEntity.Player;
        }

        [Inject]
        public void Construct(IChangingHealth changingHealth)
        {
            _changingHealth = changingHealth;

            _changingHealth.ChangedHealthEvent += OnChangedHealth;
        }

        protected new void OnEnable()
        {
            base.OnEnable();
            _changingHealth.ChangedHealthEvent += OnChangedHealth;
        }

        protected new void OnDisable()
        {
            base.OnDisable();
            _changingHealth.ChangedHealthEvent -= OnChangedHealth;
        }

        private void OnChangedHealth(int value)
        {
            _healthText.text = value.ToString();
        }
    }
}