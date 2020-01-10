using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistSE : MonoBehaviour {

    AudioSource audioSource;
    [SerializeField] GameObject dotSight;
    [SerializeField] GameObject target0;
    [SerializeField] GameObject target1;
    private GameObject mainTarget;

    Dictionary<string, int> opt;
    private float firstTime = 0;
    private float nextTime = 0;
    private int nextPlayFlag = 0;
    private float timeOut = 5;
    private float maxDist;

    // Use this for initialization
    void Start () {
        opt = Option.GetOptData();
        audioSource = GetComponent<AudioSource>();
        firstTime = 0;
        nextTime = 0;
        if(opt["range"] == 0)
        {
            mainTarget = target0;
        }
        else
        {
            mainTarget = target1;
        }
        maxDist = Vector3.Distance(mainTarget.transform.position, Vector3.zero);
}
	
	// Update is called once per frame
	void Update () {
		if(opt["assistSE"] == 1)
        {
            firstTime += Time.deltaTime;
            nextTime += Time.deltaTime;
            float dist;
            if (opt["range"] == 0)
            {
                dist = Vector2.Distance(new Vector2(dotSight.transform.position.x, dotSight.transform.position.z), new Vector2(mainTarget.transform.position.x, mainTarget.transform.position.z));
            }
            else
            {
                dist = Vector3.Distance(dotSight.transform.position,mainTarget.transform.position);
            }

            /*if (firstTime > timeOut)
            {
                audioSource.Play();
                firstTime = 0;
                nextTime = 0;
                nextPlayFlag = 1;
            }
            if(nextPlayFlag == 1 && nextTime >= (timeOut / 2) * (dist / maxDist))
            {
                audioSource.Play();
                nextPlayFlag = 0;
            }*/

            if (firstTime > timeOut * (dist / maxDist))
            {
                audioSource.Play();
                firstTime = 0;
            }
        }
	}
}
