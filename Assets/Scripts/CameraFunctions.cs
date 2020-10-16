using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    public GameObject Player;
    public Vector2 FollowOffset;
    private Vector2 Threshold;
    public float speed;
    float GameOverOffset = -5.0f;
    private Rigidbody2D rb;
    public bool GameOver;

    void Start()
    {
        Threshold = calculateThreshold();
        rb = Player.GetComponent<Rigidbody2D>();
    }

   
    private void FixedUpdate()
    {

        if (Player.transform.position.y < GameOverOffset)
        {
            OffScreen();

        }
        else
        {
            FollowPlayer();
        }

    }


    void FollowPlayer()
    {
        Vector2 followObject = Player.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * followObject.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * followObject.y);

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= Threshold.x)
        {
            newPosition.x = followObject.x;
        }

        if (Mathf.Abs(yDifference) >= Threshold.y)
        {
            newPosition.y = followObject.y;
        }
        if (rb != null)
        {
            float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }

    }
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= FollowOffset.x;
        t.y -= FollowOffset.y;
        return t;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    private void OffScreen()
    {
        if(Player.transform.position.y < GameOverOffset - 2)
        {
            GameOver = true;
        }
    }


    /////////////////////////////*Source for Camera Following Player*///////////////////////////////////////
    ///Mario Style Camera Follow Tutorial; https://www.youtube.com/watch?v=GTxiCzvYNOc&ab_channel=PressStart
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
}
