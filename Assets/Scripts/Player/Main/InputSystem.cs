using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [HideInInspector]
    public bool W;
    [HideInInspector]
    public bool S;
    [HideInInspector]
    public bool A;
    [HideInInspector]
    public bool D;
    [HideInInspector]
    public bool Jump;
    [HideInInspector]
    public bool Sprint;
    [HideInInspector]
    public bool Crouch;
    [HideInInspector]
    public bool Strafe;
    [HideInInspector]
    public bool Attack;
    [HideInInspector]
    public bool Draw;
    [HideInInspector]
    public bool Buff;
    [HideInInspector]
    public bool Fire;
    [HideInInspector]
    public bool Shield;
    [HideInInspector]
    public Vector3 Velocity;
    public AnimationClip BuffAnim;
    public AnimationClip[] AttackAnims;
    public AnimationClip[] StrafeAnims;
    public AnimationClip GroundSlashAnim;
    public AnimationClip SlashAnim;
    public AnimationClip ShieldAnim;

    private void Update() 
    {
        MoveAction();

        JumpAction();

        SprintAction();

        CrouchAction();

        StrafeAction();

        AttackAction();

        DrawAction();

        AbilityAction();
    }

    void MoveAction()
    {
        Velocity = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            W = true;
            Velocity.z = 1;
        } 
        else
        {
            W = false;
        }

        if(Input.GetKey(KeyCode.S))
        {
            S = true;
            Velocity.z = -1;
        }
        else 
        {
            S = false;
        }

        if(Input.GetKey(KeyCode.A))
        {
            A = true;
            Velocity.x = -1;
        }
        else
        {
            A = false;
        }

        if(Input.GetKey(KeyCode.D))
        {
            D = true;
            Velocity.x = 1;
        }  
        else
        {
            D = false;
        }
    }

    void JumpAction()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump = true;
        }  
        else
        {
            Jump = false;
        }
    }

    void SprintAction()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Sprint = true;
        }  
        else
        {
            Sprint = false;
        }
    }

    void CrouchAction()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Crouch = true;    
        }  
        else
        {
            Crouch = false;
        }
    }

    void StrafeAction()
    {
        if(Input.GetMouseButton(1))
        {
            Strafe = true;
        }     
        else
        {
            Strafe = false;
        }
    }

    void AttackAction()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack = true; 
        }  
        else
        {
            Attack = false;
        }
    }

    void DrawAction()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Draw = true;
        }
        else
        {
            Draw = false;
        }
    }

    void AbilityAction()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Buff = true;
        }  
        else
        {
            Buff = false;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Fire = true;
        } 
        else
        {
            Fire = false;
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Shield = true;
        }  
        else
        {
            Shield = false;
        }
    }
}
