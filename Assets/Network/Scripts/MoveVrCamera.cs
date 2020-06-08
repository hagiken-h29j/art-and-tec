using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVrCamera : MonoBehaviour
{
    public bool isMoved=false;
    public string CameraPointName;
    [SerializeField]
    private GameObject TargetObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetObj == null)
        {
            try
            {
                TargetObj = GameObject.Find(CameraPointName);
            }
            catch
            {

            }
        }
        else if (isMoved == false)
        {
            this.transform.position = TargetObj.transform.position;
            this.transform.rotation = TargetObj.transform.rotation;
            this.transform.parent = TargetObj.transform;

            isMoved = true;
        }
    }
}
