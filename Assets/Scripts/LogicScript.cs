using UnityEngine;
using UnityEngine.Rendering;
using PlayerController;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private ScriptableStats stats;
    [SerializeField] private LevelLoaderScript loader;
    [SerializeField] private playerController controller;
    public PlayerInput input;
    public float time = 0;
    public bool isAlive = true;
    public bool isDashing = false;


    void Awake()
    {
        input.SwitchCurrentActionMap("Player"); 
    }
    void Start()
    {
    }

    void Update()
    {
        time += Time.deltaTime;
        if (!isAlive)
        {
            GameOver();
        }
    }



    public void GameOver()
    {
        controller.enabled = false;
        controller.playerRB.bodyType = RigidbodyType2D.Static;
        Respawn();
    }
    public void Respawn()
    {
        isAlive = true; Time.timeScale = 1;
        loader.ReloadLevel();
    }
}
