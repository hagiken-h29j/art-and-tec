using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : MonoBehaviour
{
    //スポナー。敵などの物体を出現させる.
    //使用する時子オブジェクトにSpawnAreaという名のブロックオブジェを用意し、スポーン範囲を決定する。
    //依存→なし
    //Resources→Enemy
    //Tag→なし
    public List<GameObject> spawnObj = new List<GameObject>();

    public int maxSpawn = 10;
    public int oneTimeSpawnNum = 4;
    public float spawnTime = 5;
    private float spawnTimeCount = 0;

    public int spawnMode = 0;
    //0:一回、累計Spawn数がのmaxSpawn以下なら稼働
    //1:永続、現在Spawn数がのmaxSpawn以下なら稼働

    public int spawnCount;//累計スポーン数

    public Vector3 targetPos = Vector3.zero;

    public bool active = true;
    public bool extinctionObj = false;//spawnMode = 0で敵が全滅したかどうか
    private Transform spawnAreaTr;

    public Vector3 setPos;
    public Vector3 setScale;
    public bool setFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        if(spawnObj.Count == 0)
        {
            spawnObj.Add((GameObject)Resources.Load("Enemy"));
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
        if (setFlag)
        {
            spawnAreaTr.position = setPos;
            spawnAreaTr.localScale = setScale;
            setFlag = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (setFlag)
            {
                spawnAreaTr.position = setPos;
                spawnAreaTr.localScale = setScale;
                setFlag = false;
            }
            spawnTimeCount += Time.deltaTime;
            if (spawnTimeCount > spawnTime)
            {
                for (int i = 0; i < oneTimeSpawnNum; i++)
                {
                    if ((spawnMode == 0 && spawnCount - 1 < maxSpawn) 
                        ||(spawnMode == 1 && (transform.childCount-1) / spawnObj.Count < maxSpawn))
                    {
                        spawnCount++;
                        foreach (GameObject so in spawnObj)
                        {
                            Vector3 pos = spawnAreaTr.position - spawnAreaTr.lossyScale / 2;
                            pos.x += spawnAreaTr.lossyScale.x * Random.value;
                            pos.y += spawnAreaTr.lossyScale.y * Random.value;
                            pos.z += spawnAreaTr.lossyScale.z * Random.value;
                            GameObject obj = Instantiate(so, pos, Quaternion.LookRotation(targetPos - pos));
                            obj.transform.SetParent(transform);
                            NetworkServer.Spawn(obj);
                        }
                    }
                    if(spawnMode == 0 && spawnCount == maxSpawn)
                    {
                        active = false;
                    }
                }
                spawnTimeCount = 0;
            }
        }else if (transform.childCount == 1 && spawnCount == maxSpawn)
        {
            extinctionObj = true;
        }

    }

    public void setSpawnArea(Vector3 pos, Vector3 scale)
    {
        setPos = pos;
        setScale = scale;
        setFlag = true;
        return;
    }
}
