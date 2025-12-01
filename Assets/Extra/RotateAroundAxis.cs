using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateAroundAxis : MonoBehaviour
{
    [SerializeField] private Vector3 axis;
    [SerializeField] public float speed = 30;

    void Update()
    {
        transform.Rotate(axis, speed * Time.deltaTime);
    }
}