using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
    }
}
