using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float maxWidth;
    private float minWidth;
    private float maxHeight;
    private float minHeight;

    private bool isSpeaking;
    private bool isZoomingIn;
    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private Transform targetState;
    

    public Transform test;

    // Start is called before the first frame update
    void Start()
    {
        maxWidth = Screen.width - Screen.width / 10;
        minWidth = Screen.width / 10;
        maxHeight = Screen.height - Screen.height / 6;
        minHeight = Screen.height / 6;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isSpeaking)
            {
                Debug.Log("stop speak");

                transform.position = savedPosition;
                transform.rotation = savedRotation;
                isSpeaking = false;
            }
            else
            {
                Debug.Log("start speak");
                Debug.Log("start zoom");
                savedPosition = transform.position;
                savedRotation = transform.rotation;
                isSpeaking = true;
                isZoomingIn = true;
                targetState = test;
            }
        }
        //Debug.Log(savedState.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isZoomingIn)
        {
            if (Vector3.Distance(transform.position, targetState.position) <= 10f)
            {
                Debug.Log("stop zoom");
                isZoomingIn = false;
                return;
            }
            transform.LookAt(targetState);
            transform.position = Vector3.Lerp(transform.position, targetState.position, Time.deltaTime);
            return;
        }

        if (isSpeaking)
            return;

        if (Input.mousePosition.x > maxWidth && transform.position.x < 23)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(10, 0, 0),
                Mathf.Clamp(Math.Abs(Input.mousePosition.x - maxWidth) / 100, 0, 3) * Time.deltaTime);
        }

        if (Input.mousePosition.x < minWidth && transform.position.x > -23)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-10, 0, 0),
                Mathf.Clamp(Math.Abs(Input.mousePosition.x - minWidth) / 100, 0, 3) * Time.deltaTime);
        }

        if (Input.mousePosition.y > maxHeight && transform.position.z < -2)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, 10),
                Mathf.Clamp(Math.Abs(Input.mousePosition.y - maxHeight) / 60, 0, 3) * Time.deltaTime);
        }

        if (Input.mousePosition.y < minHeight && transform.position.z > -23)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, -10),
                Mathf.Clamp(Math.Abs(Input.mousePosition.y - minHeight) / 60, 0, 3) * Time.deltaTime);
        }
    }
}