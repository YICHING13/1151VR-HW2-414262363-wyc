using UnityEngine;

public class ButterController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float acceleration = 35f;
    public float deceleration = 20f;

    public float[] jumpForces = {20f, 23f};
    
    public float fallForce = 16f;
    public SpriteRenderer KitchenBackground;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool isGrounded;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (isGameOver)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");

        // 左右移動：加入加速與減速慣性
        float targetSpeed = moveX * moveSpeed;

        float speedChange = moveX != 0
            ? acceleration
            : deceleration;

        float newSpeed = Mathf.MoveTowards(
            rb.linearVelocity.x,
            targetSpeed,
            speedChange * Time.deltaTime
        );

        rb.linearVelocity = new Vector2(
            newSpeed,
            rb.linearVelocity.y
        );

        // 普通跳躍：方向鍵上
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForces[0]
            );
            isGrounded = false;
        }

        // 強力跳躍：空白鍵
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
            jumpForces[1]
            );
            isGrounded = false;
        }

        // 方向鍵下：加速下降
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.down * fallForce);
        }

        // 掉出背景範圍：回到起點
        if (KitchenBackground != null &&
            transform.position.y < KitchenBackground.bounds.min.y)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            transform.position = startPosition;
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    public void WinGame()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}



