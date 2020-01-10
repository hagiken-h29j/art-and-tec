using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private float fireTime = 0;
    private ParticleSystem firePar;
    void Start()
    {
        firePar = GameObject.Find("FireEff").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(fireTime >= 0.1)
        {
            fireTime += Time.deltaTime;
            if(fireTime > 1)
            {
                firePar.Stop();
                fireTime = 0;
            }
        }
    }
    public int EffectPlay(string word)
    {
        //各文字に応じた処理
        //if(word.Equals("火")){
        firePar.Play();
        fireTime = 0.1f;
        //}
        return 0;
    }
}
