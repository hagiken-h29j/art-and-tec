using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sight : MonoBehaviour {

    public MonoBehaviour ya;
    private Rigidbody yaRb;

    // Use this for initialization
    void Start () {
        yaRb = ya.transform.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (yaRb.velocity == Vector3.zero && Vector3.Distance(Vector3.zero, ya.transform.position) < 2)
        {
            transform.position = ya.transform.position + ya.transform.rotation * (new Vector3(0, 1, 0)) * -4.8f;
        }
    }
}
