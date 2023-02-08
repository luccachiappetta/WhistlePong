using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    [SerializeField] private Transform ballPos;
    [SerializeField] private float speed;
    private float bottomBounds = -3.5f;
    private float topBounds = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(-8f, -0f); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.localPosition.y > bottomBounds && ballPos.position.y < transform.localPosition.y)
        {
            transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0f);
        }
        
        if (transform.localPosition.y < topBounds && ballPos.position.y > transform.localPosition.y)
        {
            transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0f);
        }
    }
}
