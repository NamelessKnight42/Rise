using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross_Controller : MonoBehaviour
{
    public GameObject clawPrefab;
    public float bulletVelocity;
    GameObject claw;

    public bool isControlled = false;//是否正在被操控


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlled)
        {
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

    

    
}
