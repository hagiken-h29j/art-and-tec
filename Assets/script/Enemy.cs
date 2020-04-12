using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //敵の行動や判定。
    //依存→なし
    //Resources→なし
    //Tag→なし

    private Rigidbody rb;
    public float maxSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(-transform.position);
        }
    }
}
