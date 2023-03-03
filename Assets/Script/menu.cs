using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void start()
    {
        StartCoroutine(StartGame());
        anim.SetTrigger("Start");
        FindObjectOfType<AudioManager>().Play("click");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
