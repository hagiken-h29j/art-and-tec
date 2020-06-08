using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : NetworkBehaviour
{
    public List<GameObject> Targets;
    public GameObject NetworkManager;
    // Start is called before the first frame update
    void Start()
    {
        var Manager = NetworkManager.GetComponent<NetworkManager>();
        //Manager.StartServer();
        Manager.StartHost();

        //登録してあるオブジェクトを一括生成
        foreach(var Target in Targets)
        {
            var Obj = Instantiate(Target);

            try
            {
                Obj.GetComponent<EnablePrefabScripts>().EnableScripts();
            }
            catch
            {

            }

            NetworkServer.Spawn(Obj);
        }
    }


    /// <summary>
    /// ネットワーク経由でオブジェクトを生成（サーバーのみ動作可能）
    /// </summary>
    /// <param name="Target"></param>
    public void NetworkInstaniate(GameObject Target,Transform transform)
    {
        var Obj = Instantiate(Target, transform);
        NetworkServer.Spawn(Obj);
    }
}
