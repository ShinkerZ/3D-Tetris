using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour
{
    public int gridSizeX, gridSizeY, gridSizeZ;
    int randomIndex;

    [Header("Blocks")]
    public GameObject[] BlockList;
    public GameObject[] GhostList;

    public Transform[,,] theGrid;
    public static PlayField playFieldInstance;
    void Awake()
    {
        playFieldInstance = this;
    }
    private void Start()
    {
        theGrid = new Transform[gridSizeX, gridSizeY, gridSizeZ];
        GetPreview();
        SpawnNewBlock();
    }
    public Vector3 Round(Vector3 posToRound) // Every time Round is called is returns a new Rounded position
    {
        return new Vector3(Mathf.RoundToInt(posToRound.x),
                            Mathf.RoundToInt(posToRound.y),
                            Mathf.RoundToInt(posToRound.z));
    }
    public bool CheckInsidePlayField(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridSizeX &&
                (int)pos.z >= 0 && (int)pos.z < gridSizeZ &&
                (int)pos.y >= 0);
    }
    public void UpdateGrid(TetrisBlock block) // erase/ignore the parent object transform
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if(theGrid[x,y,z] != null)
                    {
                        if (theGrid[x, y, z].parent == block.transform)
                        {
                            theGrid[x, y, z] = null;
                        }
                    }
                }
            }
        }
        foreach(Transform child in block.transform) // Fill in all child objects into the grid
        {
            Vector3 pos = Round(child.position);
            if(pos.y < gridSizeY)
            {
                theGrid[(int)pos.x, (int)pos.y, (int)pos.z] = child; // again round to int each cube position
            }
        }
    }
    public Transform GetTransformOnGridPosition(Vector3 pos)
    {
        if (pos.y > gridSizeY - 1)//if we are above the grid
            return null;
        else
        {
            /*Debug.Log("X : " + pos.x + "  y: " + pos.y + "  Z : " + pos.z);*/
            return theGrid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }
    public void SpawnNewBlock()
    {
        Vector3 spawnPointPos = new Vector3((int)(transform.position.x + (float)gridSizeX / 2),
                                             (int)transform.position.y + gridSizeY -2,
                                             (int)(transform.position.x + (float)gridSizeX / 2));
        
        GameObject newBlock = Instantiate(BlockList[randomIndex], spawnPointPos, Quaternion.identity);
        //GHOST
        GameObject newGhost = Instantiate(GhostList[randomIndex], spawnPointPos, Quaternion.identity);
        newGhost.GetComponent<GhostBehavior>().SetParent(newBlock);
        newBlock.GetComponent<TetrisBlock>().ghost  =  newGhost.GetComponent<GhostBehavior>();

        GetPreview();
        Previewer.prevInstance.ShowPreview(randomIndex);
        //playFieldInstance.UpdateGrid(newBlock.GetComponent<TetrisBlock>()); //every time we spawn a new block we have to update grid
    }
    public void GetPreview()
    {
        randomIndex = Random.Range(0, BlockList.Length);
    }
    public void DeleteLAyer()
    {
        int layersCleared = 0;
        for (int y = gridSizeY -1; y >= 0; y--) //start at top, go down -- this is how we avoid a new full layer that replaces our just now deleted layer, not being destroyed
        {
            if(CheckFullLayer(y))
            {
                layersCleared++;
                DeleteFullLayer(y);
                MoveAllLayesDown(y);
            }
        }
        if(layersCleared>0)
        {
            GameManager.gmInstance.LayersCleared(layersCleared);
        }
    }
    bool CheckFullLayer(int y)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if(theGrid[x,y,z] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }
    void DeleteFullLayer(int y)//Delete all blocks  in the y layer
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Destroy(theGrid[x, y, z].gameObject);
                theGrid[x, y, z] = null;
            }
        }
    }  
    void MoveAllLayesDown(int y)//move all down by 1 , depends on y 
    {
        for (int i = y; i < gridSizeY; i++) 
        {
            MoveOneLayerDown(i);
        }
    }
    void MoveOneLayerDown(int y)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if(theGrid[x,y,z] != null)
                {
                    theGrid[x, y - 1, z] = theGrid[x, y, z];
                    theGrid[x, y , z] = null;
                    theGrid[x, y - 1, z].position += Vector3.down;
                }
            }
        }
    }
}
