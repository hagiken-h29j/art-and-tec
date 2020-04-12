using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    void Start()
    {
        //csv参考https://note.com/macgyverthink/n/n83943f3bad60
        csvFile = Resources.Load("EffectList", typeof(TextAsset)) as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }
    }

    void Update()
    {
    }

    public int EffectSelect(string word)
    {
        //対応文字をcsvから見つけて発動
        foreach(string[] effectDatas in csvDatas)
        {
            //Debug.Log(effectDatas[0] + "：" + word);
            if (word.Equals(effectDatas[0]))
            {
                GameObject.Find(effectDatas[1]).GetComponent<EffectStatus>().EffectActivate(float.Parse(effectDatas[2]), float.Parse(effectDatas[3]), effectDatas[4]);
                break;
            }
        }
        return 0;
    }
}
