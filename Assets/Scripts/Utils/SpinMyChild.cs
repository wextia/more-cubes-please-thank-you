﻿using UnityEngine;
using System.Collections;

public class SpinMyChild : MonoBehaviour {
    [SerializeField]
    float spinSpeed;
    	
	void Update () {
        transform.RotateAround(transform.position, Vector3.up, spinSpeed * Time.deltaTime);
	}
}
