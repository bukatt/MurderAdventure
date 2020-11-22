using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class DoorOpenManager : NetworkBehaviour
{
    public event Action doorOpened;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDoorToggle()
    {

    }

    public void ToggleDoor()
    {
        CmdToggleDoor();
    }

    [Command]
    public void CmdToggleDoor()
    {
        doorOpened?.Invoke();
    }
}
