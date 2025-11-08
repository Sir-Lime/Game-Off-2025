using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public void LoadNextLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        if (level >= SceneManager.sceneCountInBuildSettings) level = 0;
        StartCoroutine(LoadLevel(level));

    }
    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator LoadLevel(int level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }
}
