using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Client : NetworkBehaviour
{
    public GameObject ClientAutoConnect;
    // Start is called before the first frame update
    void Start()
    {
        var ClientConnect = ClientAutoConnect.GetComponent<NetworkManager>();
        ClientConnect.StartClient();
    }

}
