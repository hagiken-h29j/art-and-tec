using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour {

    [SerializeField]
    private int num = 0;
    [SerializeField]
    private string type = "range";
    private Option option;
    private Toggle toggle;

    // Use this for initialization
    void Start ()
    {
        option = GetComponentInParent<Option>();
        toggle = GetComponent<Toggle>();
    }
	
	// Update is called once per frame
	public void ToggleClick ()
    {
        if (toggle.isOn)
        {
            Debug.Log("type = " + type);
            Debug.Log("num = " + num);
            option.SetRangeMode(num, type);
        }
    }
}
