using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool laserOn;
    public int health;
    public bool iFrames;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserOn = false;
        health = 7;
        iFrames = false;
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

    public void ChangeHealth()
    {
        if (iFrames == false)
        {
            health -= 1;
            iFrames = true;
            StartCoroutine(WaitIFRames());
        }
        
    }

    IEnumerator WaitIFRames()
    {
        yield return new WaitForSeconds(0.5f);
        iFrames = false;
    }
}
