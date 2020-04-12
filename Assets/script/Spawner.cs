using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //スポナー。敵などの物体を出現させる.
    //使用する時子オブジェクトにSpawnAreaという名のブロックオブジェを用意し、スポーン範囲を決定する。
    //依存→なし
    //Resources→Enemy
    //Tag→なし
    public GameObject spawnObj;

    public int maxSpawn = 10;
    public int oneTimeSpawnNum = 4;
    public float spawnTime = 5;
    private float spawnTimeCount = 0;

    public Vector3 targetPos = Vector3.zero;
    private Transform spawnAreaTr;
    // Start is called before the first frame update
    void Start()
    {
        if(spawnObj == null)
        {
            spawnObj = (GameObject)Resources.Load("Enemy");
        }
        if ((spawnAreaTr = transform.Find("SpawnArea")) == null)
        {
            if (transform.childCount != 0)
            {
                spawnAreaTr = this.transform.GetChild(0);
            }
            else
            {
                spawnAreaTr = transform;
            }
            Debug.Log("スポーンエリア指用子オブジェクトがありません。名称SpawnAreaの子オブジェクトを用意してください。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimeCount += Time.deltaTime;
        if (spawnTimeCount > spawnTime)
        {
            for (int i = 0; i < oneTimeSpawnNum; i++)
            {
                if (transform.childCount - 1 < maxSpawn)
                {
                    Vector3 pos = spawnAreaTr.position - spawnAreaTr.lossyScale / 2;
                    pos.x += spawnAreaTr.lossyScale.x * Random.value;
                    pos.y += spawnAreaTr.lossyScale.y * Random.value;
                    pos.z += spawnAreaTr.lossyScale.z * Random.value;
                    GameObject obj = Instantiate(spawnObj, pos, Quaternion.LookRotation(targetPos - pos));
                    obj.transform.SetParent(transform);
                }
            }
            spawnTimeCount = 0;
        }

    }
}
