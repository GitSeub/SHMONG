using System.Collections;
using UnityEngine;

public class EnnemiShotgun : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        TimeBtwShot = StartTimeBtwShot;
        shake = Camera.main.GetComponent<CameraShake>();
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
            if (danger >= Danger) StartCoroutine(Fire());
            else FireDanger();
            TimeBtwShot = StartTimeBtwShot;
        }
        else
        {
            TimeBtwShot -= Time.deltaTime;
        }

        if (Health <= 0)
        {
            //FindObjectOfType<AudioManager>().Play("Death");
            Destroy(gameObject);
            shake.shaking = true;
        }
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(25, 0, 90));
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(-25, 0, 90));
        yield return new WaitForSeconds(0.1f);
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(25, 0, 90));
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(-25, 0, 90));
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.1f);
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(-25, 0, 90));
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(25, 0, 90));
        Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));

    }
    void FireDanger()
    {
        Instantiate(BulletDanger, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        Instantiate(BulletDanger, FirePoint.transform.position, rotation * Quaternion.Euler(-25, 0, 90));
        Instantiate(BulletDanger, FirePoint.transform.position, rotation * Quaternion.Euler(25, 0, 90));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            Health -= collision.gameObject.GetComponent<Bullet>().dmg;
            Destroy(collision.gameObject);
            //FindObjectOfType<AudioManager>().Play("E_Hit");
        }

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //FindObjectOfType<AudioManager>().Play("E_Death");
        }
    }

}
