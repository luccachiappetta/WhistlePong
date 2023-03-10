using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed;
    public Vector2 direction;

    private int CPUScore = 0;
    [SerializeField] private TMP_Text CpuText;
    private int PlayerScore = 0;
    [SerializeField] private TMP_Text PlayerText;
    
    void Start()
    {
        direction = new Vector2(Random.Range(0,2) * 2-1, -0.5f).normalized;
    }
    
    void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
        
        //Score
        CpuText.text = CPUScore.ToString();
        PlayerText.text = PlayerScore.ToString();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "paddle":
                direction.x = -direction.x;
                speed++;
                break;
            case "walls":
                direction.y = -direction.y;
                break;
            case "CPUGoal":
                CPUScore++;
                StartCoroutine(NewBall());
                break;
            case "PlayerGoal":
                PlayerScore++;
                StartCoroutine(NewBall());
                break;
        }
    }

    IEnumerator NewBall()
    {
        Debug.Log("poop");
        speed = 0;
        transform.position = new Vector2(100f, 0);
        yield return new WaitForSeconds(3f);
        
        transform.position = Vector3.zero;
        direction = new Vector2(Random.Range(0,2) * 2-1, -0.5f).normalized;
        speed = 4;
    }
}
