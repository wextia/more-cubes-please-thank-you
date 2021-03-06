﻿using UnityEngine;
using System.Collections;

public class EnemySimpleMovement : MonoBehaviour
{
    float speed = 6f;
    [SerializeField]
    float accelerationTime = 1f;

    public void Init(float speed)
    {
        this.speed = speed;
        StartCoroutine("LerpSpeed");
    }

    void Update()
    {
        if (!LevelControl.IsGameOver)
        {
            transform.LookAt(PlayerMovement.instance.transform);
            transform.position = Vector3.MoveTowards(transform.position, PlayerMovement.Pos,
                speed * Time.deltaTime);
        }
    }

    IEnumerator LerpSpeed()
    {
        float prevSpeed = speed;
        speed = 0;
        float currentTime = 0;
        while (speed < prevSpeed)
        {
            speed = Mathf.Lerp(0, prevSpeed, currentTime/accelerationTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
  
}
