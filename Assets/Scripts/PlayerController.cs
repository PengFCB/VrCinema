using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //获得Player的CharacterController组件
    private CharacterController cc;
    [Header("移动参数")]
    //定义player的移动速度
    public float moveSpeed;
    [Header("跳跃参数")]
    //定义player的跳跃速度
    public float jumpSpeed;

    //定义获得按键值的两个变量
    private float horizontalMove, verticalMove;

    //定义三位变量控制方向
    private Vector3 dir;
    //定义重力变量
    public float gravity;
    //定义y轴的加速度
    private Vector3 velocity;
    //检测点的中心位置
    public Transform groundCheck;
    //检测点的半径
    public float checkRadius;
    //定义需要检测的图层
    public LayerMask groundLayer;
    [Header("检测角色状态")]
    public bool isOnground;
    public bool isJump;
    void Start()
    {
        //用GetComponent<>()方法获得CharacterController
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        //使用Physics.CheckSphere()方法改变isOnground的值为true
        isOnground = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (isOnground && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        //用Input.GetAxis()方法获取按键左右移动的值
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        //用Input.GetAxis()方法获取按键前后移动的值
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;

        //将方向信息存储在dir中
        dir = -transform.forward * verticalMove + transform.right * horizontalMove;
        //用CharacterController中的Move()方法移动Player
        cc.Move(dir * Time.deltaTime);

        //当键盘按空格的时候可以完成角色的跳跃，并且使角色只能够跳跃一次
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isOnground)
        {
            velocity.y = jumpSpeed;
            isJump = true;
        }

        //通过每秒减去重力的值不断下降
        velocity.y -= gravity * Time.deltaTime;
        //用CharacterController中的Move()方法移动y轴
        cc.Move(velocity * Time.deltaTime);
    }
}