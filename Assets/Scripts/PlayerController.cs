using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //���Player��CharacterController���
    private CharacterController cc;
    [Header("�ƶ�����")]
    //����player���ƶ��ٶ�
    public float moveSpeed;
    [Header("��Ծ����")]
    //����player����Ծ�ٶ�
    public float jumpSpeed;

    //�����ð���ֵ����������
    private float horizontalMove, verticalMove;

    //������λ�������Ʒ���
    private Vector3 dir;
    //������������
    public float gravity;
    //����y��ļ��ٶ�
    private Vector3 velocity;
    //���������λ��
    public Transform groundCheck;
    //����İ뾶
    public float checkRadius;
    //������Ҫ����ͼ��
    public LayerMask groundLayer;
    [Header("����ɫ״̬")]
    public bool isOnground;
    public bool isJump;
    void Start()
    {
        //��GetComponent<>()�������CharacterController
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        //ʹ��Physics.CheckSphere()�����ı�isOnground��ֵΪtrue
        isOnground = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (isOnground && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        //��Input.GetAxis()������ȡ���������ƶ���ֵ
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        //��Input.GetAxis()������ȡ����ǰ���ƶ���ֵ
        verticalMove = Input.GetAxis("Vertical") * moveSpeed;

        //��������Ϣ�洢��dir��
        dir = -transform.forward * verticalMove + transform.right * horizontalMove;
        //��CharacterController�е�Move()�����ƶ�Player
        cc.Move(dir * Time.deltaTime);

        //�����̰��ո��ʱ�������ɽ�ɫ����Ծ������ʹ��ɫֻ�ܹ���Ծһ��
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isOnground)
        {
            velocity.y = jumpSpeed;
            isJump = true;
        }

        //ͨ��ÿ���ȥ������ֵ�����½�
        velocity.y -= gravity * Time.deltaTime;
        //��CharacterController�е�Move()�����ƶ�y��
        cc.Move(velocity * Time.deltaTime);
    }
}