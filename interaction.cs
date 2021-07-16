using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    public bool active;
    public string type = "E";

    private bool isInTimer;
    private bool isInColide;

    void FixedUpdate()
    {
        if (!isInTimer && (isInColide || type == "E"))
        {
            timedUpdate();
        }
    }
    IEnumerator wait(int time)
    {
        isInTimer = true;
        yield return new WaitForSeconds(time);
        isInTimer = false;
    } 
    void timedUpdate()
    {
        if (type == "E")
        {
            StartCoroutine(wait(2));
        }else if (type == "C")
        {
            StartCoroutine(wait(5));
        }
        active = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "plr" && type == "C")
        {
            isInColide = true;
            Debug.Log("fgdgdf");
        }
    }
}
