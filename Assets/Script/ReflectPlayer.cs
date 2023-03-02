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
                
                Vector3 normalVec = collision.contacts[0].normal;
                var find = collision.collider.TryGetComponent(out Rigidbody rb);
                var bullet = collision.gameObject.GetComponent<Bullet>();
                if (find)
                {
                    
                    var vel = Vector3.Reflect(bullet.CurrentVel, normalVec); ;
                    bullet.Bounce(vel);
                  
               }
                else print("no Rigid");     
                bullet.GetComponent<MeshRenderer>().material = FriendlyMat;

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
            Destroy(collision.gameObject);
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
        yield return new WaitForSeconds(0.4f);
        blit.SetActive(false);

    }

    IEnumerator ScreenFreeze()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.33f);
        Time.timeScale = 1f;
    }
}
