using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube_Controller : MonoBehaviour
{
    public float stopTimeLen;
    public float cdTimeLen;
    public float speed;
    
    public GameObject clawPrefab;

    private GameObject claw;

    public bool isControlled = false;//是否正在被操控
   
    private bool isStop = false;//是否正在时停

    private bool isFlying = false;

    private float refreshCD = 0;//cd时长记录

    private float moveLen=0;

    public float reliveTimeLen;
    private float reliveTime=0;//重生时间判断

    private Vector3 originPos;

    // Start is called before the first frame update

    void Start()
    {
        refreshCD = 0; 
        
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


            if (!isFlying)//不在飞行时的逻辑
            {
                GetComponent<Collider2D>().isTrigger = false;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (refreshCD <= 0 && !isStop)//如果cd好了，就进入时停,并刷新cd
                        TimeStop();
                }
                else
                {
                    if (refreshCD > 0)//如果不在时停则减少cd至0
                    {
                        refreshCD -= Time.deltaTime;
                    }
                }
            }
            else//飞行时的逻辑
            {
                GetComponent<Collider2D>().isTrigger = true;
                if ((this.gameObject.transform.position - originPos).magnitude > moveLen)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    this.gameObject.transform.position = claw.transform.position;
                    Destroy(claw);
                    isFlying = false;
                }
            }
        }
    }


    public void TimeStop()
    {
        Time.timeScale = 0;
        isStop = true;
       
        StartCoroutine(Flash());
        print("stop");
    }

    public void TimeRelease()
    {
        Time.timeScale = 1;
        refreshCD = cdTimeLen;
        isStop = false;
        print("release");
    }

    IEnumerator Flash()
    {
        
        float stopTime = Time.realtimeSinceStartup;
        while (true)
        {          
            if (Input.GetKeyUp(KeyCode.Space))//如果正在时停则时停结束，进入cd
            {                    
                TimeRelease();
                yield break;                
            }
            if (Input.GetMouseButtonDown(0))//如果按下鼠标并且正在时停
            {
                Vector2 pi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                claw = Instantiate(clawPrefab, pi, Quaternion.identity);
                Debug.Log("协程里的案件");
                Vector2 dir = claw.transform.position - this.gameObject.transform.position;
                GetComponent<Rigidbody2D>().velocity= speed * dir.normalized;
                moveLen = dir.magnitude;
                originPos = this.gameObject.transform.position;
                isFlying = true;
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
                //Debug.Log("点进来了");
                while (time <= 10000) time += 1;
                
                if(Message_Manager.instance.GetHaveReliveTo())
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
            default:break;


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
