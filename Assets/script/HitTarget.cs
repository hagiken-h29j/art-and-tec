using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour {

    [SerializeField] int targetMode = 0; 
    AudioSource audioSource;
    Dictionary<string, int> opt;
    // Use this for initialization
    void Start () {
        opt = Option.GetOptData();
        audioSource = GetComponent<AudioSource>();
        if(opt["range"] == targetMode)
        {
            audioSource.Play();
            transform.localScale = transform.localScale * 3 / opt["level"];
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("接触");
        //GameObject obj = (GameObject)Resources.Load("Sphere");
        //GameObject instance = (GameObject)Instantiate(obj,
        //                                              new Vector3(Mathf.Cos(Random.value * Mathf.PI), 0.0f, Mathf.Cos(Random.value * Mathf.PI)),
        //                                              Quaternion.identity);
        //Destroy(this.gameObject);

        var theta = Random.value * Mathf.PI;
        var fai = Random.value * Mathf.PI / 5 * opt["range"];
        this.transform.position = new Vector3(5 * Mathf.Cos(theta) * Mathf.Cos(fai),
                                              5 * Mathf.Sin(fai),
                                              5 * Mathf.Sin(theta) * Mathf.Cos(fai));
        audioSource.time = 0;
    }
}
