using System.Collections;
using UnityEngine;

namespace MeteorShooter
{
    public sealed class CameraShake : MonoBehaviour
    {
        private Coroutine routine;
        private Vector3 origin;

        private void Awake() => origin = transform.localPosition;

        public void Shake(float duration, float magnitude)
        {
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(ShakeRoutine(duration, magnitude));
        }

        private IEnumerator ShakeRoutine(float duration, float magnitude)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                transform.localPosition = origin + (Vector3)(Random.insideUnitCircle * magnitude);
                yield return null;
            }
            transform.localPosition = origin;
            routine = null;
        }
    }
}
