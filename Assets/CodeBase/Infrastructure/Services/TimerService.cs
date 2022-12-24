using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public interface ITimerService : IService
    {
        float interval { get; set; }
        IEnumerator IntervalCounter();
    }

    public class TimerService : ITimerService
    {
        private float _minInterval = 0.65f;
        private float _maxInterval = 2f;
        private float _timeBetweenIntervals = 10f;
        private float _nonLowerInterval = 5f;
        private float _lowerInterval = 0.05f;

        public float interval { get; set;}

        public IEnumerator IntervalCounter()
        {
            yield return new WaitForSeconds(_nonLowerInterval);

            while (true)
            {
                yield return new WaitForSeconds(_timeBetweenIntervals);
                interval -= _lowerInterval;
                //interval = Math.Clamp(interval, _minInterval, _maxInterval);
            }
        }

    }

}