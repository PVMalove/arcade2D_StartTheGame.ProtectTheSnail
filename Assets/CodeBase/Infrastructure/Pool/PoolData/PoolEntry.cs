using UnityEngine;

namespace CodeBase.Infrastructure.Pool.PoolData
{
    public class PoolEntry : MonoBehaviour
    {
        [SerializeField] private PoolPreset poolPreset;

        private void Awake() => 
            ObjectPool.InstallPoolItems(poolPreset);
    }
}