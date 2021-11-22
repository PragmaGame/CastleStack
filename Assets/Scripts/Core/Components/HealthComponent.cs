using System;
using UnityEngine;

namespace Core.Components
{
    public class HealthComponent : MonoBehaviour, IChangingHealth
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        
        public event Action DeadEvent;
        public event Action<int> ChangedHealthEvent;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                ChangedHealthEvent?.Invoke(value);
            }
        }

        private void Start()
        {
            ChangedHealthEvent?.Invoke(_health);
        }

        public void Healing(int value)
        {
            var tempHealth = _health + value;
            Health = tempHealth >= _maxHealth ? _maxHealth : tempHealth;
        }

        public void Damage(int value)
        {
            var tempHealth = _health - value;

            if (tempHealth <= 0)
            {
                Health = 0;
                DeadEvent?.Invoke();
            }
            else
            {
                Health = tempHealth;
            }
        }
    }
}