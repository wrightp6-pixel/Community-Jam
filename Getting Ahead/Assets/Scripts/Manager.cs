using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool laserOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserOn = false;
    }

    public void LaserTrue()
    {
        laserOn = true;
        StartCoroutine(WaitLaser());
    }
    public void LaserFalse()
    {
        laserOn = false;
    }

    IEnumerator WaitLaser()
    {
        yield return new WaitForSeconds(1);
        laserOn = false;
    }
}
