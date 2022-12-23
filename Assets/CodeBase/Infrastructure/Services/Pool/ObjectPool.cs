using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Pool.PoolData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Services.Pool
{
    public static class ObjectPool
    {
        private static readonly List<Pool> AllPools = new(capacity: 64);

        public static void InstallPoolItems(PoolPreset poolPreset)
        {
            foreach (PoolItem poolItem in poolPreset.PoolItems)
            {
                GameObject prefab = poolItem.Prefab;
                Pool pool = GetPoolByPrefab(prefab);

                pool.PopulatePool(poolItem.Size);
            }
        }

        public static void Spawn(GameObject toSpawn, Vector3 position = default, Quaternion rotation = default) =>
            DefaultSpawn(toSpawn, position, rotation, null, false);

        public static void Despawn(GameObject toDespawn, float delay = 0f) =>
            DefaultDespawn(toDespawn, delay);


        public static void DestroyAllPools()
        {
            Pool[] pools = AllPools.ToArray();

            foreach (Pool pool in pools)
                DestroyPool(pool);

            AllPools.Clear();
        }

        public static void DestroyPool(Pool pool)
        {
            if (pool == null)
            {
                Debug.LogWarning("Pool is null!");
                return;
            }

            foreach (Poolable poolable in pool.Poolables)
                Object.Destroy(poolable.gameObject);

            Object.Destroy(pool.gameObject);

            AllPools.Remove(pool);
        }


        public static Pool GetPoolByPrefab(GameObject prefab)
        {
            var count = AllPools.Count;

            for (int i = 0; i < count; i++)
                if (AllPools[i].Prefab == prefab)
                    return AllPools[i];

            return CreateNewPool(prefab);
        }


        private static Pool CreateNewPool(GameObject prefab)
        {
            GameObject poolParent = new GameObject($"[Pool] {prefab.name}");
            Pool newPool = poolParent.AddComponent<Pool>();

            newPool.Setup(prefab, poolParent.transform);

            AllPools.Add(newPool);

            return newPool;
        }


        private static GameObject DefaultSpawn(GameObject prefab, Vector3 position, Quaternion rotation,
            Transform parent, bool worldPositionStays)
        {
            Pool pool = GetPoolByPrefab(prefab);
            Poolable freePoolable = pool.GetFreeObject();
            GameObject gameObject = freePoolable.gameObject;
            parent = pool.PoolablesParent;

            gameObject.SetActive(true);

            SetupTransform(freePoolable.transform, position, rotation, parent, worldPositionStays);

            return gameObject;
        }


        private static void DefaultDespawn(GameObject toDespawn, float delay = 0f)
        {
            if (toDespawn.TryGetComponent(out Poolable poolable))
            {
                Pool pool = poolable.Pool;

                if (pool != null)
                {
                    toDespawn.SetActive(false);
                    toDespawn.transform.SetParent(pool.PoolablesParent);

                    pool.IncludePoolable(poolable);
                }
                else
                    Object.Destroy(toDespawn);
            }
            else
            {
                Debug.LogError($"{toDespawn.name} was not spawned and will be destroyed!");
                Object.Destroy(toDespawn, delay);
            }
        }

        private static void SetupTransform(Transform transform, Vector3 position, Quaternion rotation,
            Transform parent = null, bool worldPositionStays = false)
        {
            transform.SetParent(parent, worldPositionStays);
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}