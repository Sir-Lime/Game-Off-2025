using System.Collections;
using playerController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }
    [Header("Level Specific Settings")]
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private string nextScene;

    [Header("Transition Settings")]
    [SerializeField] private GameObject scenePanel;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private GameObject respawnPanel;
    [SerializeField] private float respawnTime = 0.5f;

    private Animator sceneTransition;
    private Animator respawnTransition;
    private GameObject player;

    // Singleton Pattern so there is always a single instance of this LevelManager in a scene
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
        sceneTransition = scenePanel.GetComponent<Animator>();
        respawnTransition = respawnPanel.GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
    }

    public void LoadNextScene()
    {
        scenePanel.SetActive(true);
        StartCoroutine(LoadLevel());
    }
    
    IEnumerator LoadLevel()
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nextScene);
    }

    public void KillPlayer()
    {
        respawnPanel.SetActive(true);
        StartCoroutine(Respawn());
    }
    
    IEnumerator Respawn()
    {
        player.SetActive(false);
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        
        yield return new WaitForSeconds(respawnTime);

        respawnTransition.SetTrigger("Respawn");
        player.transform.position = spawnPoint.transform.position;
        player.SetActive(true);
        respawnPanel.SetActive(false);   
    }
}
