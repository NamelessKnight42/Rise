using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Controller : MonoBehaviour
{
    public float stopTimeLen;
    public float cdTimeLen;
    public float speed;
    public GameObject clawPrefab;

    private GameObject claw;

    public bool is_controll = false;//是否正在被操控
    //private bool is_book = false;//是否标定位置
    private bool isStop = false;//是否正在时停
    private bool isFlying = false;
    private float refreshCD = 0;//cd时长记录
    private float moveLen=0;
    private Vector3 originPos;

    // Start is called before the first frame update

    void Start()
    {
        refreshCD = 0; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_controll) 
        {
            if (!isFlying)//不在飞行时的逻辑
            {
                GetComponent<Collider2D> ().isTrigger = false;
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
                GetComponent<Collider2D >().isTrigger = true;
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
        //is_book = false;
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
                Debug.Log("aaa");
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
}
