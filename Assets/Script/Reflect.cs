using UnityEngine;

public class Reflect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision collision)
     {
        if (collision.collider.CompareTag("Bullet"))
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
        }
     }
}
