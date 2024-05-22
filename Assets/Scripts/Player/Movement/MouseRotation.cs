using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Movement
{
    public  class MouseRotation :MonoBehaviour
    {
        #region Varialbe Declarations

        public Transform PlayerRoot, LookRoot;
        public Vector2 CurrentMouseDirections;
        public float Sensitivity = 4f;
        public int SmoothSteps = 10;
        public float SmoothWeights = 0.4f;
        public bool Invertmouse = false;

        public VariableJoystick _JoyStick;
       
        public Vector2 Default_Look_Limit = new Vector2(-70f, 80f);
        public Vector3 LookAngels;
     public    RectTransform JoyStickArea;

        #endregion
        private void Start()
        {
            Cursor.lockState= CursorLockMode.Locked;
        }


        private void LockUnlockCursorMode()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(Cursor.lockState== CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;

                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible=false;
                }
            }
        }

        public void RotateTouch()
        {
           
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = touch.position;
                float joystickRadius = 50f;
                bool isTouchingJoyStick = false;
                Vector2 joystickStartPosition=new Vector2(0f,0f);
                if (RectTransformUtility.RectangleContainsScreenPoint(JoyStickArea, touchPosition))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        isTouchingJoyStick = true;
                        joystickStartPosition = touchPosition;

                    }
                    else if (touch.phase==TouchPhase.Moved && isTouchingJoyStick)
                    {
                        Vector2 delta = touchPosition - joystickStartPosition;
                        delta = Vector2.ClampMagnitude(delta, joystickRadius);
                        joystickStartPosition = touchPosition;
                    }else if (touch.phase == TouchPhase.Ended)
                    {
                        isTouchingJoyStick = false;
                    }
                }
                else
                {
                    LookAngels.x += touch.deltaPosition.y * Sensitivity * (Invertmouse ? 1f : -1f);
                    LookAngels.y += touch.deltaPosition.x * Sensitivity;
                    LookAngels.x = Math.Clamp(LookAngels.x, Default_Look_Limit.x, Default_Look_Limit.y);
                    LookRoot.localRotation = Quaternion.Euler(LookAngels.x, 0f, 0f);
                    PlayerRoot.localRotation = Quaternion.Euler(0f, LookAngels.y, 0f);

                }
              
            }
        }
        public void RotateMouse()
        {
            CurrentMouseDirections = new Vector2(Input.GetAxis(TagManager.MOUSE_Y), Input.GetAxis(TagManager.MOUSE_X));
            LookAngels.x += CurrentMouseDirections.x * Sensitivity * (Invertmouse ? 1f : -1f);
            LookAngels.y += CurrentMouseDirections.y * Sensitivity;
            LookAngels.x = Math.Clamp(LookAngels.x,Default_Look_Limit.x,Default_Look_Limit.y);
            LookRoot.localRotation = Quaternion.Euler(LookAngels.x, 0f, 0f);
            PlayerRoot.localRotation = Quaternion.Euler(0f, LookAngels.y, 0f);
        }
        private void Update()
        {
            LockUnlockCursorMode();
            // RotateMouse();
            RotateTouch();
        }



    }
}
