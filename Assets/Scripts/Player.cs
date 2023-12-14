using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody2D rb;
    private bool canJump;

    private void Start()
    {
        canJump = false;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    private void Update()
    {
        if (!canJump) return;
        if(Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.Play("Click");
            rb.velocity = (Vector2.up + Vector2.right) * speed;
            GameManager.instance.UpdateScore();
        }
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                AudioManager.instance.Play("Click");
                rb.velocity = (Vector2.up + Vector2.right) * speed;
                GameManager.instance.UpdateScore();
            }
        }
    }

    public void GameStart()
    {
        canJump = true;
        rb.simulated = true;
        rb.velocity = (Vector2.up + Vector2.right) * speed;
        Camera.main.GetComponent<CameraFollow>().StartFollowing();
    }

    public void GameOver()
    {
        AudioManager.instance.Play("Lose");
        rb.simulated = false;
        canJump = false;
        Camera.main.GetComponent<CameraFollow>().StopFollowing();
    }
}
