#define TEST_MODE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DronCtrl : MonoBehaviour
{
    public Transform[] wings;
    public Joystick leftJoyStick;
    public Joystick rightJoyStick;

    private Transform tr;
    public float wingSpeed = 3000.0f; // 로터의 회전 속도
    public float moveSpeed = 10.0f; // 드론의 이동 속도
    public float rotSpeed = 50.0f; // Yaw 회전 속도 
    private float throttle;
    private float yaw;
    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();// Generic type
        //tr = GetComponent("Transform") as Transform;
        //tr = (Transform)GetComponent("Transform");

        leftJoyStick = GameObject.FindGameObjectWithTag("LEFT_JOYSTICK").GetComponent<Joystick>();
        rightJoyStick = GameObject.FindGameObjectWithTag("RIGHT_JOYSTICK").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        throttle = leftJoyStick.Vertical; // -1.0 ~ 0.0f ~ +1.0f
        yaw = leftJoyStick.Horizontal;
        pitch = rightJoyStick.Vertical;
#endif

#if UNITY_EDITOR
        throttle = Input.GetAxis("Vertical"); // -1.0f ~ 0.0f ~+1.0f // 상승하강
        yaw = Input.GetAxis("Horizontal"); // 회전
        pitch = Input.GetAxis("DronMove"); // 좌우
#endif

#if TEST_MODE
        throttle = leftJoyStick.Vertical; // -1.0 ~ 0.0f ~ +1.0f
        yaw = leftJoyStick.Horizontal;
        pitch = rightJoyStick.Vertical;
#endif

        MoveUpDown(throttle);
        Yaw(yaw);
        Pitch(pitch);

        WingsOn(1.0f);
    }
    void WingsOn(float speed)
    {
        for(int i = 0; i<wings.Length; i++)
        {
            wings[i].Rotate(Vector3.up * Time.deltaTime * wingSpeed * speed);
        }
    }

    void MoveUpDown(float speed)
    {
        tr.Translate(Vector3.up * Time.deltaTime * moveSpeed * speed);

    }
    void Yaw(float speed)
    {
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * speed);
    }
    void Pitch(float speed)
    {
        tr.Translate(Vector3.forward * Time.deltaTime * moveSpeed * speed);
    }
}
