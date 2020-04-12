using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //火系統スキル

    private float fireTime = 0;
    private ParticleSystem firePar;
    private EffectStatus effectStatus;
    // Start is called before the first frame update
    void Start()
    {
        firePar = GetComponent<ParticleSystem>();
        effectStatus = GetComponent<EffectStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTime >= 0.1)
        {
            fireTime += Time.deltaTime;
            if (fireTime > effectStatus.time)
            {
                firePar.Stop();
                fireTime = 0;
            }
        }
    }

    public void FireActivate(string str)
    {
        firePar.Play();
        fireTime = 0.1f;
    }
}
