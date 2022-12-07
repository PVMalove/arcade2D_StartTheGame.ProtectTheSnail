using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool.PoolData
{
    [CreateAssetMenu(menuName = "Source/Pool/PoolPreset", fileName = "PoolPreset", order = 0)]
    public class PoolPreset : ScriptableObject
    {
        [SerializeField] private string poolName;
        [SerializeField] private List<PoolItem> poolItems = new List<PoolItem>(256);

        public IReadOnlyList<PoolItem> PoolItems => poolItems;

        public string GetName() => 
            poolName;
    }
}