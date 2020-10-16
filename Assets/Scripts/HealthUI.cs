using UnityEngine;
public class HealthUI : MonoBehaviour
{
    public GameObject Heart;
    public GameObject Heart2;
    public GameObject Heart3;

    GameObject player;
    PlayerMovement pm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
        Heart.SetActive(true);
        Heart2.SetActive(true);
        Heart3.SetActive(true);
        
    }

    void Update()
    {
        LoseHeart();
        
    }

    void LoseHeart()
    {
        if(pm.life == 2)
        {
            Heart3.SetActive(false);
        }
        if (pm.life == 1)
        {
            Heart2.SetActive(false);
        }
        if (pm.life == 0)
        {
            Heart.SetActive(false);
        }
    }
}
