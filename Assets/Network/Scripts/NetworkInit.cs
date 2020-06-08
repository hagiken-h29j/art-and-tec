using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInit : MonoBehaviour
{
    public bool isServer = true;

    public List<GameObject> ServerObjects;
    public List<GameObject> ClientObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            this.GetComponent<Server>().enabled = true;
            this.GetComponent<Client>().enabled = false;

            foreach(var x in ServerObjects)
            {
                x.SetActive(true);
            }
            foreach(var x in ClientObjects)
            {
                x.SetActive(false);
            }

        }
        else
        {
            foreach (var x in ServerObjects)
            {
                x.SetActive(false);
            }
            foreach (var x in ClientObjects)
            {
                x.SetActive(true);
            }
            this.GetComponent<Server>().enabled = false;
            this.GetComponent<Client>().enabled = true;
        }
    }
}
