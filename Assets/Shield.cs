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
        Inputs = new Vector3(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"), 0);
        print(Inputs);

        if (Inputs.magnitude > 0.2f)
        {
            var relative = (transform.position + Inputs) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.forward);

            transform.rotation = rot;
        }

    }
}
