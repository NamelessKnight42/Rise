using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross_Controller : MonoBehaviour
{
    public GameObject clawPrefab;
    public float bulletVelocity;
    GameObject claw;

    public bool isControlled = false;//是否正在被操控


    public float reliveTimeLen;
    private float reliveTime = 0;//重生时间判断

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlled)
        {
            #region 秽土转生判断
            if (Input.GetKey(KeyCode.C))//按c键秽土转生
            {
                StartRelive();
            }

            #endregion

            if (Input.GetMouseButtonUp(0))
            {
                if (claw == null)
                {
                    Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position);
                    Shoot(dir.normalized);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (claw != null)
                {
                    Destroy(claw);
                    claw = null;
                }

            }
        }
    }


    /// <summary>
    /// 发射钩爪
    /// </summary>
    /// <param name="dir"></param>
    void Shoot(Vector2 dir)
    {
        claw = Instantiate(clawPrefab, this.gameObject.transform.position, Quaternion.identity);
        claw.GetComponent<Cross_Claw>().parent = this.gameObject;
        claw.GetComponent<Rigidbody2D>().velocity = bulletVelocity * dir;
    }


    #region 秽土转生函数
    public void StartRelive()
    {
        StartCoroutine(Relive());
    }

    IEnumerator Relive()//秽土转生函数
    {
        Debug.Log("进入了秽土转生");
        isControlled = false;
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
                Debug.Log("点进来了");
                while (time <= 30) time += 1;
               
                if (Message_Manager.instance.GetHaveReliveTo())
                {
                    reliveTo = Message_Manager.instance.GetReliveTo();
                    //进入动画函数并完成转移，暂定为RushB()
                    Debug.Log("进入RushB辣");
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
                Debug.Log("重生超时");
                yield break;
            }
            yield return 0;
        }

    }



    public void SetController(bool a)
    {
        isControlled = a;
    }

    public void InitMessage()//初始化信息，防止传递错误
    {
        Message_Manager.instance.setIsRelive(false);
        Message_Manager.instance.SetHaveReliveTo(false);
    }

    public void RushB(GameObject B)
    {
        string name = B.name;
        Debug.Log(name);
        switch (name)
        {
            case "Cube":
                Cube_Controller cube = B.GetComponent<Cube_Controller>();
                cube.SetController(true);
                break;
            case "Angle":
                Angle_Controller angle = B.GetComponent<Angle_Controller>();
                angle.SetController(true);
                break;
            case "Circle":
                Circle_Controller circle = B.GetComponent<Circle_Controller>();
                circle.SetController(true);
                Debug.Log("进入圆辣");
                break;
            case "Cross":
                Cross_Controller cross = B.GetComponent<Cross_Controller>();
                cross.SetController(true);
                break;
            default: break;


        }

    }
    void OnMouseDown()
    {
        if (Message_Manager.instance.GetIsRelive())
        {
            Debug.Log("succeed to access");
            Message_Manager.instance.SetHaveReliveTo(true);
            Message_Manager.instance.SetReliveTo(gameObject);
        }
        else
            Debug.Log("fail to access");
        //Debug.Log(gameObject.name);
    }
    #endregion



}
