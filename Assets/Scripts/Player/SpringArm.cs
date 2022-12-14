using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    Vector2 RotRange = new Vector2(-40, 80);
    float rotSpeed = 40.0f;
    Vector2 curRot = Vector2.zero;
    public Transform myCam = null;
    Vector2 ZoomRange = new Vector2(1.5f, 10.0f);
    float curCamDist = 0.0f;
    float zoomSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        curRot.x = transform.localRotation.eulerAngles.x;
        curCamDist = -myCam.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        curRot.x += -Input.GetAxisRaw("Mouse Y") * rotSpeed;
        curRot.x = Mathf.Clamp(curRot.x, RotRange.x, RotRange.y);

        curRot.y = 0.0f;
        transform.parent.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * rotSpeed);

        Quaternion x = Quaternion.Euler(new Vector3(curRot.x, 0, 0));
        Quaternion y = Quaternion.Euler(new Vector3(0, curRot.y, 0));

        transform.localRotation = y * x;

        if (Input.GetAxisRaw("Mouse ScrollWheel") > Mathf.Epsilon || Input.GetAxisRaw("Mouse ScrollWheel") < Mathf.Epsilon)
        {
            curCamDist -= Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
            curCamDist = Mathf.Clamp(curCamDist, ZoomRange.x, ZoomRange.y);
        }

        myCam.transform.localPosition = new Vector3(0, 0, -curCamDist);
    }
}
