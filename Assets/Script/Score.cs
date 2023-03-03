using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score;
    public float scoreTarget;
    public TextMeshPro tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score < scoreTarget)
        {
            score++;
        }

        tmp.text = ("Score " + score);
    }
}
