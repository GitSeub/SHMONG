using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float speed;
    [SerializeField] private float multiply;
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
    }

    public void PerfectBounce(float angle)
    {
        transform.localEulerAngles = new Vector3(0, 0, angle);
        _rb.velocity = Vector3.zero;
        _rb.AddForce(transform.right * speed * multiply * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
