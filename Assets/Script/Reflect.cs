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
            Vector3 incomingVec = collision.relativeVelocity.normalized;
            Vector3 normalVec = collision.contacts[0].normal;
            var impactAngle = Vector3.Angle(incomingVec, normalVec);
            var bullet = collision.gameObject.GetComponent<Bullet>();
            bullet.Bounce(-impactAngle);
        }
     }
}
