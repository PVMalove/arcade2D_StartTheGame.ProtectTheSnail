using UnityEngine;

namespace CodeBase.Infrastructure.Services.Pool.PoolData
{
    public class PoolEntry : MonoBehaviour
    {
        [SerializeField] private PoolPreset poolArrowPreset;
        [SerializeField] private PoolPreset poolFXPreset;

        private void Awake()
        {
            ObjectPool.InstallPoolItems(poolArrowPreset);
            ObjectPool.InstallPoolItems(poolFXPreset);
        }
    }
}