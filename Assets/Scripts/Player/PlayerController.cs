using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MeteorShooter
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7.5f;
        [SerializeField] private float fireCooldown = 0.18f;
        [SerializeField] private float invulnerabilitySeconds = 1.35f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;

        private SpriteRenderer spriteRenderer;
        private Camera mainCamera;
        private float nextShotTime;
        private bool invulnerable;
        private bool dead;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (dead || GameManager.Instance == null || GameManager.Instance.IsPaused || GameManager.Instance.IsGameOver) return;
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null) return;

            float horizontal = 0f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) horizontal -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) horizontal += 1f;
            transform.position += Vector3.right * (horizontal * moveSpeed * Time.deltaTime);
            ClampToView();

            if (keyboard.spaceKey.isPressed && Time.time >= nextShotTime)
            {
                nextShotTime = Time.time + fireCooldown;
                Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            }
        }

        public bool TryTakeHit()
        {
            if (dead || invulnerable || GameManager.Instance == null || GameManager.Instance.IsGameOver) return false;
            if (GameManager.Instance.LoseLife())
            {
                dead = true;
                spriteRenderer.enabled = false;
                GameManager.Instance.TriggerGameOver(transform.position);
            }
            else
            {
                StartCoroutine(InvulnerabilityRoutine());
            }
            return true;
        }

        private void ClampToView()
        {
            if (mainCamera == null) return;
            float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float spriteHalfWidth = spriteRenderer.bounds.extents.x;
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -halfWidth + spriteHalfWidth + 0.08f, halfWidth - spriteHalfWidth - 0.08f);
            transform.position = position;
        }

        private IEnumerator InvulnerabilityRoutine()
        {
            invulnerable = true;
            float elapsed = 0f;
            while (elapsed < invulnerabilitySeconds)
            {
                elapsed += 0.1f;
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.1f);
            }
            spriteRenderer.enabled = true;
            invulnerable = false;
        }
    }
}
