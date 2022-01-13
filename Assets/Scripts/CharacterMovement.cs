using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    enum States
    {
        Idle,
        Run,
        Jump
    }

    [SerializeField]
    private States CurrentState;

    private CharacterController Controller;
    private Animator AnimatorCharacter;
    [SerializeField]
    private float AnimationFinishTime = 0.9f;

    int IdIsRunning;
    int IdIsAttacking1;
    int IdIsAttacking2;

    CustomInputs Inputs;

    Vector2 CurrentMovement;
    bool MovementPressed;
    bool IsAttacking = false;



    //Smooth Movement
    [SerializeField]
    private float InputSmoothDamp = 0.3f;
    [SerializeField]
    private float SmoothInputSpeed = 0.2f;

    private Vector2 InputMovement;
    private Vector2 CurrentInputVector;
    private Vector2 SmoothInputVelocity;
    private void Awake()
    {
        HandleInputs();
    }
    // Start is called before the first frame update
    void Start()
    {
        AnimatorCharacter = GetComponentInChildren<Animator>();
        IdIsRunning = Animator.StringToHash("IsRunning");
        IdIsAttacking1 = Animator.StringToHash("IsAttacking1");
        IdIsAttacking2 = Animator.StringToHash("IsAttacking2");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();

        HandleCombat();
    }
    private void HandleMovement()
    {
        //Controller.Move(new Vector3(CurrentInputVector.x, 0, CurrentInputVector.y));
        bool IsRunning = AnimatorCharacter.GetBool(IdIsRunning);

        if(MovementPressed && !IsRunning)
        {
            AnimatorCharacter.SetBool(IdIsRunning, true);
        }

        if (!MovementPressed && IsRunning)
        {
            AnimatorCharacter.SetBool(IdIsRunning, false);
        }
    }

    private void HandleRotation()
    {
        Vector3 CurrentPosition = transform.position;

        
        Vector3 NewPosition = new Vector3(InputMovement.x, 0, InputMovement.y);

        Vector3 PositionToLookAt = CurrentPosition + NewPosition;

        transform.LookAt(PositionToLookAt);
    }

    void HandleInputs()
    {
        Inputs = new CustomInputs();
        Inputs.Player.Movement.performed += context => {
            InputMovement = context.ReadValue<Vector2>();
            //Smooth Movement doesnt Rotate
            //CurrentInputVector = Vector2.SmoothDamp(CurrentInputVector, InputMovement, ref SmoothInputVelocity, SmoothInputSpeed);
            MovementPressed = true;
        };
        Inputs.Player.Movement.canceled += context => {
            MovementPressed = false;
        };

        Inputs.Player.Attack.performed += context => {
            Attack();
        };
    }
    void Attack()
    {
        if (!IsAttacking)
        {
            AnimatorCharacter.SetTrigger(IdIsAttacking2);
            StartCoroutine(InitializeAttack());
        }
    }
    void HandleCombat()
    {
        if(IsAttacking && AnimatorCharacter.GetCurrentAnimatorStateInfo(1).normalizedTime >= AnimationFinishTime)
        {
            IsAttacking = false;
        }
    }

    IEnumerator InitializeAttack()
    {
        yield return new WaitForSeconds(0.1f);
        IsAttacking = true;
    }
    private void OnEnable()
    {
        Inputs.Player.Enable();
    }

    private void OnDisable()
    {
        Inputs.Player.Disable();
    }
}
