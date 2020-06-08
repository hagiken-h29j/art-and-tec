//参考https://web.archive.org/web/20180718102139/http://www.baku-dreameater.net/archives/5741
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConnectToneuronRobot : MonoBehaviour {

    //マネ対象のアニメータークラス
    public Animator targetAnimator;

    //このオブジェクト自身のアニメーター
    private Animator animator;

    //初期状態の回転情報キャッシュ
    private Dictionary<HumanBodyBones, Quaternion> initialRotations;
    private Dictionary<HumanBodyBones, Vector3> pseudXaxis;
    private Dictionary<HumanBodyBones, Vector3> pseudYaxis;
    private Dictionary<HumanBodyBones, Vector3> pseudZaxis;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        InitializeLocalRotations();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (targetAnimator != null && animator != null)
        {
            CopyRotations(targetAnimator, animator);
        }
    }


    private void CopyRotations(Animator src, Animator dest)
    {
        foreach (var bone in targetBones)
        {
            //ボーンがデフォでWorldのXYZに沿ってる場合の回転を表すパラメタをまず拾う
            float angle;
            Vector3 axis;
            src.GetBoneTransform(bone).localRotation.ToAngleAxis(out angle, out axis);

            Vector3 axisInLocalCoordinate = axis.x * pseudXaxis[bone] + axis.y * pseudYaxis[bone] + axis.z * pseudZaxis[bone];

            Quaternion modifiedRotation = Quaternion.AngleAxis(angle, axisInLocalCoordinate);

            dest.GetBoneTransform(bone).localRotation =
                initialRotations[bone] *
                modifiedRotation;
        }
    }

    private void InitializeLocalRotations()
    {
        initialRotations = targetBones.ToDictionary(
            b => b,
            b => animator.GetBoneTransform(b).localRotation
            );

        var rootT = animator.GetBoneTransform(HumanBodyBones.Hips).root;

        pseudXaxis = targetBones.ToDictionary(
            b => b,
            b =>
            {
                var t = animator.GetBoneTransform(b);
                return new Vector3(
                    Vector3.Dot(t.right, rootT.right),
                    Vector3.Dot(t.up, rootT.right),
                    Vector3.Dot(t.forward, rootT.right)
                    );
            });

        pseudYaxis = targetBones.ToDictionary(
            b => b,
            b =>
            {
                var t = animator.GetBoneTransform(b);
                return new Vector3(
                    Vector3.Dot(t.right, rootT.up),
                    Vector3.Dot(t.up, rootT.up),
                    Vector3.Dot(t.forward, rootT.up)
                    );
            });

        pseudZaxis = targetBones.ToDictionary(
            b => b,
            b =>
            {
                var t = animator.GetBoneTransform(b);
                return new Vector3(
                    Vector3.Dot(t.right, rootT.forward),
                    Vector3.Dot(t.up, rootT.forward),
                    Vector3.Dot(t.forward, rootT.forward)
                    );
            });


    }

    //コピー対象になるボーン一覧(もちろん足したり削ったりしてOK)
    private static HumanBodyBones[] targetBones = new[]
    {
        HumanBodyBones.Hips,

        HumanBodyBones.RightUpperLeg,
        HumanBodyBones.RightLowerLeg,
        HumanBodyBones.RightFoot,
        HumanBodyBones.LeftUpperLeg,
        HumanBodyBones.LeftLowerLeg,
        HumanBodyBones.LeftFoot,

        HumanBodyBones.Spine,
        HumanBodyBones.Chest,
        HumanBodyBones.Neck,
        HumanBodyBones.Head,

        HumanBodyBones.RightShoulder,
        HumanBodyBones.RightUpperArm,
        HumanBodyBones.RightLowerArm,
        HumanBodyBones.LeftShoulder,
        HumanBodyBones.LeftUpperArm,
        HumanBodyBones.LeftLowerArm,


        HumanBodyBones.RightHand,
        HumanBodyBones.LeftHand,
      /*  HumanBodyBones.RightThumbProximal,
        HumanBodyBones.RightThumbIntermediate,
        HumanBodyBones.RightThumbDistal,
        HumanBodyBones.RightIndexProximal,
        HumanBodyBones.RightIndexIntermediate,
        HumanBodyBones.RightIndexDistal,
        HumanBodyBones.RightMiddleProximal,
        HumanBodyBones.RightMiddleIntermediate,
        HumanBodyBones.RightMiddleDistal,
        HumanBodyBones.RightRingProximal,
        HumanBodyBones.RightRingIntermediate,
        HumanBodyBones.RightRingDistal,
        HumanBodyBones.RightLittleProximal,
        HumanBodyBones.RightLittleIntermediate,
        HumanBodyBones.RightLittleDistal,*/

    };
}
