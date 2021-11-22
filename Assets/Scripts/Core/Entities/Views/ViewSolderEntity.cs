using UnityEngine;
using Zenject;

namespace Core.Entities
{
    public class ViewSolderEntity : ViewEntity
    {
        [SerializeField] private TextMesh _healthText;
        
        private IChangingHealth _changingHealth;

        private void Awake()
        {
            _typeEntity = TypeEntity.Solder;
        }

        [Inject]
        public void Init(IChangingHealth changingHealth)
        {
            _changingHealth = changingHealth;
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