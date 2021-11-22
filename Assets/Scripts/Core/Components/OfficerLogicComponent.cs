using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ScriptableObjects;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.Components
{
    public class OfficerLogicComponent : MonoBehaviour, IIndicateMoveDirection
    {
        [SerializeField] private int _botComplexityLevel;
        [SerializeField] private BehaviorOfficersType _currentTypeBehavior;
        [SerializeField] private float _timeRecalculationBehavior;
        [SerializeField] private float _toleranceZoneRadius;
        [SerializeField] private ComplexityBotsSettings _complexityBotsSettings;

        private IOfficerBehavior _currentBehavior;
        private IOfficerBehavior _boostingHousesBehavior;
        private IOfficerBehavior _collectingBricksBehavior;
        private IOfficerBehavior _searchEnemiesBehavior;
        private WaitForSeconds _timeRecalculationBehaviorWait;
        private int _behaviorLevel;
        private Complexity _currentComplexity;
        private EntityCollections _entityCollections;
        private AffiliationComponent _affiliationComponent;

        public event Action<Vector3> ChangedMoveDirectionEvent;

        public EntityCollections EntityCollections => _entityCollections;
        public AffiliationComponent AffiliationComponent => _affiliationComponent;
        public float ToleranceZoneRadius => _toleranceZoneRadius;

        [Inject]
        private void Construct(EntityCollections entityCollections, AffiliationComponent affiliationComponent)
        {
            _entityCollections = entityCollections;
            _affiliationComponent = affiliationComponent;
        }
        
        private void Awake()
        {
            _timeRecalculationBehaviorWait = new WaitForSeconds(_timeRecalculationBehavior);
            _behaviorLevel = 0;

            _boostingHousesBehavior = new BoostingHousesBehavior(this);
            _collectingBricksBehavior = new CollectingBricksBehavior(this);
            _searchEnemiesBehavior = new SearchEnemiesBehavior(this);
        }

        public List<BehaviorPriorityTypes> BehaviorPriorityTypes()
        {
            var collection = _complexityBotsSettings.LevelsBehaviors[_behaviorLevel].BehaviorPriorityTypes;
            
            foreach (var item in collection)
            {
                if (item.type == _currentTypeBehavior)
                {
                    return item.priorityTargetSelection;
                }
            }

            return null;
        }

        private void Start()
        {
            ChangeBehavior(_currentTypeBehavior);
            StartCoroutine(ChangeDifficulty());
        }

        private void Update()
        {
            _currentBehavior.Tick();
        }

        private IEnumerator ChangeDifficulty()
        {
            while (_behaviorLevel <= 2)
            {
                yield return _timeRecalculationBehaviorWait;
                
                _currentComplexity = _complexityBotsSettings.GetComplexityBot(_behaviorLevel, _botComplexityLevel);
                var typeBehavior = RecalculationBehavior(_currentComplexity.ProbabilityBehaviors);
                ChangeBehavior(typeBehavior);
                _behaviorLevel++;
            }
        }

        public void ChangeBehavior(BehaviorOfficersType type)
        {
            StopCurrentBehavior();
            
            switch (type)
            {
                case BehaviorOfficersType.BoostingHouses:
                {
                    _currentBehavior = _boostingHousesBehavior;
                    break;   
                }
                case BehaviorOfficersType.CollectingBricks:
                {
                    _currentBehavior = _collectingBricksBehavior;
                    break;
                }
                case BehaviorOfficersType.SearchEnemies:
                {
                    _currentBehavior = _searchEnemiesBehavior;
                    break;
                }
            }
            
            _currentTypeBehavior = type;
            
            StartCurrentBehavior();
        }

        private void StopCurrentBehavior()
        {
            if(_currentBehavior != null)
                _currentBehavior.ChangeMoveDirectionEvent -= OnChangedMoveDirection;
        }

        private void StartCurrentBehavior()
        {
            _currentBehavior.ChangeMoveDirectionEvent += OnChangedMoveDirection;
            _currentBehavior.Run();
        }

        private void OnChangedMoveDirection(Vector3 direction)
        {
            ChangedMoveDirectionEvent?.Invoke(direction);
        }

        private BehaviorOfficersType RecalculationBehavior(IReadOnlyList<ProbabilityBehavior> probabilityBehaviors)
        {
            float total = 0;

            foreach (var t in probabilityBehaviors)
            {
                total += t.probability;
            }
            
            var randomPoint = Random.value * total;
            
            foreach (var item in probabilityBehaviors)
            {
                if (randomPoint < item.probability)
                {
                    return item.type;
                }
		    
                randomPoint -= item.probability;
            }

            return probabilityBehaviors[probabilityBehaviors.Count - 1].type;
        }
    }
}