using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform ytarget;
    Transform rotTarget;
    Vector3 lastPos;
    public static CameraControl camInstance;
    float sensitivitiy = 0.25f;

    
    public int quadrant;

    void Awake()
    {
        camInstance = this;
        rotTarget = transform.parent; // this would handle UP & DOWN
        ytarget = rotTarget.transform.parent; // this would handle LEFT & RIGHT

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ytarget);
        if(Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        Orbit();
        DetectQuadrant();
    }
    void Orbit() // Rotate around the field with the mouse
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPos;
            float angleY = -delta.y * sensitivitiy; 
            float angleX = delta.x * sensitivitiy;

            //Rotation Up & Down
            Vector3 angles = rotTarget.transform.eulerAngles;
            angles.x += angleY;
            angles.x = ClampAngle(angles.x, -45f, 75f);

            rotTarget.transform.eulerAngles = angles;
            //Rotation Left& Right
            ytarget.RotateAround(ytarget.position, Vector3.up, angleX);
            lastPos = Input.mousePosition;
        }
    }
    float ClampAngle(float angle, float min, float max) // Custom Clamping for our UP & DOWN movement aroud the field
    {
        if (angle < 0) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + min);
        return Mathf.Min(angle, max);
    }
    public void DetectQuadrant() //Relative movement depends on camera rotation
    {
        
        float y = ytarget.rotation.eulerAngles.y;
        
        if (y > 315 || y < 45) 
        {
            quadrant = 1;
        }
        else if(y>=45 && y<135) 
        {
            quadrant = 4;
        }
        else if (y >= 135 && y <225) 
        {
            quadrant = 3;
        }
        else if (y >=225 && y<=315) 
        {
            quadrant = 2;
        }
    }
}
