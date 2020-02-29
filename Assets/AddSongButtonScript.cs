using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AddSongButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float buttonTimer = 0f;
    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();


    void Update()
    {
        if (!ispressed)
        {
            stopWatch.Reset();
            return;
        }

        stopWatch.Start();


        //timer starten ++ if timer is 10 => doen
        if (stopWatch.Elapsed.TotalSeconds >= 10)
        {

            SceneManager.LoadScene("AddContent");
        }
        else
        {
            Debug.Log("Not held long enough!" + stopWatch.Elapsed.TotalSeconds);
        }

    }
    bool ispressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        ispressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ispressed = false;
    }
}
