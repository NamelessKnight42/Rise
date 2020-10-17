using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Controller : MonoBehaviour
{
    public float stop_time_len;
    public float cd_time_len;
    public float speed;
   
    //public bool isControlled = false;//是否正在被操控
    
    private bool is_stop = false;//是否正在时停
    
    private float x_move, y_move;
    private float stop_time_cd = 0;//时停时长
    private float refresh_cd = 0;//cd时长记录
    private Vector2 pi;
    private Vector3 move_to;

    public float reliveTimeLen;
    private float reliveTime = 0;//重生时间判断

    private float scale = 0.2f;//时间流速
    // Start is called before the first frame update

    void Start()
    {
        refresh_cd = 0;
        pi = transform.position;
        move_to = transform.position;



    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < 0)
            Destroy(gameObject);
        if (this.tag == "isControlled")
        {
            #region 秽土转生判断
            if (Input.GetKeyDown(KeyCode.C))//按c键秽土转生
            {
                StartRelive();
            }

            #endregion
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
                    move_to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                   // Debug.Log("aaa");
                }
                pi = (move_to - transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = pi * speed;
        }
     
    }





    #region 秽土转生函数
    public void StartRelive()
    {
        StartCoroutine(Relive());
    }

    IEnumerator Relive()//秽土转生函数
    {
        Debug.Log("进入了秽土转生");
        this.tag = "abonded";
        float stopTime = Time.realtimeSinceStartup;
        GameObject reliveTo = null;
        Message_Manager.instance.setIsRelive(true);
        Time.timeScale = 0;

        while (true)
        {
            //Debug.Log("进入循环辣");
            if (Input.GetMouseButtonDown(0))//点击了某处的残骸
            {
                float time = 0;
               // Debug.Log("点进来了");
                while (time <= 30) time += 1;
                
                if (Message_Manager.instance.GetHaveReliveTo())
                {
                    reliveTo = Message_Manager.instance.GetReliveTo();
                    //进入动画函数并完成转移，暂定为RushB()
                    //Debug.Log("进入RushB辣");
                    InitMessage();
                    Time.timeScale = 1;
                    RushB(reliveTo);
                    yield break;
                }
            }


            if (Time.realtimeSinceStartup - stopTime > reliveTimeLen)
            {
                Time.timeScale = 1;
                //Message_Manager.instance.setIsRelive(false);
                //Debug.Log("重生超时");
                yield break;
            }
            yield return 0;
        }

    }



    public void SetController(string a)
    {
        this.tag = a;
    }

    public void InitMessage()//初始化信息，防止传递错误
    {
        Message_Manager.instance.setIsRelive(false);
        Message_Manager.instance.SetHaveReliveTo(false);
    }

    public void RushB(GameObject B)
    {
        string a = "isControlled";
        string name = B.name;
        
        switch (name)
        {
            case "Cube(Clone)":
                Cube_Controller cube = B.GetComponent<Cube_Controller>();
                cube.SetController(a);
                break;
            case "Angle(Clone)":
                Angle_Controller angle = B.GetComponent<Angle_Controller>();
                angle.SetController(a);
                break;
            case "Circle(Clone)":
                Circle_Controller circle = B.GetComponent<Circle_Controller>();
                circle.SetController(a);
               // Debug.Log("进入圆辣");
                break;
            case "Cross(Clone)":
                Cross_Controller cross = B.GetComponent<Cross_Controller>();
                cross.SetController(a);
                break;
            default: break;


        }

    }
    void OnMouseDown()
    {
        if (Message_Manager.instance.GetIsRelive())
        {
            //Debug.Log("succeed to access");
            Message_Manager.instance.SetHaveReliveTo(true);
            Message_Manager.instance.SetReliveTo(gameObject);
        }

    }
    #endregion
}
