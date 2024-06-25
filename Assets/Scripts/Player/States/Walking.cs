using UnityEngine;

public class Walking : State
{
    private PlayerController controller;
    public Walking(PlayerController controller) : base("Walking")
    {
        this.controller = controller;
    }
    
    public override void Enter()
    {
        base.Enter();
        ;
    }
    public override void Exit()
    {
        base.Exit();
        
    }
    public override void Update()
    {
        base.Update();

        // Switch to Idle
        if (controller.movementVector.IsZero())
        {
            controller.stateMachine.ChangeState(controller.idleState);
            return;
        }


    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //  Create Vector
        Vector3 walkingVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        walkingVector = controller.GetForward() * walkingVector;
        walkingVector *= controller.movementSpeed;
        
        
        // Apply input force
        controller.thisRigidbody.AddForce(walkingVector, ForceMode.Force);

        // Rotate character
        controller.RoteteBodyToFaceInput();
    }

    
}