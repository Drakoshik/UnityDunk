using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameLogic.Pool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly bool _autoExpand;
        private readonly GameObject _container;
        private List<T> _pool;

        public ObjectPool(T prefab, int count, Transform mainContainer, bool autoExpand)
        {
            this._prefab = prefab;
            _container = Object.Instantiate(new GameObject(), mainContainer);
            _container.name = prefab.name;
            this._autoExpand = autoExpand;
            this.CreatePool(count, _container.transform);
        }

        private void CreatePool(int count, Transform container)
        {
            this._pool = new List<T>();

            for (var i = 0; i < count; i++)
            {
                this.CreateObject(container);
            }

        }

        private T CreateObject(Transform container, bool isActive = false)
        {
            var createdObject = Object.Instantiate(this._prefab, container);
            createdObject.gameObject.SetActive(isActive);
            this._pool.Add(createdObject);
            return createdObject;
        }


        public T GetFreeElement()
        {
            foreach (var obj in _pool.Where(obj =>
                         !obj.gameObject.activeInHierarchy))
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
            
        
            if (this._autoExpand)
                return this.CreateObject(_container.transform, true);
        
            throw new Exception("No free elements");
        }

        public void HideAllElements()
        {
            foreach (var obj in _pool)
            {
                obj.gameObject.SetActive(false);
            }
        }

        public List<T> GetAllElements()
        {
            return _pool;
        }
    

    }
}
