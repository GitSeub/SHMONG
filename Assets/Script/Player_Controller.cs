using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_Controller : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float Acceleration;
    public float Deceleration;
    public float velPower;
    public float life = 3;
    public Animator anim;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var force = new Vector3(moveX, moveY, 0).normalized;

            float targetSpeedX = moveX * speed;
            float speedDifX = targetSpeedX - rb.velocity.x;
            float accelRateX = (Mathf.Abs(targetSpeedX) < 0.01f) ? Acceleration : Deceleration;
            float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

            float targetSpeedY = moveY * speed;
            float speedDifY = targetSpeedY - rb.velocity.y;
            float accelRateY = (Mathf.Abs(targetSpeedY) < 0.01f) ? Acceleration : Deceleration;
            float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);



            rb.AddForce(movementX * Vector2.right);
            rb.AddForce(movementY * Vector2.up);
        }


        if (Input.GetButtonDown("Fire2"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            life -= 1;
            Destroy(collision.gameObject);
            anim.SetTrigger("Hit");
        }
    }

    void Death()
    {
        if (life <= 0)
        {
            dead = true; 
        }
    }
}
