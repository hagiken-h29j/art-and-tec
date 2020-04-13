using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pen : MonoBehaviour {
    //ペン。ペンを右人差し指の座標に合わせる。
    //依存→なし
    //Resources→なし
    //Tag→なし

    //参照するアニメータークラス
    public Animator targetAnimator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = targetAnimator.GetBoneTransform(HumanBodyBones.RightIndexIntermediate).position;

    }
}
