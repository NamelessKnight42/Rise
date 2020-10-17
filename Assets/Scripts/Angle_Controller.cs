using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Angle_Controller : MonoBehaviour
{
    //public int speed;
    //private string isControlled ;//是否正在被操控
    public float stopTimeLen;
    public float force;
    public float cdTimeLen;
    float refreshCD = 0;


    public float reliveTimeLen;
    private float reliveTime = 0;//重生时间判断
    private float scale = 0.2f;//时间流速

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.tag);
        if (this.transform.position.y < 0)
            Destroy(gameObject);

        if (this.tag == "isControlled")
        {
            //Debug.Log("三角正在被操控");
            #region 秽土转生判断
            if (Input.GetKeyDown(KeyCode.C))//按c键秽土转生
            {
                StartRelive();
            }

            #endregion


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
        Time.timeScale = scale;
        StartCoroutine(Jump());
        //print("stop");
    }

    public void TimeRelease()
    {
        Time.timeScale = 1;
        refreshCD = cdTimeLen;
        //print("release");
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


    #region 秽土转生函数
    public void StartRelive()
    {
        StartCoroutine(Relive());
    }

    IEnumerator Relive()//秽土转生函数
    {
        Debug.Log("进入了秽土转生");
        this.tag="abonded";
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
                //Debug.Log("点进来了");
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
               // Debug.Log("重生超时");
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
        //Debug.Log(name);
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
                //Debug.Log("进入圆辣");
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
