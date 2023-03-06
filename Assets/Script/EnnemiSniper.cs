using System.Collections;
using UnityEngine;

public class EnnemiSniper : MonoBehaviour
{
    public int Health;
    public float Speed;
    private float TimeBtwShot;
    public float StartTimeBtwShot;
    public int Danger;
    private Transform Player;
    public GameObject Bullet;
    public GameObject BulletDanger;
    public GameObject FirePoint;
    public GameObject Pivot;
    private Vector2 aim;
    private Quaternion rotation;
    public Transform Destination;
    private CameraShake shake;
    private GameManager gm;
    public ParticleSystem Death;
    public ParticleSystem Hit2;
    public Material Hit;
    public Material Base;
    public GameObject Mesh;
    private Score score;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        TimeBtwShot = StartTimeBtwShot;
        shake = Camera.main.GetComponent<CameraShake>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        score = GameObject.Find("SCore").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        transform.position = Vector3.MoveTowards(transform.position, Destination.position, Speed * Time.deltaTime);
        //transform.Translate(Destination.position * Speed * Time.deltaTime);

        //Aim
        aim = new Vector2(transform.position.x - Player.position.x, transform.position.y - Player.position.y);
        float angle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(angle -180, 90, 0);
        Pivot.transform.rotation = rotation;

        if (transform.position.x - Player.position.x > 0.5) transform.rotation = Quaternion.Euler(0, 90, 0);
        else transform.rotation = Quaternion.Euler(0, -90, 0);


        //Shoot
        if (TimeBtwShot <= 0)
        {
            var danger = Random.Range(0, 11);
            if (danger >= Danger) Fire();
            else FireDanger();
            TimeBtwShot = StartTimeBtwShot + Random.Range(0f,0.2f);
        }
        else
        {
            TimeBtwShot -= Time.deltaTime;
        }

        if (Health <= 0)
        {
            gm.E_Count--;
            FindObjectOfType<AudioManager>().Play("Death");
            shake.shaking = true;
            Instantiate(Death, transform.position, Quaternion.identity);
            score.scoreTarget += 100;
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        FindObjectOfType<AudioManager>().Play("Shoot");
    }
    void FireDanger()
    {
        Instantiate(BulletDanger, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        FindObjectOfType<AudioManager>().Play("Shoot");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            Health -= collision.gameObject.GetComponent<Bullet>().dmg;
            Destroy(collision.gameObject);
            Mesh.GetComponent<SkinnedMeshRenderer>().material = Hit;
            StartCoroutine(HitDelay());
            Instantiate(Hit2, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("Hit");
        }

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Health = 0;
        }
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Mesh.GetComponent<SkinnedMeshRenderer>().material = Base;
    }

}
