using System;
using System.Collections.Generic;
using Core.Entities;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private List<ObjectPoolType> _objectPoolTypes;

        private DiContainer _diContainer;
        private Dictionary<TypeEntity, ObjectPoolItem> _pool;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        private void Awake()
        {
            InitPool();
        }

        private void InitPool()
        {
            _pool = new Dictionary<TypeEntity, ObjectPoolItem>();

            foreach (var objectPoolType in _objectPoolTypes)
            {
                var container = new GameObject(objectPoolType.type.ToString());
                container.transform.parent = transform;

                _pool[objectPoolType.type] = new ObjectPoolItem(container.transform);

                for (var i = 0; i < objectPoolType.amount; i++)
                {
                    var entity = InstantiateObject(objectPoolType.type, container.transform);
                    _pool[objectPoolType.type].pool.Enqueue(entity);
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private GameObject InstantiateObject(TypeEntity type, Transform parent)
        {
            var prefab = _objectPoolTypes.Find(x => x.type == type).prefab;
            var obj = _diContainer.InstantiatePrefab(prefab,parent);
            obj.SetActive(false);
            return obj;
        }

        public GameObject GetObject(TypeEntity type, bool expandable = false)
        {
            if (_pool[type].pool.Count > 0)
            {
                var obj = _pool[type].pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
        
            if (expandable)
            {
                var obj = InstantiateObject(type, _pool[type].Container);
                obj.SetActive(true);
                return obj;
            }

            return null;
        }

        public void ReturnObject(GameObject obj, TypeEntity typeEntity)
        {
            _pool[typeEntity].pool.Enqueue(obj);
            obj.SetActive(false);
        }

        public int GetAmountObject(TypeEntity type)
        {
            return _objectPoolTypes.Find(x => x.type == type).amount;
        }
    }

    [Serializable]
    public class ObjectPoolType
    {
        public TypeEntity type;
        public GameObject prefab;
        public int amount;

        public ObjectPoolType(TypeEntity type, GameObject prefab, int amount)
        {
            this.type = type;
            this.prefab = prefab;
            this.amount = amount;
        }
    }

    public class ObjectPoolItem
    {
        public Transform Container { get; private set; }

        public Queue<GameObject> pool;

        public ObjectPoolItem(Transform container)
        {
            Container = container;
            pool = new Queue<GameObject>();
        }
    }
}