using System.Collections;
using CodeBase.Infrastructure.Services.Pool;
using UnityEngine;

namespace CodeBase.Logic
{
    public class CoroutineFX : MonoBehaviour
    {
        private readonly float LifeTimeFX = 1.5f;

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