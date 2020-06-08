using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMan : MonoBehaviour
{
    private GameObject enemySpawner;
    private Spawner spawnOpt;
    private List<GameObject> enemys = new List<GameObject>();
    private List<GameObject> spawners = new List<GameObject>();
    private int state = 0;
    private Vector3 spawnArea;

    public bool active = true;
    public float time;
    public List<float> stateTimes = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = Resources.Load<GameObject>("EnemySpawner");
        spawnOpt = enemySpawner.GetComponent<Spawner>();
        enemys.Add(Resources.Load<GameObject>("Enemy"));
        enemys.Add(Resources.Load<GameObject>("Enemy1"));
        enemys.Add(Resources.Load<GameObject>("Enemy2"));
        enemys.Add(Resources.Load<GameObject>("Enemy3"));
        enemys.Add(Resources.Load<GameObject>("Enemy4"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }

        time += Time.deltaTime;
        if (Option.stage == 1)
        {
            if (state == 0)
            {
                state = 1;
                spawnOpt.active = true;

                spawnOpt.spawnObj.Clear();
                spawnOpt.spawnObj.Add(enemys[1]);
                spawnOpt.maxSpawn = 10;
                spawnOpt.oneTimeSpawnNum = 2;
                spawnOpt.spawnTime = 3;
                spawnOpt.setSpawnArea(new Vector3(0.0f, 0.0f, 15.0f), new Vector3(10, 0.01f, 1));
                spawners.Add((GameObject)Instantiate(enemySpawner, Vector3.zero, Quaternion.identity));

                spawnOpt.spawnObj.Clear();
                spawnOpt.spawnObj.Add(enemys[1]);
                spawnOpt.spawnObj.Add(enemys[2]);
                spawnOpt.maxSpawn = 3;
                spawnOpt.oneTimeSpawnNum = 2;
                spawnOpt.spawnTime = 5;
                spawnOpt.setSpawnArea(new Vector3(0.0f, 0.0f, 18.0f), new Vector3(10, 0.01f, 1));
                spawners.Add((GameObject)Instantiate(enemySpawner, Vector3.zero, Quaternion.identity));

                spawnOpt.spawnObj.Clear();
                spawnOpt.spawnObj.Add(enemys[4]);
                spawnOpt.maxSpawn = 1;
                spawnOpt.oneTimeSpawnNum = 1;
                spawnOpt.spawnTime = 5;
                spawnOpt.setSpawnArea(new Vector3(-2.0f, 0.0f, 18.0f), new Vector3(1, 0.01f, 1));
                spawners.Add((GameObject)Instantiate(enemySpawner, Vector3.zero, Quaternion.identity));
                spawnOpt.spawnTime = 10;
                spawnOpt.setSpawnArea(new Vector3(0.0f, 0.0f, 18.0f), new Vector3(1, 0.01f, 1));
                spawners.Add((GameObject)Instantiate(enemySpawner, Vector3.zero, Quaternion.identity));
                spawnOpt.spawnTime = 15;
                spawnOpt.setSpawnArea(new Vector3(2.0f, 0.0f, 18.0f), new Vector3(1, 0.01f, 1));
                spawners.Add((GameObject)Instantiate(enemySpawner, Vector3.zero, Quaternion.identity));
            }
            else if(state == 1 && time > 10)
            {
                state = 2;
                spawnOpt.spawnObj.Clear();
                spawnOpt.spawnObj.Add(enemys[3]);
                spawnOpt.maxSpawn = 1;
                spawnOpt.oneTimeSpawnNum = 1;
                spawnOpt.spawnTime = 1;
                spawnOpt.setSpawnArea(new Vector3(0.0f, 0.0f, 25.0f), new Vector3(1, 0.01f, 1));
                spawners.Add((GameObject)Instantiate(enemySpawner, Vector3.zero, Quaternion.identity));
            }else if(state == 2)
            {
                bool finish = true;
                foreach(GameObject spawner in spawners)
                {
                    if(spawner.GetComponent<Spawner>().extinctionObj == false)
                    {
                        finish = false;
                    }
                }
                if (finish)
                {
                    foreach (GameObject spawner in spawners)
                    {
                        Destroy(spawner);
                    }
                    spawners.Clear();
                    stateTimes.Add(time);
                    state = 3;
                }
            }
        }
    }
}
