using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiShooter : MonoBehaviour
{
    public int Health;
    public float Speed;
    public float StoppingDistance;
    public float RetreatDistance;
    private float TimeBtwShot;
    public float StartTimeBtwShot;
    private Transform Player;
    public GameObject BulletPrefab;
    public GameObject FirePoint;
    public GameObject Pivot;
    private Player_Controller player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        TimeBtwShot = StartTimeBtwShot;
       
    }

    // Update is called once per frame
    void Update()
    {

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

        Vector2 aim = new Vector2(transform.position.x - Player.position.x, transform.position.y - Player.position.y);
        float angle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(angle+45, 0, 0);
        Pivot.transform.rotation = rotation;

        if (transform.position.x - Player.position.x > 0.5) transform.rotation = Quaternion.Euler(0, 90, 0);
        else transform.rotation = Quaternion.Euler(0, -90, 0);
        //Shoot
        if (TimeBtwShot <= 0)
        {
            GameObject Go = Instantiate(BulletPrefab, FirePoint.transform.position, rotation * Quaternion.Euler(0, 0, 90) );
            Rigidbody rb_bullet = Go.GetComponent<Rigidbody>();
            rb_bullet.AddForce(aim.normalized * -0.8f, ForceMode.Impulse);
            TimeBtwShot = StartTimeBtwShot;
            //FindObjectOfType<AudioManager>().Play("E_Shoot");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))
        {
            Health -= 1;
            //FindObjectOfType<AudioManager>().Play("E_Hit");
        }

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //FindObjectOfType<AudioManager>().Play("E_Death");
        }
    }

}
