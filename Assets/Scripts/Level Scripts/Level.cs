using System.Collections;
using NUnit.Framework.Constraints;
using playerController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    public void LoadNextScene()
    {
        if(collectible.IsCollected) {
            scenePanel.SetActive(true);
            StartCoroutine(LoadLevel());
        }
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
        } 
    }
    
    IEnumerator Respawn()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;

        yield return new WaitForSeconds(0.3f);

        respawnPanel.SetActive(true);
        
        //playerSprite.enabled = false;
        
        yield return new WaitForSeconds(respawnTime);

        respawnTransition.SetTrigger("Respawn");
        player.transform.position = spawnPoint.transform.position;
        
        playerRb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
         
        respawnPanel.SetActive(false);   

        yield return new WaitForSeconds(0.3f);

        isDead = false;
        playerAnim.SetTrigger("onRespawn");
    }
}
