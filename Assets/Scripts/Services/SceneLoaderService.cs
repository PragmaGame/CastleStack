using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneLoaderService
    {
        private AsyncOperation asyncLoad;
        
        public void LoadLevel(int level)
        {
            asyncLoad = SceneManager.LoadSceneAsync(level);
        }

        public void RestartScene()
        {
            asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}