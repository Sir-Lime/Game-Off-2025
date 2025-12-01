using System.Collections;
using NUnit.Framework.Constraints;
using playerController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.Tilemaps;
using System.Linq;


public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }
    [Header("Level Specific Settings")]
    [SerializeField] private Collectible collectible;
    [SerializeField] private string nextScene;

    [Header("Transition Settings")]
    [SerializeField] private GameObject scenePanel;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private GameObject respawnPanel;
    [SerializeField] private float respawnTime = 0.5f;
    private Animator sceneTransition;
    private Animator respawnTransition;
    private GameObject player;
    private Animator playerAnim;
    private Rigidbody2D playerRb;
    private Camera mainCamera;
    private bool isDead = false;
    private Vector3 playerOriginalPos;
    private Vector3 collectibleOriginalPos;
    public float levelStartTime;
    private IActivatable[] activators;

    // Singleton Pattern so there is always a single instance of this LevelManager in a scene
    void Awake()
    {
        collectible = GameObject.FindWithTag("Tape").GetComponent<Collectible>();
        levelStartTime = TimerManager.Instance.GetTime(false);
        TimerManager.Instance.startTime = levelStartTime;
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
           //DontDestroyOnLoad(gameObject);

        // Query gameObjects that implement that IActivable interface
        activators = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IActivatable>().ToArray();
    }

    private void Start() {
        sceneTransition = scenePanel.GetComponent<Animator>();
        respawnTransition = respawnPanel.GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
        playerAnim = player.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        isDead = false;
        collectibleOriginalPos = collectible.transform.position;
        playerOriginalPos = player.transform.position;

        if(nextScene == "null" || nextScene == "")
        {
            nextScene = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    public void LoadNextScene()
    {
        scenePanel.SetActive(true);
        StartCoroutine(LoadLevel());
        if (nextScene == "mainMenuScene") 
        {
            TimerManager.Instance.StopAndSave();

        }
    }
    
    private IEnumerator LoadLevel()
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nextScene);

        yield return new WaitForSeconds(0.3f);

        playerAnim.SetTrigger("onRespawn");
    }

    public void ReloadLevel()
    {
        foreach(var obj in activators)
        {
            obj.ResetState();
        }
    }

    public void KillPlayer()
    {
        if(!isDead)
        {
            playerAnim.SetTrigger("onDeath");
            SFXScript.instance.deathSFX();
            isDead = true;
            StartCoroutine(Respawn()); 
        } 
    }
    
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.3f);

        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        respawnPanel.SetActive(true);
        
        yield return new WaitForSeconds(respawnTime);

        player.transform.position = playerOriginalPos;
        mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, mainCamera.transform.position.z);
        collectible.IsCollected = false;
        collectible.transform.position = collectibleOriginalPos;
        
        respawnTransition.SetTrigger("Respawn");
        playerRb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(0.3f);

        playerAnim.SetTrigger("onRespawn");
        respawnPanel.SetActive(false);
        isDead = false;
        TimerManager.Instance.startTime = TimerManager.Instance.GetTime(true) -  levelStartTime;
    }
}
