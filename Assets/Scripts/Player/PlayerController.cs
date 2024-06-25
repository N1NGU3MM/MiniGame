using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
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

    // StateMachine
        stateMachine.Update();

    }
}