using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePreview : MonoBehaviour
{
    public float rotSpeed = 200f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotSpeed * Time.deltaTime);
    }
}
