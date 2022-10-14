using UnityEngine;
using UnityEngine.SceneManagement;

namespace BubblePopper
{
    public class SceneReloader : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}