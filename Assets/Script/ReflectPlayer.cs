using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    public Material FriendlyMat;
    private bool ParryBool;
    private bool Activated = true;
    public float ShieldDownMax;
    private float ShieldDownTimer;
    public ScriptableRendererFeature blit;
    public CameraShake Shake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aim = Shield.transform.eulerAngles.z;
        if (Activated) Parry();
        else ShieldDown();
        if (Input.GetButtonDown("Fire3")) Distortion();
    }

     void OnCollisionEnter(Collision collision)
     {
        if (collision.collider.CompareTag("Bullet"))
        {
            if (collision.gameObject.GetComponent<Bullet>().Danger)
            {
                Activated = false;
                ShieldDownTimer = ShieldDownMax;
                Shield.GetComponent<MeshRenderer>().material = VoidMat;
                Shield.GetComponent<BoxCollider>().enabled = false;
                ParryBool = false;
                Destroy(collision.gameObject);
            }
            if (!ParryBool)
            {
                Vector3 incomingVec = collision.relativeVelocity;
                Vector3 normalVec = collision.contacts[0].normal;
                var impactAngle = Vector3.Angle(incomingVec, normalVec);
                var bullet = collision.gameObject.GetComponent<Bullet>();
                bullet.GetComponent<MeshRenderer>().material = FriendlyMat;
                
                bullet.Bounce(aim + impactAngle);
            }
            if (ParryBool)
            {
                Vector3 incomingVec = collision.relativeVelocity;
                Vector3 normalVec = collision.contacts[0].normal;
                var impactAngle = Vector3.Angle(incomingVec, normalVec);
                var bullet = collision.gameObject.GetComponent<Bullet>();
                bullet.GetComponent<MeshRenderer>().material = FriendlyMat;
                bullet.PerfectBounce(aim + impactAngle);
                ParryTimer = 0;
                Shield.GetComponent<MeshRenderer>().material = ShieldMat;
                Shield.GetComponent<BoxCollider>().enabled = true;
                Distortion();
                Shake.shaking = true;
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
            ParryTimer -= Time.deltaTime;
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

    void ShieldDown()
    {
        ShieldDownTimer -= Time.deltaTime;
        if (ShieldDownTimer <= 0)
        {
            Activated = true;
            ParryTimer = 0;
        }
    }
    IEnumerator Distortion()
    {
        blit.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        blit.SetActive(false);
    }

}
