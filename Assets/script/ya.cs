using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ya : MonoBehaviour
{


    //参照するアニメータークラス
    public Animator neuronAnimator;
    public Animator charaAnimator;
    private MonoBehaviour mono;
    public int status = 0;//0:初期、1:構え、2:放つ
    public int maxDistance = 10;
    private Rigidbody rb;
    private float pow;

    // Use this for initialization
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))//調整用
        {
            Debug.Log("RightIndexDistal.z: " + neuronAnimator.GetBoneTransform(HumanBodyBones.RightIndexDistal).localRotation.z);
            
            Debug.Log("RightIndexProximal.z: " + neuronAnimator.GetBoneTransform(HumanBodyBones.RightIndexProximal).localRotation.z);
            
            Debug.Log("RightIndexIntermediate.z: " + neuronAnimator.GetBoneTransform(HumanBodyBones.RightIndexIntermediate).localRotation.z);

            Debug.Log("2-RightIndexDistal.z: " + (charaAnimator.GetBoneTransform(HumanBodyBones.RightIndexProximal).localEulerAngles.z - 180));
            Debug.Log("2-RightIndexDistal.z: " + (charaAnimator.GetBoneTransform(HumanBodyBones.RightIndexIntermediate).localEulerAngles.z - 360));
            Debug.Log("2-RightIndexDistal.z: " + (charaAnimator.GetBoneTransform(HumanBodyBones.RightIndexDistal).localEulerAngles.z - 360));
            Debug.Log("2-RightIndexDistal.x: " + (charaAnimator.GetBoneTransform(HumanBodyBones.RightIndexDistal).localEulerAngles.x - 360));
            Debug.Log("2-RightIndexDistal.y: " + (charaAnimator.GetBoneTransform(HumanBodyBones.RightIndexDistal).localEulerAngles.y - 360));

        }

        if (neuronAnimator.GetBoneTransform(HumanBodyBones.RightIndexDistal).localRotation.z > 0.15 && status <= 1)
        {
            transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), charaAnimator.GetBoneTransform(HumanBodyBones.RightMiddleProximal).position - charaAnimator.GetBoneTransform(HumanBodyBones.LeftIndexIntermediate).position);
            transform.position = charaAnimator.GetBoneTransform(HumanBodyBones.RightMiddleProximal).position
                                 - transform.rotation * (new Vector3(0, 1, 0)) * transform.localScale.x * 6;
            pow = Vector3.Distance(charaAnimator.GetBoneTransform(HumanBodyBones.RightMiddleProximal).position, charaAnimator.GetBoneTransform(HumanBodyBones.LeftIndexIntermediate).position);
            status = 1;
        }
        else if (status == 1 && rb.velocity.magnitude < 10.0f)
        {
            //rb.AddForce(transform.rotation * (new Vector3(0, 1, 0)) * -10);
            rb.velocity = transform.rotation * (new Vector3(0, 1, 0)) * -10 * pow;
            status = 2;
        }
        else if (status == 2 && Vector3.Distance(Vector3.zero, transform.position) >= maxDistance)//消去
        {
            status = 0;
            rb.velocity = Vector3.zero;
        }
    }
}
