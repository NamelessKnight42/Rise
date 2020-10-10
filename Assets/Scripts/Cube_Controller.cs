using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Controller : MonoBehaviour
{
    public float stop_time_len;
    public float cd_time_len;
    public GameObject cube_claw;
    private GameObject claw;
    private bool is_controll = false;//是否正在被操控
    private bool is_book = false;//是否标定位置
    private bool is_stop = false;//是否正在时停
    private bool is_flying = false;
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
            if (!is_flying)//不在飞行时的逻辑
            {
                GetComponent<Collider2D> ().isTrigger = false;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (refresh_cd <= 0 && !is_stop)//如果cd好了，就进入时停,并刷新cd
                    {
                        is_stop = true;
                        is_book = false;
                        refresh_cd = cd_time_len;

                    }
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (is_stop)//如果正在时停则时停结束，进入cd
                    {
                        is_stop = false;
                        is_flying = true;
                        
                    }

                }

                if (is_stop)//记录时停时间，正在时停则增加，否则减少cd
                {
                    if (stop_time_cd >= stop_time_len)//进入cd并刷新时停时长
                    {
                        is_stop = false;
                        stop_time_cd = 0;
                        if (is_book)
                        {

                            is_flying = true;
                           
                        }
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


                if (Input.GetMouseButtonDown(0) && is_stop && !is_book)//如果按下鼠标并且正在时停
                {

                    Vector2 pi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    claw = Instantiate(cube_claw, pi, Quaternion.identity);

                    Debug.Log("aaa");
                    is_book = true;
                }
            }
            else//飞行时的逻辑
            {
                GetComponent<Collider2D >().isTrigger = true;


            }
         }
    }

    private void flash_move()
    {
        Vector2 dir = claw.transform.position-this.gameObject.transform.position ;
        GetComponent<Rigidbody2D>().AddForce(100 * dir);
    }
}
