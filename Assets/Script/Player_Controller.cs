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
    public CameraShake shake;
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

            float targetSpeedX = moveX * speed * Time.deltaTime;
            float speedDifX = targetSpeedX - rb.velocity.x;
            float accelRateX = (Mathf.Abs(targetSpeedX) < 0.01f) ? Acceleration : Deceleration;
            float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

            float targetSpeedY = moveY * speed * Time.deltaTime;
            float speedDifY = targetSpeedY - rb.velocity.y;
            float accelRateY = (Mathf.Abs(targetSpeedY) < 0.01f) ? Acceleration : Deceleration;
            float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);

            var Movement = new Vector3(movementX, movementY, 0);

            rb.AddForce(Movement);

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
            if (!collision.gameObject.GetComponent<Bullet>().Friendly)
            {
                life -= 1;
                Destroy(collision.gameObject);
                anim.SetTrigger("Hit");
                rb.velocity = Vector3.zero;
                shake.shaking = true;
            }
            else Destroy(collision.gameObject);
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
