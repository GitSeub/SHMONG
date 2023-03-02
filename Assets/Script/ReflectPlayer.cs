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
    private Vector3 _velocity;
    public Material shader;
    private float time;
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

        if (time < 0.5f) time += Time.deltaTime;
        shader.SetFloat("_TimeValue", time);
    }

     void OnCollisionEnter(Collision collision)
     {
        if (collision.collider.CompareTag("Bullet"))
        {
            if (collision.gameObject.GetComponent<Bullet>().Danger)
            {
                if (ParryBool)
                {
                    Destroy(collision.gameObject);
                    ParryTimer = 0;
                    Shield.GetComponent<MeshRenderer>().material = ShieldMat;
                    Shield.GetComponent<BoxCollider>().enabled = true;
                    Shake.shaking = true;
                    StartCoroutine(Distortion());
                    time = 0;
                }
                else
                {
                    Activated = false;
                    ShieldDownTimer = ShieldDownMax;
                    Shield.GetComponent<MeshRenderer>().material = VoidMat;
                    Shield.GetComponent<BoxCollider>().enabled = false;
                    ParryBool = false;
                    Destroy(collision.gameObject);
                }

            }
            if (!ParryBool)
            {
                
                Vector3 normalVec = collision.contacts[0].normal;
                var find = collision.collider.TryGetComponent(out Rigidbody rb);
                var bullet = collision.gameObject.GetComponent<Bullet>();
                if (find)
                {
                    
                    var vel = Vector3.Reflect(bullet.CurrentVel, normalVec); ;
                    bullet.Bounce(vel);
                  
               }
                else print("no Rigid");     
            }
            if (ParryBool)
            {
                Vector3 normalVec = collision.contacts[0].normal;
                var find = collision.collider.TryGetComponent(out Rigidbody rb);
                var bullet = collision.gameObject.GetComponent<Bullet>();
                if (find)
                {

                    var vel = Vector3.Reflect(bullet.CurrentVel, normalVec); ;
                    bullet.PerfectBounce(vel);
                    time = 0;
                }
                else print("no Rigid");
                bullet.GetComponent<MeshRenderer>().material = ParryMat;
                ParryTimer = 0;
                Shield.GetComponent<MeshRenderer>().material = ShieldMat;
                Shield.GetComponent<BoxCollider>().enabled = true;
                Shake.shaking = true;
                StartCoroutine(Distortion());
            }
           
        }
        if (collision.collider.CompareTag("Ennemi"))
        {
            if (collision.gameObject.TryGetComponent(out EnnemiShooter E1)) E1.Health = 0;
            if (collision.gameObject.TryGetComponent(out EnnemiShotgun E2)) E2.Health = 0;
            if (collision.gameObject.TryGetComponent(out EnnemiSniper E3)) E3.Health = 0;
            Activated = false;
            ShieldDownTimer = ShieldDownMax;
            Shield.GetComponent<MeshRenderer>().material = VoidMat;
            Shield.GetComponent<BoxCollider>().enabled = false;
            ParryBool = false;
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
        var pos = Camera.main.WorldToScreenPoint(Shield.transform.position);
        var X = pos.x / Screen.width;
        var Y = pos.y / Screen.height;
        shader.SetVector("_FocalPoint", new Vector2(X,Y));
        blit.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        blit.SetActive(false);

    }
}
