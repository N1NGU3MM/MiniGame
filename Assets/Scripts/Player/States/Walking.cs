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

        Vector3 walkingVector = new Vector3(controller.movementVector.x, 0, controller.movementVector.y);
        walkingVector *= controller.movementSpeed * Time.deltaTime;

        // Apply input force
        controller.transform.Translate(walkingVector);
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    
}