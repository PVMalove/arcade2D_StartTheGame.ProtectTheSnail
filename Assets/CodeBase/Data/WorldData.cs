using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public DiamondData DiamondData;

        public WorldData()
        {
            DiamondData = new DiamondData();
        }
    }
}