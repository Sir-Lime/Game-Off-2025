using System.Collections;
using NUnit.Framework.Constraints;
using playerController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }
    [Header("Level Specific Settings")]
    [SerializeField] private GameObject spawnPoint;
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

    // Singleton Pattern so there is always a single instance of this LevelManager in a scene
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
           //DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        sceneTransition = scenePanel.GetComponent<Animator>();
        respawnTransition = respawnPanel.GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
        playerAnim = player.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        isDead = false;


        if(nextScene == "null" || nextScene == "")
        {
            nextScene = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        }
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

        yield return new WaitForSeconds(0.3f);

        playerAnim.SetTrigger("onRespawn");
    }

    public void KillPlayer()
    {
        if(!isDead)
        {
            isDead = true;
            playerAnim.SetTrigger("onDeath");
            StartCoroutine(Respawn()); 
            SFXScript.instance.deathSFX();
        } 
    }
    
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.3f);

        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        respawnPanel.SetActive(true);
        
        //playerSprite.enabled = false;
        
        yield return new WaitForSeconds(respawnTime);

        respawnTransition.SetTrigger("Respawn");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerAnim.SetTrigger("onRespawn");
    }
}
