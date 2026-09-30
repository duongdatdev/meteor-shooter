using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeteorShooter
{
    public sealed class MainMenuController : MonoBehaviour
    {
        public void Play()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Gameplay");
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
