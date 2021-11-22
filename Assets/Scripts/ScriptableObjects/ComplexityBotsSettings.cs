using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core;
using Core.Entities;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ComplexityBotsSettings", menuName = "ComplexityBotsSettings")]
    public class ComplexityBotsSettings : ScriptableObject
    {
        [SerializeField] private List<LevelsBehavior> _levelsBehaviors;

        public ReadOnlyCollection<LevelsBehavior> LevelsBehaviors => _levelsBehaviors.AsReadOnly();

        public Complexity GetComplexityBot(int levelBehavior, int levelComplexity)
        {
            return _levelsBehaviors[levelBehavior].ComplexitiesBots[levelComplexity];
        }

        public LevelsBehavior GetLevelBehavior(int levelBehavior)
        {
            return _levelsBehaviors[levelBehavior];
        }
    }

    [Serializable]
    public class Complexity
    {
        [SerializeField] private List<ProbabilityBehavior> _probabilityBehaviors;
        
        public ReadOnlyCollection<ProbabilityBehavior> ProbabilityBehaviors => _probabilityBehaviors.AsReadOnly();
    }

    [Serializable]
    public class LevelsBehavior
    {
        [SerializeField] private List<Complexity> _complexitiesBots;
        [SerializeField] private List<PrioritySelectionTarget> _behaviorPriorityTypes;
        
        public ReadOnlyCollection<Complexity> ComplexitiesBots => _complexitiesBots.AsReadOnly();
        public ReadOnlyCollection<PrioritySelectionTarget> BehaviorPriorityTypes => _behaviorPriorityTypes.AsReadOnly();
    }

    [Serializable]
    public class ProbabilityBehavior
    {
        public BehaviorOfficersType type;
        public float probability;
    }

    [Serializable]
    public class PrioritySelectionTarget
    {
        public BehaviorOfficersType type;
        public List<BehaviorPriorityTypes> priorityTargetSelection;
    }
}