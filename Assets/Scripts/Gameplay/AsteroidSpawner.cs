using System.Collections;
using UnityEngine;

namespace MeteorShooter
{
    public sealed class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] asteroidPrefabs;
        [SerializeField] private float initialInterval = 1.05f;
        [SerializeField] private float minimumInterval = 0.38f;
        [SerializeField] private float spawnY = 6.25f;
        private Coroutine routine;

        private void Start() => routine = StartCoroutine(SpawnRoutine());

        public void StopSpawning()
        {
            if (routine != null) StopCoroutine(routine);
            routine = null;
        }

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(0.7f);
            while (GameManager.Instance != null && !GameManager.Instance.IsGameOver)
            {
                float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
                float x = Random.Range(-halfWidth + 0.55f, halfWidth - 0.55f);
                GameObject prefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
                Instantiate(prefab, new Vector3(x, spawnY, 0f), Quaternion.identity);
                float difficulty = GameManager.Instance.Difficulty;
                float interval = Mathf.Lerp(initialInterval, minimumInterval, difficulty) * Random.Range(0.9f, 1.12f);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
