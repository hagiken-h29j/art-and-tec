using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectStatus : MonoBehaviour
{
    public int status = 0;
    public float damage = 0;
    public float time = 0;
    public string stringData = "";
    [SerializeField] UnityEvent ActivateEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectActivate(float damage, float time, string str)
    {
        status = 1;
        this.damage = damage;
        this.time = time;
        stringData = str;
        ActivateEvent.Invoke();
    }
}
