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
    public GameObject shield;
    public GameObject vaisso;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var force = new Vector3(moveX, moveY, 0).normalized;

            float targetSpeedX = force.x * speed * Time.deltaTime;
            float speedDifX = targetSpeedX - rb.velocity.x;
            float accelRateX = (Mathf.Abs(targetSpeedX) < 0.01f) ? Acceleration : Deceleration;
            float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

            float targetSpeedY = force.y * speed * Time.deltaTime;
            float speedDifY = targetSpeedY - rb.velocity.y;
            float accelRateY = (Mathf.Abs(targetSpeedY) < 0.01f) ? Acceleration : Deceleration;
            float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);

            var Movement = new Vector3(movementX, movementY, 0);
            Movement = Vector3.ClampMagnitude(Movement, speed);
            rb.AddForce(Movement);

            if (life <= 0)
            {
                dead = true;
            }
        }
        if (dead)
        {
            Destroy(shield);
            Destroy(vaisso);
            gameObject.GetComponent<BoxCollider>().enabled = false;
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
}
