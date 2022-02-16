using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previewer : MonoBehaviour
{
    public static Previewer prevInstance;
    public GameObject[] previewBlocks;
    GameObject currentActive;

    private void Awake()
    {
        prevInstance = this;
    }
    public void ShowPreview(int index)
    {
        Destroy(currentActive);
        currentActive = Instantiate(previewBlocks[index], transform.position, Quaternion.identity) as GameObject;
    }
}
