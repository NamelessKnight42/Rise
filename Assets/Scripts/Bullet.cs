using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float timer;bool isCatch;
    public GameObject parent;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            GetComponent<Rigidbody2D>().simulated = false;isCatch=true;
            parent.GetComponent<XController>().Pull();
        }
    }
}
