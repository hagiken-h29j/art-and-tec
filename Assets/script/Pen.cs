using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pen : MonoBehaviour {
    //ペン。ペンを右人差し指の座標に合わせる。エンターで文字認識。
    //依存→Line.cs,EffectManager.cs
    //Resources→PenOneLine
    //Tag→Lines
    //Note,

    //参照するアニメータークラス
    public Animator targetAnimator;
    public GameObject markObj;
    private Vector3 notePos = new Vector3(0, 0, 1);

    public GameObject note;
    private GameObject oneline;
    public RenderTexture CamTex;
    public EffectManager effectManager;

    public bool debugMode = false;

    // Use this for initialization
    void Start () {
        if(markObj == null)
        {
            markObj = transform.GetChild(0).gameObject;
        }
        oneline = (GameObject)Resources.Load("PenOneLine");
        if(note == null)
        {
            note = GameObject.Find("Note");
        }
        notePos = note.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //penの座標変更処理
        transform.position = targetAnimator.GetBoneTransform(HumanBodyBones.RightIndexIntermediate).position;
        //https://qiita.com/edo_m18/items/c8808f318f5abfa8af1e
        var n = -notePos;
        var x = notePos;
        var x0 = transform.position;
        var m = transform.forward;
        var h = Vector3.Dot(n, x);

        var intersectPoint = x0 + ((h - Vector3.Dot(n, x0)) / (Vector3.Dot(n, m))) * m;

        markObj.transform.position = intersectPoint;

        //文字認識系統
        if (Input.GetKeyDown(KeyCode.Return)  || Input.GetMouseButtonDown(1))//enterで文字判定処理、ライン消去
        {
            foreach (Transform child in note.transform)//ライン消去
            {
                if (child.tag == "Lines")
                    Destroy(child.gameObject);
            }
            if (CamTex != null)
            {//文字判定処理
                //文字画像（string）データ取得
                Texture2D tex = new Texture2D(CamTex.width, CamTex.height, TextureFormat.RGB24, false);
                RenderTexture.active = CamTex;
                tex.ReadPixels(new Rect(0, 0, CamTex.width, CamTex.height), 0, 0);
                tex.Apply();

                byte[] bytes = tex.EncodeToPNG();
                string str = Convert.ToBase64String(bytes);//PNG画像データの文字列
                //Debug.Log(str);
                /*
#if UNITY_EDITOR
                string path = Directory.GetCurrentDirectory();
#else
string path = Application.persistentDataPath;
#endif
                File.WriteAllBytes(path + "/picture.png", bytes);//pngファイルの保存*/

                //判定
                string word = "";
                word = "焔";
                //word = 
                Debug.Log("認識文字：" + word);
                //各文字に応じた処理
                effectManager.EffectSelect(word);

            }
        }

        //線を書く処理
        if (Input.GetMouseButtonDown(0))
        {

            GameObject objBuff = Instantiate(oneline, transform.position, Quaternion.identity);
            objBuff.transform.parent = note.transform;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 vec = markObj.transform.position;
            foreach (Transform child in note.transform)
            {
                if (child.tag == "Lines")
                {
                    if (debugMode)
                    {
                        Vector3 mousePos = Input.mousePosition;
                        mousePos.z = notePos.z;
                        vec = Camera.main.ScreenToWorldPoint(mousePos);
                    }
                    child.GetComponent<Line>().Writing(vec);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            foreach (Transform child in note.transform)
            {
                if (child.tag == "Lines")
                {
                    child.GetComponent<Line>().WriteEnd();
                }
            }
        }
    }
}
