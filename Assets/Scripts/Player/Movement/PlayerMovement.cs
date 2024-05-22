using Assets.Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update



    #region Variables and Components

    private CharacterController PlayerCharacterController;
    public float Speed = 5f;
    public float Gravity = 20f;
    public float Jump_Force=10f;
    private float Vertical_Velocity;
    Vector3 Player_MoveDirection;
    private PlayerCroutch _PlayerCroutch;
    PlayerInput Character_Input;
  
    #endregion



    #region Awake

    private void Awake()
    {
        PlayerCharacterController = GetComponent<CharacterController>();
        _PlayerCroutch=GetComponent<PlayerCroutch>();
        UnityEngine.Input.simulateMouseWithTouches = false;
        Character_Input = GetComponent<PlayerInput>();
    }

    #endregion
    void Start()
    {
        
    }


    #region Player Movement Functions

    public void Move_Player()
    {

        Vector2 Player_Movement = Character_Input.actions["Move"].ReadValue<Vector2>();
        Player_MoveDirection = new Vector3(Player_Movement.x, 0f, Player_Movement.y);
        Player_MoveDirection = transform.TransformDirection(Player_MoveDirection);


        #region Player Movement Old Code
       // Player_MoveDirection = new Vector3(UnityEngine.Input.GetAxis(TagManager.MOVEMENT_X), 0f, UnityEngine.Input.GetAxis(TagManager.MOVEMENT_Y));
      
      //  Player_MoveDirection =transform.TransformDirection(Player_MoveDirection);

        #endregion
        Player_MoveDirection *= Speed * Time.deltaTime;
        ApplyGravity();
        PlayerCharacterController.Move(Player_MoveDirection);
       
    }

    public void Move_Player_Android()
    {

       // Player_MoveDirection = new Vector3(Character_Movemnt_JoyStick.Horizontal, 0f, Character_Movemnt_JoyStick.Vertical);

        Player_MoveDirection = transform.TransformDirection(Player_MoveDirection);


        Player_MoveDirection *= Speed * Time.deltaTime;
        ApplyGravity();
        PlayerCharacterController.Move(Player_MoveDirection);

    }

    public void PlayerJump()
    {
        if(PlayerCharacterController.isGrounded && UnityEngine.Input.GetKeyDown(KeyCode.Space) && !_PlayerCroutch.isCrouching)
        {
            Vertical_Velocity = Jump_Force;
        }
    }
    public void ApplyGravity()
    {
        if (PlayerCharacterController.isGrounded)
        {
            Vertical_Velocity -= Gravity * Time.deltaTime;
            PlayerJump();
            
        }
        else
        {
            Vertical_Velocity -= Gravity * Time.deltaTime;

        }
        Player_MoveDirection.y = Vertical_Velocity * Time.deltaTime;

    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        Move_Player();
       // Move_Player_Android();
    }
}
