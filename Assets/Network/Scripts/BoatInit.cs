using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        //this.GetComponent<MeshCollider>().enabled = true;
    }
}
