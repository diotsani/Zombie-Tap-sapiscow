using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sapi.ZombieTap.Scenes
{
    public class SceneReloader : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}