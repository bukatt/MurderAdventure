using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class DoorOpen : NetworkBehaviour
{
    private Rigidbody2D rigidbody2D;
    private HingeJoint2D myHingeJoint;
    [SyncVar]
    private bool open = false;

    public event Action doorOpened;

    public float doorSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        myHingeJoint = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Server]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.player)
        {
            if (collision.gameObject.GetComponent<GamePlayer>().isLocalPlayer)
            {
                collision.gameObject.GetComponent<DoorOpenManager>().doorOpened += ToggleDoor;
            }
        }
    }

    [Server]
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Constants.Tags.player)
        {
            if (collision.gameObject.GetComponent<GamePlayer>().isLocalPlayer)
            {
                collision.gameObject.GetComponent<DoorOpenManager>().doorOpened -= ToggleDoor;
            }
        }
    }

    [Command (ignoreAuthority = true)]
    private void CmdToggleDoor()
    {
        Debug.Log("toggle doror");
        if (open)
        {
            open = false;
           // RpcCloseDoor();
        }
        else
        {
            open = true;
            //RpcOpenDoor();
        }
    }


    public void ToggleDoor()
    {
        CmdToggleDoor();
    }

    [ClientRpc]
    private void RpcOpenDoor()
    {
       // rigidbody2D.SetRotation(70);
        
        open = true;
    }

    [ClientRpc]
    private void RpcCloseDoor()
    {
       // rigidbody2D.SetRotation(0);
        open = false;
    }

    public void FixedUpdate()
    {
        if (open && rigidbody2D.rotation < 60 && !myHingeJoint.useMotor)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            myHingeJoint.useMotor = true;
            JointMotor2D tempMotor = new JointMotor2D();
            tempMotor.motorSpeed = -doorSpeed;
            tempMotor.maxMotorTorque = 100f;
            myHingeJoint.motor = tempMotor;
        } else if(!open && rigidbody2D.rotation > 0 && !myHingeJoint.useMotor)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            myHingeJoint.useMotor = true;
            JointMotor2D tempMotor = new JointMotor2D();
            tempMotor.motorSpeed = doorSpeed;
            tempMotor.maxMotorTorque = 100f;
            myHingeJoint.motor = tempMotor;
        } else if (open && rigidbody2D.rotation >= 60 && myHingeJoint.useMotor)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            myHingeJoint.useMotor = false;
        } else if (!open && rigidbody2D.rotation <= 0 && myHingeJoint.useMotor)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            myHingeJoint.useMotor = false;
        }
    }

    private void OnDestroy()
    {
        
    }


}
