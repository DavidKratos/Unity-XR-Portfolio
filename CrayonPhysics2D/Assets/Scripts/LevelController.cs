using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrayonPhysics
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private string[] levelSceneNames = {"Level01", "Level02", "Level03", "Level04", "Level05"};
        public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        public void NextLevel()
        {
            string current = SceneManager.GetActiveScene().name;
            int index = System.Array.IndexOf(levelSceneNames, current);
            if (index >= 0 && index < levelSceneNames.Length - 1)
                SceneManager.LoadScene(levelSceneNames[index + 1]);
            else Debug.Log("All five levels completed!");
        }
    }
}
