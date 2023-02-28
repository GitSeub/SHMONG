using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float Power;
    public float Acceleration;
    public float Deceleration;
    public float velPower;
    public float frictionAmount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float MoveX = (Input.GetAxis("Horizontal"));
        float MoveY = (Input.GetAxis("Vertical"));

        float targetSpeedX = MoveX * speed;
        float speedDifX = targetSpeedX - rb.velocity.x;
        float accelRateX = (Mathf.Abs(targetSpeedX) < 0.01f) ? Acceleration : Deceleration;
        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);

        float targetSpeedY = MoveY * speed;
        float speedDifY = targetSpeedY - rb.velocity.y;
        float accelRateY = (Mathf.Abs(targetSpeedY) < 0.01f) ? Acceleration : Deceleration;
        float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);

        rb.AddForce(movementX * Vector2.right);
        rb.AddForce(movementY * Vector2.up);
    }
}
