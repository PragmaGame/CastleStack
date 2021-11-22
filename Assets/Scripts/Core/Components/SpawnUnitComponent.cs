using System;
using System.Collections;
using Core.Entities;
using UnityEngine;
using Zenject;

namespace Core.Components
{
    public class SpawnUnitComponent : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private TypeEntity _typeSpawnEntity;
        
        private AffiliationComponent _affiliationComponent;
        private WaitForSeconds _spawnIntervalWait;
        private PoolManager _poolManager;
        private EntityListCollection<PlayerEntity> _playersCollections;
        private OfficerComponent _officerComponent;
        private Coroutine _spawnCoroutine;

        private void Awake()
        {
            _spawnIntervalWait = new WaitForSeconds(_spawnInterval);
        }

        [Inject]
        private void Construct(AffiliationComponent affiliationComponent, EntityCollections entityCollections, PoolManager poolManager)
        {
            _affiliationComponent = affiliationComponent;
            
            _playersCollections = entityCollections.PlayersCollection;
            _poolManager = poolManager;
        }

        private void OnEnable()
        {
            _affiliationComponent.ChangedAffiliationEvent += OnChangedAffiliation;
        }

        private void OnDisable()
        {
            _affiliationComponent.ChangedAffiliationEvent -= OnChangedAffiliation;
        }

        private void OnChangedAffiliation(int id)
        {
            if (_playersCollections.TryGetFirstEntity(_affiliationComponent.ID, out PlayerEntity playerEntity))
            {
                _officerComponent = playerEntity.OfficerComponent;
                
                if (_spawnCoroutine != null)
                {
                    StopCoroutine(_spawnCoroutine);
                }

                _spawnCoroutine = StartCoroutine(Spawn());
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Spawn()
        {
            var a = 6;
            while (a != 0)
            {
                yield return _spawnIntervalWait;
                
                var solder = _poolManager.GetObject(_typeSpawnEntity);
                solder.GetComponent<AffiliationComponent>().SetAffiliation(_affiliationComponent.ID, true);
                solder.transform.SetPositionAndRotation(_spawnPoint.position,Quaternion.identity);
                _officerComponent.AddSolder(solder.GetComponent<SolderLogicComponent>());
                a--;
            }
        }
    }
}