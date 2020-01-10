﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    //参考https://teratail.com/questions/141994
    private LineRenderer lineRenderer;
    private int index = 0;
    public int  writeActive = 0;//0待機1筆記中-1筆記終了
    public float zpos = 0.5f;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // ラインの座標指定を、このラインオブジェクトのローカル座標系を基準にするよう設定を変更
        // この状態でラインオブジェクトを移動・回転させると、描かれたラインもワールド空間に
        // 取り残されることなく、一緒に移動・回転するはず
        lineRenderer.useWorldSpace = false;

        index = 0;
    }

    public void Writing(Collider collision)
    {
        lineRenderer = GetComponent<LineRenderer>();

        // ラインの座標指定を、このラインオブジェクトのローカル座標系を基準にするよう設定を変更
        // この状態でラインオブジェクトを移動・回転させると、描かれたラインもワールド空間に
        // 取り残されることなく、一緒に移動・回転するはず
        lineRenderer.useWorldSpace = false;

        // 物体がトリガーに接触している間、常に呼ばれる
        if (writeActive >= 0 && collision.gameObject.tag == "Pen")
        {
            writeActive = 1;
            var pos = collision.transform.position;
            pos.z = zpos;

            // さらにそれをラインオブジェクトにおけるローカル座標に直し...
            pos = transform.InverseTransformPoint(pos);

            // 得られたローカル座標をラインレンダラーに追加する
            index++;
            lineRenderer.positionCount = index;
            lineRenderer.SetPosition(index - 1, pos);
        }
    }

    public void WriteEnd(Collider collision)
    {
        // 物体がトリガーと離れたとき、１度だけ呼ばれる
        if (writeActive >= 1 && collision.gameObject.tag == "Pen")
        {
            writeActive = -1;
            index = 0;
        }
    }
}
