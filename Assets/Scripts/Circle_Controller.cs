using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Controller : MonoBehaviour
{
    public float stop_time_len;
    public float cd_time_len;
    public float speed;
   
    private bool is_controll = false;//是否正在被操控
    
    private bool is_stop = false;//是否正在时停
    
    private float x_move, y_move;
    private float stop_time_cd = 0;//时停时长
    private float refresh_cd = 0;//cd时长记录

    // Start is called before the first frame update

    void Start()
    {
        refresh_cd = 0;



    }

    // Update is called once per frame
    void Update()
    {
        if (is_controll)
        {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (refresh_cd <= 0 && !is_stop)//如果cd好了，就进入时停,并刷新cd
                    {
                        is_stop = true;
                        
                        refresh_cd = cd_time_len;

                    }
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (is_stop)//如果正在时停则时停结束，进入cd
                    {
                        is_stop = false;
                        

                    }

                }

                if (is_stop)//记录时停时间，正在时停则增加，否则减少cd
                {
                    if (stop_time_cd >= stop_time_len)//进入cd并刷新时停时长
                    {
                        is_stop = false;
                        stop_time_cd = 0;
                      
                    }
                    stop_time_cd += Time.deltaTime;
                }
                else
                {
                    if (refresh_cd > 0)//如果不在时停则减少cd至0
                    {
                        refresh_cd -= Time.deltaTime;
                    }
                }


                if (Input.GetMouseButtonDown(1)|| Input.GetMouseButtonDown(0))//如果按下鼠标
                {

                    Vector2 pi = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized;
                    GetComponent<Rigidbody2D>().AddForce(pi * speed, ForceMode2D.Impulse);
                    Debug.Log("aaa");
                }
            }
     
    }
}
