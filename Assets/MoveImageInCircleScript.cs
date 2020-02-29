using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveImageInCircleScript : MonoBehaviour
{
    public float speed = .5f;

    private void Awake()
    {
        speed = Random.Range(.2f, 1f);
    }
    private void Update()
    {
        transform.Rotate(0, 0, speed);
    }
}
