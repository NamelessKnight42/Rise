using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross_Claw : MonoBehaviour
{
    float timer;bool isCatch;
    public GameObject parent;
    private GameObject something;
    public float rate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 1f)
        {
            Destroy(this.gameObject);
        }
        else if (!isCatch)
        {
            timer += Time.deltaTime;
            DrawLine(parent.transform.position);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            GetComponent<Rigidbody2D>().simulated = false;
            something = collision.gameObject;
            isCatch =true;
            Pull(true);

        }
    }


    /// <summary>
    /// 参数为true时钩爪收缩
    /// </summary>
    /// <param name="Catch"></param>
    public void Pull(bool Catch)
    {
        if(Catch)
            StartCoroutine(PullIE());
    }

    /// <summary>
    /// 画线，从此物件到参数点
    /// </summary>
    /// <param name="endPoint"></param>
    void DrawLine(Vector2 endPoint)
    {
        GetComponent<LineRenderer>().SetPosition(0, this.gameObject.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, endPoint);
    }

    /// <summary>
    /// 钩爪和X之间连线并产生拉力
    /// </summary>
    /// <returns></returns>
    IEnumerator PullIE()
    {
        float timer = 0;
        while (true)
        {
            DrawLine(parent.transform.position);
            Vector2 dir = this.gameObject.transform.position - parent.transform.position;
        
            if (dir.magnitude < 0.5 || timer > 10f)//距离过近或者超过一定时间都会销毁钩爪
            {
                Destroy(this.gameObject); 
            }
            parent.GetComponent<Rigidbody2D>().AddForce(rate * dir.normalized);
            something.GetComponent<Rigidbody2D>().AddForce(-rate * dir.normalized);
            
            transform.parent = something.transform;
            timer += Time.deltaTime;
            yield return 0;
        }
    }
}
