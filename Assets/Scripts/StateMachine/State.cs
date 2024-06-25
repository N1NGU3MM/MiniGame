using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    // Name
    public readonly string name;

    // Contruvtor
    protected State(string name)
    {
        this.name = name;
    }
    // Enter
    public virtual void Enter()
    {

    }
    // Exit
    public virtual void Exit()
    {
        
    }
    // Upadete
    public virtual void Update()
    {
        
    }
    // LateUpdate
    public virtual void LateUpdate()
    {
        
    }

    // FixUpadate
    public virtual void FixedUpdate()
    {
        
    }
}
