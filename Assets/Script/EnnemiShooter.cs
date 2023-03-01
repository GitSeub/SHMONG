using System.Collections;
using UnityEngine;

public class EnnemiShooter : MonoBehaviour
{
    public int Health;
    public float Speed;
    public float StoppingDistance;
    public float RetreatDistance;
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

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        TimeBtwShot = StartTimeBtwShot;
       
    }

    // Update is called once per frame
    void Update()
    {
        //Distance
        if (Vector2.Distance(transform.position, Player.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, Player.position) < StoppingDistance && Vector2.Distance(transform.position, Player.position) > RetreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, Player.position) < RetreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime * -1);
        }

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
            else StartCoroutine(FireDanger());
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
            //FindObjectOfType<AudioManager>().Play("E_Death");
        }
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject Go = Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.2f);
        GameObject Go2 = Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.2f);
        GameObject Go3 = Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.2f);
        GameObject Go4 = Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.2f);
        GameObject Go5 = Instantiate(Bullet, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
    }
    IEnumerator FireDanger()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject Go = Instantiate(BulletDanger, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.3f);
        GameObject Go2 = Instantiate(BulletDanger, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(0.3f);
        GameObject Go3 = Instantiate(BulletDanger, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90));
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
