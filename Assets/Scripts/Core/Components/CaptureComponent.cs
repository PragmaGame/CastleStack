using System;
using UnityEngine;
using Zenject;

namespace Core.Components
{
    public class CaptureComponent : MonoBehaviour
    {
        private AffiliationComponent _affiliationComponent;
        private HealthComponent _healthComponent;
        private TakingBlocksComponent _takingBlocksComponent;

        [Inject]
        private void Construct(AffiliationComponent affiliationComponent, HealthComponent healthComponent, TakingBlocksComponent takingBlocksComponent)
        {
            _affiliationComponent = affiliationComponent;
            _healthComponent = healthComponent;
            _takingBlocksComponent = takingBlocksComponent;
        }

        private void OnEnable()
        {
            _takingBlocksComponent.TookBlockEvent += OnChangeCapturePoints;
        }

        private void OnDisable()
        {
            _takingBlocksComponent.TookBlockEvent -= OnChangeCapturePoints;
        }

        private void OnChangeCapturePoints(int value, AffiliationComponent affiliationComponent)
        {
            if (_affiliationComponent.ID == affiliationComponent.ID)
            {
                _healthComponent.Healing(value);
            }
            else
            {
                _healthComponent.Damage(value);
            }

            if (_healthComponent.Health == 0)
            {
                _affiliationComponent.SetAffiliation(affiliationComponent.ID, affiliationComponent.ColorID);
            }
        }
    }
}