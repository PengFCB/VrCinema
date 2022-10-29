using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //获得player的transform
    public Transform player;
    //获取鼠标移动的值
    private float mouseX, mouseY;
    //添加鼠标灵敏度
    public float mouseSensitivity;
    //声明变量来累加mouseY
    public float xRotation;


    void Update()
    {
        //获得鼠标左右移动的值
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //获得鼠标上下移动的值
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //用数学函数Mathf.Clamp()将xRotation的值限制在一个范围内
        xRotation = Mathf.Clamp(xRotation, -70, 70);

        //使用transform中的Rotate()方法使player旋转
        player.Rotate(Vector3.up * mouseX);
        //使用transform.localRotation()方法使相机上下旋转
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}