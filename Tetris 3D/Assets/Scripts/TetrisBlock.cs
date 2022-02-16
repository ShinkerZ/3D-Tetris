using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public GhostBehavior ghost;
    float previousTime;
    float fallTime = 1f;
    int quadrant;

    
    //Time takes the block to transport to the next position
    void Start()
    { 
        fallTime = GameManager.gmInstance.ReadFallSpeed();
        
        if (!CheckValidMove())
        {
            GameManager.gmInstance.SetGameIsOver();
        }
        PlayField.playFieldInstance.UpdateGrid(this); // Immedietly Update grid with intel on blocking blocks in path
    }
    void Update()
    {
        quadrant = CameraControl.camInstance.quadrant;
        Inputs();
        if (Time.time - previousTime > fallTime) //Calculate time of fall
        {
            transform.position += Vector3.down;

            if (!CheckValidMove()) // Not a Valid Move
            {
                transform.position += Vector3.up;
                //Delete Layer
                PlayField.playFieldInstance.DeleteLAyer(); // Delete Layer first checks if there are any full rows or layers first
                Destroy(ghost.gameObject); // delete ghost
                enabled = false; // disable script from block


                if (!GameManager.gmInstance.ReadGameOver())// if not a valid move and not Game Over , spawn a new block
                {
                    // Create a new terris block
                    //Debug.Log("About To Spawn");
                    PlayField.playFieldInstance.SpawnNewBlock();
                }
            }
            else // Valid Move
            {
                PlayField.playFieldInstance.UpdateGrid(this);
            }
            previousTime = Time.time; // prev Time is now the current time 
        }   
    }
    public void SetInput(Vector3 direction) // Actual directional control
    {
        transform.position += direction;
        if(!CheckValidMove())
        {
            transform.position -= direction;
        }
        else
        {
            PlayField.playFieldInstance.UpdateGrid(this);
        }
    }
    public void SetRotationInput(Vector3 rotation) // Actual Rotational control
    {
        transform.Rotate(rotation, Space.World);
        if(!CheckValidMove())
        {
            transform.Rotate(-rotation, Space.World);
        }
        else
        {
            PlayField.playFieldInstance.UpdateGrid(this);
        }
    }
    bool CheckValidMove()
    {
        foreach(Transform child in transform) // Loop Through all cubes
        {
            Vector3 pos = PlayField.playFieldInstance.Round(child.position); // Store each cubes position and round it to only Integers
            
            if(!PlayField.playFieldInstance.CheckInsidePlayField(pos)) // Check if block is outside of the play field based on the child cube positions
            {
                //Debug.Log("Is outside of playarea !");
                return false;
            }
        }
        foreach(Transform child in transform)
        {
            Vector3 pos = PlayField.playFieldInstance.Round(child.position);
            Transform t = PlayField.playFieldInstance.GetTransformOnGridPosition(pos);
            if (t != null && t.parent != transform) // check if there is something in the blocks position but not him
            {
                //Debug.Log("Something is blocking !");
                return false;
            }
        }
        return true;
    }
    public void Inputs() // Space is always avilable -- depends on quadrant location  invert Button movement and rotation controller 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fallTime = 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fallTime = 1f;
        }
        if (quadrant == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetInput(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetInput(Vector3.right);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetInput(Vector3.forward);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetInput(Vector3.back);
            }
            if (Input.GetKeyDown(KeyCode.Q)) // Rotate X
            {
                SetRotationInput(new Vector3(90, 0, 0));
            }
            if (Input.GetKeyDown(KeyCode.E)) // Rotae Y
            {
                SetRotationInput(new Vector3(0, 90, 0));
            }
            if (Input.GetKeyDown(KeyCode.R)) //Rotate Z
            {
                SetRotationInput(new Vector3(0, 0, 90));
            }
        }
        if (quadrant == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetInput(Vector3.back);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetInput(Vector3.forward);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetInput(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetInput(Vector3.right);
            }
            if (Input.GetKeyDown(KeyCode.Q)) // Rotate X
            {
                SetRotationInput(new Vector3(0, 0, 90));
            }
            if (Input.GetKeyDown(KeyCode.E)) // Rotae Y
            {
                SetRotationInput(new Vector3(0, 90, 0));
            }
            if (Input.GetKeyDown(KeyCode.R)) //Rotate Z
            {
                SetRotationInput(new Vector3(-90, 0, 0));
            }
        }
        if (quadrant == 3)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetInput(Vector3.right);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetInput(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetInput(Vector3.back);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetInput(Vector3.forward);
            }
            if (Input.GetKeyDown(KeyCode.Q)) // Rotate X
            {
                SetRotationInput(new Vector3(0, 0, -90));
            }
            if (Input.GetKeyDown(KeyCode.E)) // Rotae Y
            {
                SetRotationInput(new Vector3(0, 90, 0));
            }
            if (Input.GetKeyDown(KeyCode.R)) //Rotate Z
            {
                SetRotationInput(new Vector3(-90, 0, 0));
            }
        }
        if (quadrant == 4)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetInput(Vector3.forward);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetInput(Vector3.back);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetInput(Vector3.right);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetInput(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.Q)) // Rotate X
            {
                SetRotationInput(new Vector3(0, 0, -90));
            }
            if (Input.GetKeyDown(KeyCode.E)) // Rotae Y
            {
                SetRotationInput(new Vector3(0, 90, 0));
            }
            if (Input.GetKeyDown(KeyCode.R)) //Rotate Z
            {
                SetRotationInput(new Vector3(90, 0, 0));
            }
        }

    }

}
