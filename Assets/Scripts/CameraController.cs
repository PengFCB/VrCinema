using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //���player��transform
    public Transform player;
    //��ȡ����ƶ���ֵ
    private float mouseX, mouseY;
    //������������
    public float mouseSensitivity;
    //�����������ۼ�mouseY
    public float xRotation;


    void Update()
    {
        //�����������ƶ���ֵ
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //�����������ƶ���ֵ
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //����ѧ����Mathf.Clamp()��xRotation��ֵ������һ����Χ��
        xRotation = Mathf.Clamp(xRotation, -70, 70);

        //ʹ��transform�е�Rotate()����ʹplayer��ת
        player.Rotate(Vector3.up * mouseX);
        //ʹ��transform.localRotation()����ʹ���������ת
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}