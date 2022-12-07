using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
    [DisallowMultipleComponent]
    public class Poolable : MonoBehaviour
    {
        public Pool Pool { get; private set; }
        public GameObject Prefab { get; private set; }
        public bool IsActive { get; private set; }
        
        private bool isSetup;

        public void Setup(Pool pool, GameObject prefab, bool active)
        {
            if (isSetup)
                return;
            
            if (prefab == null)
                throw new NullReferenceException(nameof(prefab), null);

            gameObject.SetActive(active);

            IsActive = active;
            Pool = pool;
            Prefab = prefab;

            isSetup = true;
        }
    }
}