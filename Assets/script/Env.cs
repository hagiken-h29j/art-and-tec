using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//参考 https://qiita.com/sea_mountain/items/6513b330983ffa003959
public class Env : MonoBehaviour
{
    public class Environment
    {
        public string GOOGLE_API_KEY;
    }

    public static string GetEnv(string key)
    {
        Environment item = JsonUtility.FromJson<Environment>((Resources.Load("env", typeof(TextAsset)) as TextAsset).text);

        // 環境変数が追加されたときここ追加
        if (key == "GOOGLE_API_KEY")
        {
            return item.GOOGLE_API_KEY;
        }

        return "ERROR";
    }
}
