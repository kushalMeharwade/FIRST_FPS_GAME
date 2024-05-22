using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidRotation : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 Default_Look_Limit = new Vector2(-70f, 80f);
    public Transform PlayerRoot, LookRoot;
    public float Sensitivity = 4f;
    public float Current_x = 0.0f;
    public float Current_y = 0.0f;
    public VariableJoystick _variableJoyStick;
    public void LateUpdate()
    {
        Current_x += _variableJoyStick.Horizontal * Sensitivity * Time.deltaTime;
        Current_y-=_variableJoyStick.Vertical *Sensitivity * Time.deltaTime;

        Current_y = Mathf.Clamp(Current_y, Default_Look_Limit.x, Default_Look_Limit.y);
        Vector3 Direction = new Vector3(0, 0, 10);

        Quaternion rotation =  Quaternion.Euler(Current_y, Current_x, 0);
        transform.position=LookRoot.position + rotation * Direction;

    }
}

