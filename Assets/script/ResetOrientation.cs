using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class ResetOrientation : MonoBehaviour
{
    public GameObject modelRoot;
    public GameObject modelHip;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            InputTracking.Recenter();
            modelRoot.transform.rotation = Quaternion.Euler(0, -modelHip.transform.localRotation.eulerAngles.y, 0);
        }
    }
}
