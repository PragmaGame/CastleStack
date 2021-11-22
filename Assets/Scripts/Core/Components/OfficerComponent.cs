using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Components
{
    public class OfficerComponent : MonoBehaviour
    {
        [SerializeField] private Transform _collectionСenter;
        [SerializeField] private float _radiusCollection;
        
        private List<Vector3> _locationOffsetPoints;
        private LinkedList<SolderLogicComponent> _solderLogicComponents;

        private void Awake()
        {
            _locationOffsetPoints = new List<Vector3>();
            _solderLogicComponents = new LinkedList<SolderLogicComponent>();
        }

        private void RecalculationOffsetLocationPoints()
        {
            var countSection = _solderLogicComponents.Count;
            
            for (var i = 1; i <= countSection; i++)
            {
                var posAlpha = 2 * Mathf.PI * i / countSection;

                var offsetPoint = new Vector3(Mathf.Cos(posAlpha) * _radiusCollection, 0,
                    Mathf.Sin(posAlpha) * _radiusCollection);

                if (_locationOffsetPoints.Count < i)
                {
                    _locationOffsetPoints.Add(offsetPoint);
                }
                else
                {
                    _locationOffsetPoints[i - 1] = offsetPoint;
                }
            }
        }

        private void DistributeTargetLocation()
        {
            var index = 0;
            
            for (var node = _solderLogicComponents.First; node != null; node = node.Next)
            {
                node.Value.OffsetLocationPoint = _locationOffsetPoints[index];
                index++;
            }
        }

        public void AddSolder(SolderLogicComponent solderLogicComponent)
        {
            solderLogicComponent.OfficerTransform = _collectionСenter;
            _solderLogicComponents.AddLast(solderLogicComponent);
            RecalculationOffsetLocationPoints();
            DistributeTargetLocation();
        }

        public void RemoveSolder(SolderLogicComponent solderLogicComponent)
        {
            _solderLogicComponents.Remove(solderLogicComponent);
            RecalculationOffsetLocationPoints();
            DistributeTargetLocation();
        }
    }
}