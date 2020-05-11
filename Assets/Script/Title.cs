using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーンマネジメントを有効にする

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.S)) //スペースキー、Sボタンを押した場合
        {
            SceneManager.LoadScene("SampleScene");//SampleSceneをロードする
        }
    }
}