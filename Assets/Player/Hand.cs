using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Hand : MonoBehaviour
{
    public InputAction grip;
    public float gripThreshold = 0.35f; // how much the player needs to press for grip
    bool previousGripState = false;


    public float throwThreshold = 10.0f; // minimum speed needed to throw the disc

    public delegate void ThrowEvent();
    public event ThrowEvent OnThrow;

    float velocity;
    Vector3 previousPositon;

    void Start()
    {
        grip.Enable();
        previousPositon = transform.position;
    }

    void Update()
    {
        bool gripping = grip.ReadValue<float>() > gripThreshold;

        velocity = (transform.position - previousPositon).magnitude;

        if (!gripping && previousGripState && velocity >= throwThreshold)
        {
            OnThrow();
        }

        previousGripState = gripping;
    }
}
