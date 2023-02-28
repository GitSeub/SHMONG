using UnityEngine;

public class ReflectPlayer : MonoBehaviour
{
    public GameObject Shield;
    private float aim;
    private float ParryTimer;
    public float ParryThreshHold;
    public float ParryMax;
    public Material ShieldMat;
    public Material ParryMat;
    public Material VoidMat;
    private bool ParryBool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aim = Shield.transform.eulerAngles.z;
        Parry();
    }

     void OnCollisionEnter(Collision collision)
     {
        if (collision.collider.CompareTag("Bullet"))
        {
            if (!ParryBool)
            {
                Vector3 incomingVec = collision.relativeVelocity.normalized;
                Vector3 normalVec = collision.contacts[0].normal;
                var impactAngle = Vector3.Angle(incomingVec, normalVec);
                var bullet = collision.gameObject.GetComponent<Bullet>();
                bullet.Bounce(aim + impactAngle);
            }
            if (ParryBool)
            {
                Vector3 incomingVec = collision.relativeVelocity.normalized;
                Vector3 normalVec = collision.contacts[0].normal;
                var impactAngle = Vector3.Angle(incomingVec, normalVec);
                var bullet = collision.gameObject.GetComponent<Bullet>();
                bullet.PerfectBounce(aim + impactAngle);
                ParryTimer = 0;
                Shield.GetComponent<MeshRenderer>().material = ShieldMat;
                Shield.GetComponent<BoxCollider>().enabled = true;
            }
        }
     }

    void Parry()
    {
        if (Input.GetButtonDown("Fire1") && ParryTimer <= 0 && !ParryBool)
        {
            ParryTimer = ParryMax;
        }

        if (ParryTimer > 0)
        {
            ParryTimer -= Time.fixedDeltaTime;
            if (ParryTimer >= ParryThreshHold)
            {
                ParryBool = true;
                Shield.GetComponent<MeshRenderer>().material = ParryMat;
            }
            if (ParryTimer < ParryThreshHold)
            {
                Shield.GetComponent<MeshRenderer>().material = VoidMat;
                Shield.GetComponent<BoxCollider>().enabled = false;
                ParryBool = false;
            }
        }

        if (ParryTimer <= 0)
        {
            Shield.GetComponent<MeshRenderer>().material = ShieldMat;
            Shield.GetComponent<BoxCollider>().enabled = true;
            ParryBool = false;
        }
    }
}
