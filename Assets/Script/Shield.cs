using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector3 Inputs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Inputs = new Vector3(-Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"), 0);

        if (Inputs.magnitude > 0.2f)
        {
            var relative = (transform.position + Inputs) - transform.position;
            var rot = Mathf.Atan2(relative.x, -relative.y);
            
            transform.localEulerAngles = new Vector3(0,0, rot*Mathf.Rad2Deg);
        }

    }
}
