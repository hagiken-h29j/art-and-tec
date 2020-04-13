using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    //自陣。敵接触時とかの挙動。
    //今は敵接触消去。継続HP減少とかもあり
    //依存→Enemy
    //Resources→なし
    //Tag→Enemy

    public float HP = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        // 物体がトリガーに接触しとき、今は敵消去・HP減少
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
                HP -= collision.gameObject.GetComponent<Enemy>().ATK;
            }
            else
            {
                HP--;
            }
            Destroy(collision.gameObject);
            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
