using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle_Controller : MonoBehaviour
{
    public int speed;
    private bool is_controll = true;//是否正在被操控
    private bool is_keeping = false;//是否在蓄力
    private Vector2 jump_dir;//三角跳的方向
    private float keep_time = 0;//三角蓄力的时间
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_controll)
        {
            
            if(Input.GetKeyDown(KeyCode.Space)&&!is_keeping)
            {
                is_keeping = true;
            }
            if (Input.GetKeyUp(KeyCode.Space) && is_keeping)
            {
                is_keeping = false;
                GetComponent<Rigidbody2D>().AddForce(jump_dir*keep_time*speed, ForceMode2D.Impulse);
                keep_time = 0;
            }
            if(is_keeping)
            {
                jump_dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position);
                float angle = Mathf.Atan2(jump_dir.y, jump_dir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, angle);
                keep_time += Time.deltaTime;
            }
            //Debug.Log("aaa");
        }
    }
}
