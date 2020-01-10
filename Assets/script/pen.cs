using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pen : MonoBehaviour {


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
