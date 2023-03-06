using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] Spawners;
    public Transform[] Destinations;
    public int E_Count;
    public int WaveCount = 1;
    public GameObject E_Shotgun;
    public GameObject E_Sniper;
    public GameObject E_Rifle;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        Wave1();
    }

    // Update is called once per frame
    void Update()
    {
        if (E_Count <= 0 && once)
        {
            WaveCount++;
            StartCoroutine(NextWave());
            once = false;
        }
    }

    void Wave1()
    {
        GameObject E1 = Instantiate(E_Rifle, Spawners[1].position, Quaternion.identity);
        E1.GetComponent<EnnemiShooter>().Destination = Destinations[6];
        E_Count++;
    }

    void Wave2()
    {
        E_Count += 2;
        once = true;
        GameObject E1 = Instantiate(E_Rifle, Spawners[0].position, Quaternion.identity);
        E1.GetComponent<EnnemiShooter>().Destination = Destinations[0];
        GameObject E2 = Instantiate(E_Rifle, Spawners[2].position, Quaternion.identity);
        E2.GetComponent<EnnemiShooter>().Destination = Destinations[3];

    }
    IEnumerator Wave3()
    {
        E_Count += 2;
        GameObject E1 = Instantiate(E_Sniper, Spawners[1].position, Quaternion.identity);
        E1.GetComponent<EnnemiSniper>().Destination = Destinations[4];
        yield return new WaitForSeconds(0.5f);
        GameObject E2 = Instantiate(E_Sniper, Spawners[1].position, Quaternion.identity);
        E2.GetComponent<EnnemiSniper>().Destination = Destinations[5];
        once = true;
    }

    IEnumerator Wave4()
    {
        E_Count += 3;
        GameObject E1 = Instantiate(E_Shotgun, Spawners[1].position, Quaternion.identity);
        E1.GetComponent<EnnemiShotgun>().Destination = Destinations[6];
        yield return new WaitForSeconds(0.75f);
        GameObject E2 = Instantiate(E_Sniper, Spawners[0].position, Quaternion.identity);
        E2.GetComponent<EnnemiSniper>().Destination = Destinations[4];
        GameObject E3 = Instantiate(E_Sniper, Spawners[2].position, Quaternion.identity);
        E3.GetComponent<EnnemiSniper>().Destination = Destinations[5];
        once = true;
    }
    IEnumerator Wave5()
    {
        E_Count += 3;
        GameObject E1 = Instantiate(E_Shotgun, Spawners[1].position, Quaternion.identity);
        E1.GetComponent<EnnemiShotgun>().Destination = Destinations[6];
        yield return new WaitForSeconds(0.5f);
        GameObject E2 = Instantiate(E_Rifle, Spawners[3].position, Quaternion.identity);
        E2.GetComponent<EnnemiShooter>().Destination = Destinations[0];
        yield return new WaitForSeconds(0.5f);
        GameObject E3 = Instantiate(E_Rifle, Spawners[4].position, Quaternion.identity);
        E3.GetComponent<EnnemiShooter>().Destination = Destinations[3];
        once = true;
    }

    void Wave6()
    {
        var E_numb = Random.Range(2, 5);
        for (int i = 0; i < E_numb; i++)
        {
            var E_Type = Random.Range(0, 3);
            if (i == 0)
            {
                SpawnrandomE1(E_Type);
            }
            if (i == 1)
            {
                SpawnrandomE2(E_Type);
            }
            if (i == 2)
            {
                SpawnrandomE3(E_Type);
            }
            if (i == 3)
            {
                SpawnrandomE4(E_Type);
            }
        }
        E_Count+= E_numb;
    }

    void SpawnrandomE1(int E)
    {
        if (E == 0)
        {
            GameObject E1 = Instantiate(E_Rifle, Spawners[3].position, Quaternion.identity);
            E1.GetComponent<EnnemiShooter>().Destination = Destinations[5];
        }
        if (E == 1)
        {
            GameObject E1 = Instantiate(E_Sniper, Spawners[3].position, Quaternion.identity);
            E1.GetComponent<EnnemiSniper>().Destination = Destinations[5];
        }
        if (E == 2)
        {
            GameObject E1 = Instantiate(E_Shotgun, Spawners[3].position, Quaternion.identity);
            E1.GetComponent<EnnemiShotgun>().Destination = Destinations[5];
        }
    }
    void SpawnrandomE2(int E)
    {
        if (E == 0)
        {
            GameObject E1 = Instantiate(E_Rifle, Spawners[4].position, Quaternion.identity);
            E1.GetComponent<EnnemiShooter>().Destination = Destinations[4];
        }
        if (E == 1)
        {
            GameObject E1 = Instantiate(E_Sniper, Spawners[4].position, Quaternion.identity);
            E1.GetComponent<EnnemiSniper>().Destination = Destinations[4];
        }
        if (E == 2)
        {
            GameObject E1 = Instantiate(E_Shotgun, Spawners[4].position, Quaternion.identity);
            E1.GetComponent<EnnemiShotgun>().Destination = Destinations[4];
        }
    }

    void SpawnrandomE3(int E)
    {
        if (E == 0)
        {
            GameObject E1 = Instantiate(E_Rifle, Spawners[0].position, Quaternion.identity);
            E1.GetComponent<EnnemiShooter>().Destination = Destinations[1];
        }
        if (E == 1)
        {
            GameObject E1 = Instantiate(E_Sniper, Spawners[0].position, Quaternion.identity);
            E1.GetComponent<EnnemiSniper>().Destination = Destinations[1];
        }
        if (E == 2)
        {
            GameObject E1 = Instantiate(E_Shotgun, Spawners[0].position, Quaternion.identity);
            E1.GetComponent<EnnemiShotgun>().Destination = Destinations[1];
        }
    }
    void SpawnrandomE4(int E)
    {
        if (E == 0)
        {
            GameObject E1 = Instantiate(E_Rifle, Spawners[2].position, Quaternion.identity);
            E1.GetComponent<EnnemiShooter>().Destination = Destinations[3];
        }
        if (E == 1)
        {
            GameObject E1 = Instantiate(E_Sniper, Spawners[2].position, Quaternion.identity);
            E1.GetComponent<EnnemiSniper>().Destination = Destinations[3];
        }
        if (E == 2)
        {
            GameObject E1 = Instantiate(E_Shotgun, Spawners[2].position, Quaternion.identity);
            E1.GetComponent<EnnemiShotgun>().Destination = Destinations[3];
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(.75f);
        if (WaveCount == 2) Wave2();
        if (WaveCount == 3) StartCoroutine(Wave3());
        if (WaveCount == 4) StartCoroutine(Wave4());
        if (WaveCount == 5) StartCoroutine(Wave5());
        if (WaveCount >= 6) Wave6();
        once = true;
    }
}
