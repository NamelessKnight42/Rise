using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletVelocity;
    GameObject bullet;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (bullet == null)
            {
                Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position);
                Shoot(dir.normalized);
            }
            else
            {
                Destroy(bullet);
                bullet = null;
            }
        }
    }


    /// <summary>
    /// 发射钩爪
    /// </summary>
    /// <param name="dir"></param>
    void Shoot(Vector2 dir)
    {
        bullet = Instantiate(bulletPrefab, this.gameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().parent = this.gameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity * dir;
    }

    void DrawLine(Vector2 endPoint)
    {
        GetComponent<LineRenderer>().SetPosition(0, this.gameObject.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, endPoint); 
    }

    public void Pull()
    {
        StartCoroutine(PullIE());        
    }

    /// <summary>
    /// 钩爪和X之间连线
    /// </summary>
    /// <returns></returns>
    IEnumerator PullIE()
    {
        float timer = 0;
        while (bullet != null)
        {
            DrawLine(bullet.transform.position);
            Vector2 dir = bullet.transform.position - this.gameObject.transform.position;
            if (dir.magnitude < 0.5 || timer>1f)//距离过近或者超过一定时间都会销毁钩爪
            {
                Destroy(bullet);bullet = null;
            }
            GetComponent<Rigidbody2D>().AddForce(force * dir.normalized);
            timer += Time.deltaTime;
            yield return 0;
        }
        DrawLine(this.gameObject.transform.position);
    }
}
