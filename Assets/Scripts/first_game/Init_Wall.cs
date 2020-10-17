using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;

using UnityEngine;

public class Init_Wall : MonoBehaviour
{
    public GameObject stone;
    private float stoneTime = 0.5f;
    private float stoneTimeLen = 0.4f;

    public GameObject[] deadBody;
    private float bodyTime = 1.5f;
    private float bodyTimeLen = 1.5f;
    
    public GameObject wall;
    private Vector2 p1,p2;
    private int y = -10;


    // Start is called before the first frame update
    void Start()
    {
        p1.x = 10;
        p2.y=p1.y = -10;
        p2.x = -10;
        while(y<200)
        {
            Instantiate(wall, p1, Quaternion.identity);
            Instantiate(wall, p2, Quaternion.identity);
            y++;
            p2.y = p1.y = y;
        }
        Vector2 point;
        point.y = 20;
        point.x = -1;
        for(int i=1;i<=3;i++)
        {
            point.x=i;
            Instantiate(deadBody[i], point, Quaternion.identity);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        stoneTimeLen = (float)(0.4 - transform.position.y / 50 * 0.1);
        if(stoneTime<stoneTimeLen)
        {       
            stoneTime += Time.deltaTime;
        }
        else
        {
            Init_Stone();
            stoneTime = 0;
        }

        if (bodyTime<bodyTimeLen)
        {
            bodyTime+= Time.deltaTime;
        }
        else
        {
            Init_Body();
            bodyTime = 0;
        }
    }

    public void Init_Stone()
    {
        Vector2 stone_pi;
        for (int i = 1; i <= 2; i++)
        {
            stone_pi.y = this.transform.position.y + 15;
            int seed = (int)System.DateTime.Now.Ticks;
            UnityEngine.Random.InitState(seed+i);
            stone_pi.x = UnityEngine.Random.Range(-9f, 9f);
            Instantiate(stone, stone_pi, Quaternion.identity);
        }
    }

    private void Init_Body()
    {
        Vector2 bodyPi;
        bodyPi.y= this.transform.position.y + 15;
        int seed = (int)System.DateTime.Now.Ticks;
        UnityEngine.Random.InitState(seed);
        GameObject body = deadBody[UnityEngine.Random.Range(0, 3)];
        bodyPi.x= UnityEngine.Random.Range(-9f, 9f);
        Instantiate(body, bodyPi, Quaternion.identity);

    }
}
