using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBehaviour : MonoBehaviour
{
    //エフェクトの基本的振る舞い
    //0.5秒ごとのダメージ判定開始DamageStart(GameObject gameObject),終了DamageStop(GameObject gameObject)
    //依存→EffectStatus
    //Resources→なし
    //Tag→Enemy

    private float effectTime = -1;//エフェクト発動後経過時間
    private float damagetTime = 0;//ダメージ判定時刻
    private EffectStatus effectStatus;
    private ParticleSystem ParticleSystem;
    private Collider damageCollider;

    private bool PDAutoStop;
    public static void DamageStart(GameObject gameObject)
    {
        if (gameObject.GetComponent<EffectBehaviour>() == null)
        {
            gameObject.AddComponent<EffectBehaviour>().Start();
        }
        gameObject.GetComponent<EffectBehaviour>().DamageRestart();
    }
    public static void ParticleDamegeStartAutoMode(GameObject gameObject)
    {
        if (gameObject.GetComponent<EffectBehaviour>() == null)
        {
            gameObject.AddComponent<EffectBehaviour>().Start();
        }
        gameObject.GetComponent<EffectBehaviour>().ParticleDamegeStartAutoMode();
    }
    public static void DamageStop(GameObject gameObject)
    {
        if (gameObject.GetComponent<EffectBehaviour>() != null)
        {
            gameObject.GetComponent<EffectBehaviour>().DamageStop();
        }
    }

    public void ParticleDamegeStartAutoMode()
    {
        ParticleSystem.Play();
        DamageRestart();
        PDAutoStop = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        effectStatus = GetComponent<EffectStatus>();
        if(GetComponent<CapsuleCollider>() != null)
        {
            damageCollider = GetComponent<CapsuleCollider>();
        }
        else if (GetComponent<BoxCollider>() != null)
        {
            damageCollider = GetComponent<BoxCollider>();
        }
        else if (GetComponent<SphereCollider>() != null)
        {
            damageCollider = GetComponent<SphereCollider>();
        }
        else if (GetComponent<MeshCollider>() != null)
        {
            damageCollider = GetComponent<MeshCollider>();
        }
        else if (GetComponent<WheelCollider>() != null)
        {
            damageCollider = GetComponent<WheelCollider>();
        }

        ParticleSystem = GetComponent<ParticleSystem>();
    }
    void DamageRestart()
    {
        effectTime = 0;
    }


    public void DamageStop()
    {
        damageCollider.enabled = false;
        effectTime = -1;
        damagetTime = 0;
    }

    void Update()
    {
        if (effectTime >= 0)
        {
            effectTime += Time.deltaTime;
            if (damageCollider.enabled == true)//ダメージ判定後一時的に無効化
            {
                damageCollider.enabled = false;
            }
            if (effectTime > damagetTime)//0.5秒毎にダメージ判定
            {
                damageCollider.enabled = true;
                damagetTime += 0.5f;
            }
            if (PDAutoStop && effectTime > effectStatus.time)
            {
                damageCollider.enabled = false;
                DamageStop();
                ParticleSystem.Stop();
                effectTime = -1;
                PDAutoStop = false;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // 物体がトリガーに接触しとき、敵HP減少
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().AddDamege(effectStatus.damage);
            }
        }
    }
}
