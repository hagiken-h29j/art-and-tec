using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject dotSight;
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        // targetオブジェクトを注視するhttps://www.sejuku.net/blog/55071
        this.transform.LookAt(dotSight.transform);
    }
}
