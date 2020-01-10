using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteLine : MonoBehaviour
{
    private GameObject oneline;
    // Start is called before the first frame update
    void Start()
    {
        oneline = (GameObject)Resources.Load("OneLine");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//クリック時書き始める。onelineオブジェクトの生成
        {
            GameObject objBuff = Instantiate(oneline, transform.position, Quaternion.identity);
            objBuff.transform.parent = transform;
        }
        if (Input.GetKeyDown(KeyCode.Return))//enterでline全削除
        {
            foreach (Transform child in transform)
            {
                if(child.tag == "Lines")
                Destroy(child.gameObject);
            }
        }
    }
}
