using UnityEngine;

namespace MeteorShooter
{
    public sealed class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private Transform[] panels;
        [SerializeField] private float scrollSpeed = 0.22f;
        [SerializeField] private float panelHeight = 10.82f;

        private void Awake()
        {
            Camera camera = Camera.main;
            if (camera == null || panels == null || panels.Length == 0) return;
            SpriteRenderer renderer = panels[0].GetComponent<SpriteRenderer>();
            if (renderer == null || renderer.sprite == null) return;
            float cameraHeight = camera.orthographicSize * 2f;
            float cameraWidth = cameraHeight * camera.aspect;
            float scale = Mathf.Max(cameraWidth / renderer.sprite.bounds.size.x, cameraHeight / renderer.sprite.bounds.size.y);
            panelHeight = renderer.sprite.bounds.size.y * scale;
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].localScale = new Vector3(scale, scale, 1f);
                panels[i].position = new Vector3(0f, i * panelHeight, 2f);
            }
        }

        private void Update()
        {
            foreach (Transform panel in panels)
            {
                panel.position += Vector3.down * (scrollSpeed * Time.deltaTime);
                if (panel.position.y <= -panelHeight) panel.position += Vector3.up * (panelHeight * 2f);
            }
        }
    }
}
