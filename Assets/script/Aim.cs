using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    //照準。エフェクト発現場所を動かす。キャラから10m以内で動く
    //依存→なし
    //Resources→なし
    //Tag→なし


    public Animator targetAnimator;
    public HumanBodyBones startBone;
    public HumanBodyBones endBone;
    public GameObject aimObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = targetAnimator.GetBoneTransform(startBone).position;
        Vector3 endPos = targetAnimator.GetBoneTransform(endBone).position;
        if (startPos.y > endPos.y)
        {
            //https://qiita.com/edo_m18/items/c8808f318f5abfa8af1e
            var n = new Vector3(0, 1, 0);
            var x = new Vector3(0, -1, 0);
            var x0 = startPos;
            var m = (endPos - startPos).normalized;
            var h = Vector3.Dot(n, x);

            var intersectPoint = x0 + ((h - Vector3.Dot(n, x0)) / (Vector3.Dot(n, m))) * m;

            if(Vector3.Distance(intersectPoint, targetAnimator.GetBoneTransform(HumanBodyBones.Hips).position) <100) aimObject.transform.position = intersectPoint;
        }
    }
}
