using UnityEngine;


// Unity�� Lifecycle �޼���
// ��ü�� ����Ű�� �Է��� �޾� �̵���Ų��.
public class MovePlayer : MonoBehaviour
{
    public float speed = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("��Ƴ�");
    }

    // Update is called once per frame
    void Update()
    {
        // NewMethod();
        MoveWithTime();
    }

    private void MoveWithTime()
    {

        // ���̽�ƽ�� ��ǲ�� ����� -1~1�� ǥ���ϴ� �Լ�
        float horizontalInput = Input.GetAxis("Horizontal"); // ����Ű �¿츦 ������ ��
        float verticalInput = Input.GetAxis("Vertical"); // ����Ű�� ���ϸ� ������ ��

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
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
