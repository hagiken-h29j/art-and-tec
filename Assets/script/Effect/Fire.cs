using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //火系統エフェクト
    //火の種類に応じて範囲増加
    //依存→EffectStatus,EffectBehaviour
    //Resources→なし
    //Tag→なし

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FireActivate()
    {
        float scale = float.Parse(GetComponent<EffectStatus>().stringData);
        this.transform.localScale = new Vector3(scale, 1 + (scale - 1) / 2, scale);
        EffectBehaviour.ParticleDamegeStartAutoMode(this.gameObject);
    }
}
