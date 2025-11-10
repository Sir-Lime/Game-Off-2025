using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject screenTransition;
    [SerializeField] private float transitionTime;
    private Animator transition;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        transition = screenTransition.GetComponent<Animator>();
    }

    public void LoadNextScene()
    {
        screenTransition.SetActive(true);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(nextScene);
    }
}
