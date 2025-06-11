using System;
using UnityEngine;
using UnityEngine.UIElements;


// Unity�� Lifecycle �޼���
// ��ü�� ����Ű�� �Է��� �޾� �̵���Ų��.
public class MovePlayer : MonoBehaviour
{
    public float speed = 2;
    public float rotspeed = 2;
    float xRot;
    float yRot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("��Ƴ�");

        // ȸ�� �ʱ�ȭ
        transform.rotation = Quaternion.identity; // ȸ���� ���� ����(identity rotation) (0,0,0)
        // transform.rotation = Quaternion.Euler(30, 45, 60); // ���� ���ʹϾ��� ����� �־��� ���� �ִ�
    }

    // Update is called once per frame
    void Update()
    {
        // NewMethod();
        MoveWithTime();

        RotatePlayer();
    }

    private void RotatePlayer()
    {
        // ���Ϸ�ȸ�� : 0~360 �����ϱ� ���� ������ ���� �־ ȸ��
        // ���ʹϾ�ȸ�� : 4����(x,y,z,w), ���Ϸ�ȸ���� ������ ������(gimbal lock)�� ����
        // ������ : ������ ȸ���� �ܺ��� ȸ���� ���� �������� �Ҿ������ ����

        // transform.eulerAngles = new Vector3(30, 45, 60);
        // �������� ����
        // transform.eulerAngles += new Vector3(1 * 0.1f, 1 * 0.1f, 0);

        // ���ʹϾ� ȸ�� ����
        //transform.Rotate(transform.up, 0.1f);    // �� �ڽ��� up vector ���� ȸ��
        //transform.Rotate(transform.right, 0.1f); // �� �ڽ��� right vector ���� ȸ��


        // rotate�� ���� ���
        //Quaternion.rotY90 = Quaternion.AngleAxis(0.1f, Vector3.up); // ���ʹϾ� ����
        //transform.rotation += rotY90;  // Quaternion�� ���Ѵ�.

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        print($"mouseX : {mouseX}, mouseY : {mouseY}");

        xRot += mouseX * rotspeed * Time.deltaTime;
        yRot += mouseY * rotspeed * Time.deltaTime;

        // -90 ~90
        yRot = Mathf.Clamp(yRot, -90, 90);  // ������ �����ϰ� ���� �� ���

        transform.rotation = Quaternion.Euler(-yRot, xRot, 0);
       
    }

    private void MoveWithTime()
    {

        // ���̽�ƽ�� ��ǲ�� ����� -1~1�� ǥ���ϴ� �Լ�
        float horizontalInput = Input.GetAxis("Horizontal"); // ����Ű �¿츦 ������ ��
        float verticalInput = Input.GetAxis("Vertical"); // ����Ű�� ���ϸ� ������ ��

        // Vector3 direction = new Vector3(horizontalInput, 0, verticalInput); // �Ϲ����� ���� ����
        
        // ������Ʈ ��ġ ���� ���� ����
        Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void NewMethod()
    {
        bool isAKeyDown = Input.GetKey(KeyCode.A);
        bool isSKeyDown = Input.GetKey(KeyCode.S);
        bool isWKeyDown = Input.GetKey(KeyCode.W);   // W Ű�� ���Ƚ��ϱ�? -> Input
        bool isDKeyDown = Input.GetKey(KeyCode.D);

        if (isWKeyDown)
        {
            // ���� ���ϱ�
            Vector3 direction = Vector3.forward * speed; // ���� ��ǥ�� ����
            Vector3 localDirection = transform.forward * speed; // ���� ��ǥ�� ����

            // ������ǥ�� ���� ������ġ + ���⺤��
            transform.position += localDirection;
        }
        if (isAKeyDown)
        {
            // ���� ���ϱ�
            Vector3 direction = Vector3.left * speed;
            Vector3 localDirection = -transform.right * speed;

            // ������ǥ�� ���� ������ġ + ���⺤��
            transform.position += localDirection;

        }
        if (isSKeyDown)
        {
            // ���� ���ϱ�
            Vector3 direction = Vector3.back * speed;
            Vector3 localDirection = -transform.forward * speed;

            // ������ǥ�� ���� ������ġ + ���⺤��
            transform.position += localDirection;
        }
        if (isDKeyDown)
        {
            // ���� ���ϱ�
            Vector3 direction = Vector3.right * speed;
            Vector3 localDirection = transform.right * speed;

            // ������ǥ�� ���� ������ġ + ���⺤��
            transform.position += localDirection;
        }
    }
}
