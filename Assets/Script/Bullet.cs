using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Danger;
    private Rigidbody _rb;
    public int dmg;
    [SerializeField] private float speed;
    [SerializeField] private float multiply;
    public bool Friendly;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.right * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Bounce(float angle)
    {
        transform.localEulerAngles = new Vector3(0, 0, angle);
        _rb.velocity = Vector3.zero;
        _rb.AddForce(transform.right * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        dmg = 1;
        Friendly = true;
    }

    public void PerfectBounce(float angle)
    {
        transform.localEulerAngles = new Vector3(0, 0, angle);
        _rb.velocity = Vector3.zero;
        _rb.AddForce(transform.right * speed * multiply * Time.fixedDeltaTime, ForceMode.Impulse);
        dmg = 2;
        Friendly = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4)
        {
            Destroy(gameObject);
        }
    }
}
