using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Danger;
    private Rigidbody _rb;
    public int dmg;
    [SerializeField] private float speed;
    [SerializeField] private float multiply;
    public bool Friendly;
    public Vector3 CurrentVel;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        CurrentVel = transform.right * speed;
        _rb.AddForce(CurrentVel, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Bounce( Vector3 ReflectedVec)
    {
        _rb.velocity = Vector3.zero;
        CurrentVel = ReflectedVec;
        _rb.AddForce(CurrentVel, ForceMode.Impulse);
        dmg = 1;
        Friendly = true;
        gameObject.layer = 7;
    }

    public void PerfectBounce(Vector3 ReflectedVec)
    {
        _rb.velocity = Vector3.zero;
        CurrentVel = ReflectedVec;
        _rb.AddForce(CurrentVel * multiply, ForceMode.Impulse);
        dmg = 2;
        Friendly = true;
        gameObject.layer = 7;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
