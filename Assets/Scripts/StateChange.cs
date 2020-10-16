using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class StateChange : CameraFunctions
{
    public GameObject GameOverObject;
    public GameObject RestartButtonObject;
    public GameObject GameWonObject;
    GameObject flag;
    GameObject player;
    PlayerMovement pm;
    FlagScript fs;
    public bool PlayerLostGame;
    public bool PlayerWonGame;
    
    void Awake()
    {
        GameOverObject.SetActive(false);
        RestartButtonObject.SetActive(false);
        GameWonObject.SetActive(false);
        flag = GameObject.FindGameObjectWithTag("Flag");
        fs = flag.GetComponent<FlagScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (GameOver || pm.PlayerDefeated)
        {
            PlayerLost();
        }

        if (fs.theGameIsComplete)
        {

            PlayerWon();
            GameWonObject.SetActive(true);
        }

        RestartButtonActivated();
    }

    void PlayerLost()
    {
        GameOverObject.SetActive(true);
        PlayerLostGame = true;
        Time.timeScale = 0.0f;
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

        }
    }
   
}
