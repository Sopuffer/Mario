using UnityEngine;
using UnityEngine.SceneManagement;

public class StateChange : CameraFunctions
{
    public GameObject GameOverObject;
    public GameObject RestartButtonObject;
    public GameObject GameWonObject;
    GameObject flag;
    GameObject player;
    PlayerMovement pm;
    Flag f;
    public bool PlayerLostGame;
    public bool PlayerWonGame;
    
    void Awake()
    {
        GameOverObject.SetActive(false);
        RestartButtonObject.SetActive(false);
        GameWonObject.SetActive(false);
        flag = GameObject.FindGameObjectWithTag("Flag");
        f = flag.GetComponent<Flag>();
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (GameOver || pm.PlayerDefeated)
        {
            PlayerLost();
        }

        if (f.theGameIsComplete)
        {

            PlayerWon();
            GameWonObject.SetActive(true);
        }

        RestartButtonActivated();
    }

    public void PlayerLost()
    {
        GameOverObject.SetActive(true);
        PlayerLostGame = true;
    }

    void PlayerWon()
    {
        GameWonObject.SetActive(true);
        PlayerWonGame = true;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RestartButtonActivated()
    {
        if(PlayerWonGame || PlayerLostGame)
        {
            RestartButtonObject.SetActive(true);
            Time.timeScale = 0.0f;

        }
    }
   
}
