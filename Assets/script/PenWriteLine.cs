using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class PenWriteLine : MonoBehaviour
{
    private GameObject oneline;
    public RenderTexture CamTex;
    public EffectManager effectManager;
    // Start is called before the first frame update
    void Start()
    {
        oneline = (GameObject)Resources.Load("PenOneLine");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))//enterで文字判定処理、ライン消去
        {
            foreach (Transform child in transform)//ライン消去
            {
                if(child.tag == "Lines")
                Destroy(child.gameObject);
            }
            if (CamTex != null) {//文字判定処理
                //文字画像（string）データ取得
                Texture2D tex = new Texture2D(CamTex.width, CamTex.height, TextureFormat.RGB24, false);
                RenderTexture.active = CamTex;
                tex.ReadPixels(new Rect(0, 0, CamTex.width, CamTex.height), 0, 0);
                tex.Apply();

                byte[] bytes = tex.EncodeToPNG();
                string str = Convert.ToBase64String(bytes);
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
                //word = 

                //各文字に応じた処理
                effectManager.EffectPlay(word);

            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        // 物体がトリガーに接触しとき、１度だけ呼ばれる
        if(collision.gameObject.tag == "Pen")
        {
            GameObject objBuff = Instantiate(oneline, transform.position, Quaternion.identity);
            objBuff.transform.parent = transform;
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Lines")
            {
                child.GetComponent<line>().Writing(collision);
            }
        }

    }

    private void OnTriggerExit(Collider collision)
    {

        foreach (Transform child in transform)
        {
            if (child.tag == "Lines")
            {
                child.GetComponent<line>().WriteEnd(collision);
            }
        }
    }
}
