using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle_Controller : MonoBehaviour
{
    //public int speed;
    public bool isControlled = true;//是否正在被操控
    public float stopTimeLen;
    public float force;
    public float cdTimeLen;
    float refreshCD = 0;

    //private bool is_keeping = false;//是否在蓄力
    //private Vector2 jump_dir;//三角跳的方向
    //private float keep_time = 0;//三角蓄力的时间
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && refreshCD<=0)
            {
                TimeStop();
            }
            else
            {
                if(refreshCD > 0)
                    refreshCD -= Time.deltaTime;
            }
            transform.up = GetComponent<Rigidbody2D>().velocity.normalized;
        }
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
        StartCoroutine(Jump());
        print("stop");
    }

    public void TimeRelease()
    {
        Time.timeScale = 1;
        refreshCD = cdTimeLen;
        print("release");
    }

    IEnumerator Jump()
    {
        float stopTime = Time.realtimeSinceStartup;
        while (true)
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position;
            //Vector2 pos = transform.position;
            //transform.up = Vector2.Lerp(transform.up, dir, 0.05f);
            //transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(transform.position, dir));
            this.gameObject.transform.up = dir.normalized;
            if (Input.GetKeyUp(KeyCode.Space))//如果正在时停则时停结束，进入cd
            {
                TimeRelease();
                yield break;
            }
            if (Input.GetMouseButtonDown(0))//如果按下鼠标并且正在时停
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = dir.normalized;
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(force * dir.normalized); 
                TimeRelease();
                break;
            }
            if (Time.realtimeSinceStartup - stopTime > stopTimeLen)
            {
                TimeRelease();
                yield break;
            }
            yield return 0;
        }
    }
}
