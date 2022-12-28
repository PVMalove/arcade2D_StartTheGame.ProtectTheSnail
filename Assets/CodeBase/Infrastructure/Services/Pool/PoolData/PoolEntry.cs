using UnityEngine;

namespace CodeBase.Infrastructure.Services.Pool.PoolData
{
    public class PoolEntry : MonoBehaviour
    {
        [SerializeField] private PoolPreset poolPreset;

        private void Awake() => 
            ObjectPool.InstallPoolItems(poolPreset);
    }
}