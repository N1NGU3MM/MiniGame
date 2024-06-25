using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    //  public properties
    public float movementSpeed = 10;
    // State Machine
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Walking walkingState;

    // internal Properties
    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public Rigidbody thisRigidbody;

    [HideInInspector] public Animator thisAnimator;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update

    void Start()
    {
        // stateMachine and it's states
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        stateMachine.ChangeState(idleState);

    }

    // Update is called once per frame
    void Update()
    {
        // Creat input vector
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        movementVector = new Vector2(inputX, inputY);

        // Pass the velocity for Animator
        float velocity = thisRigidbody.velocity.magnitude;
        float velocityRate = velocity / movementSpeed;
        thisAnimator.SetFloat("fVelocity", velocityRate);

        stateMachine.Update();

    }
    void LateUpdate()
    {
        // StateMachine
        stateMachine.LateUpdate();

    }

    void FixedUpdate()
    {
        
        stateMachine.FixedUpdate();
    }

    public Quaternion GetForward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RoteteBodyToFaceInput()
    {
        // Calculate rotation
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        Quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion toRotation =  q1 * q2;
        Quaternion newRotation =  Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);

        // apply rotation

        thisRigidbody.MoveRotation(newRotation);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(5 , 5, 200, 50), stateMachine.currentStateName);
    }
}