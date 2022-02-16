using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public GhostBehavior ghost;
    GameObject actualTetrisBlock;
    TetrisBlock parentTetris;
    
    void Start()
    {
        // StartCoroutine(RepositionBlock());
    }
    void Update()
    {
        PositionGhost();
        StuckDown();
    }

    public void SetParent(GameObject _parent)
    {
        actualTetrisBlock = _parent;
        parentTetris = actualTetrisBlock.GetComponent<TetrisBlock>();
    }
    void PositionGhost()
    {
        transform.position = actualTetrisBlock.transform.position;
        transform.rotation = actualTetrisBlock.transform.rotation;
    }
    void StuckDown()
    {
        while(CheckValidMove())
        {
            transform.position += Vector3.down;
        }
        if(!CheckValidMove())
        {
            transform.position += Vector3.up;
        }
    }
    bool CheckValidMove()
    {
        foreach (Transform child in transform) // Loop Through all cubes
        {
            Vector3 pos = PlayField.playFieldInstance.Round(child.position); // Store each cubes position and round it to only Integers
            if (!PlayField.playFieldInstance.CheckInsidePlayField(pos)) // Check if block is outside of the play field based on the chils cube positions
            {
                return false;
            }
        }
        foreach (Transform child in transform)
        {
            Vector3 pos = PlayField.playFieldInstance.Round(child.position);
            Transform t = PlayField.playFieldInstance.GetTransformOnGridPosition(pos);
            if(t!=null && t.parent == actualTetrisBlock.transform) // if we found a transform but also found our parent block
            {
                continue;
            }
            if (t != null && t.parent != transform) // if we have found a transform which is not NULL and also not the same as the block - return false
            {
                return false;
            }
        }
        return true;
    }
    public void DestroyGhost()
    {
        Destroy(gameObject);
    }
    /*IEnumerator RepositionBlock()
    {
        while (parentTetris.enabled)//Null 
        {
            PositionGhost();
            //Move Downwards

            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
        yield return null;
    }*/
}
