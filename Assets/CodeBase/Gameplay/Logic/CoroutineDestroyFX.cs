using System.Collections;
using CodeBase.Infrastructure.Services.Pool;
using UnityEngine;

namespace CodeBase.Gameplay.Logic
{
    public class CoroutineDestroyFX : MonoBehaviour
    {
        private const float LifeTimeFX = 1.5f;

        private void OnEnable() => 
            StartCoroutine(DespawnFX());

        private void OnDisable() => 
            StopCoroutine(DespawnFX());

        private IEnumerator DespawnFX()
        {
            yield return new WaitForSeconds(LifeTimeFX);
            ObjectPool.Despawn(gameObject);
        }
    }
}