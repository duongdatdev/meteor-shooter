using UnityEngine;

namespace MeteorShooter
{
    public sealed class FxLifetime : MonoBehaviour
    {
        [SerializeField] private float lifetime = 0.55f;
        private void Start() => Destroy(gameObject, lifetime);
    }
}
