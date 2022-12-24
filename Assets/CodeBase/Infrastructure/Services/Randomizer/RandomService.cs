using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure.Services.Randomizer
{
    class RandomService : IRandomService
    {
        public int Next(int minValue, int maxValue) => 
            Random.Range(minValue, maxValue);
    }
}