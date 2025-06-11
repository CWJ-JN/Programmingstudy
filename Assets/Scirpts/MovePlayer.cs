using System;
using UnityEngine;
using UnityEngine.UIElements;


// Unity의 Lifecycle 메서드
// 물체를 방향키의 입력을 받아 이동시킨다.
public class MovePlayer : MonoBehaviour
{
    public float speed = 2;
    public float rotspeed = 2;
    float xRot;
    float yRot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("살아남");

        // 회전 초기화
        transform.rotation = Quaternion.identity; // 회전이 없는 상태(identity rotation) (0,0,0)
        // transform.rotation = Quaternion.Euler(30, 45, 60); // 직접 쿼터니언을 만들어 넣어줄 수도 있다
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
        // 오일러회전 : 0~360 이해하기 쉬운 각도의 값을 넣어서 회전
        // 쿼터니언회전 : 4원수(x,y,z,w), 오일러회전의 단점인 짐벌락(gimbal lock)을 보완
        // 짐벌락 : 내부의 회전이 외부의 회전에 의해 자유도를 잃어버리는 현상

        // transform.eulerAngles = new Vector3(30, 45, 60);
        // 짐벌락의 예시
        // transform.eulerAngles += new Vector3(1 * 0.1f, 1 * 0.1f, 0);

        // 쿼터니언 회전 예시
        //transform.Rotate(transform.up, 0.1f);    // 내 자신의 up vector 기준 회전
        //transform.Rotate(transform.right, 0.1f); // 내 자신의 right vector 기준 회전


        // rotate와 같은 기능
        //Quaternion.rotY90 = Quaternion.AngleAxis(0.1f, Vector3.up); // 쿼터니언 정의
        //transform.rotation += rotY90;  // Quaternion을 곱한다.

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        print($"mouseX : {mouseX}, mouseY : {mouseY}");

        xRot += mouseX * rotspeed * Time.deltaTime;
        yRot += mouseY * rotspeed * Time.deltaTime;

        // -90 ~90
        yRot = Mathf.Clamp(yRot, -90, 90);  // 각도를 고정하고 싶을 때 사용

        transform.rotation = Quaternion.Euler(-yRot, xRot, 0);
       
    }

    private void MoveWithTime()
    {

        // 조이스틱의 인풋을 모방한 -1~1을 표현하는 함수
        float horizontalInput = Input.GetAxis("Horizontal"); // 방향키 좌우를 눌렀을 때
        float verticalInput = Input.GetAxis("Vertical"); // 방향키의 상하를 눌렀을 때

        // Vector3 direction = new Vector3(horizontalInput, 0, verticalInput); // 일반적인 벡터 정의
        
        // 오브젝트 위치 기준 벡터 정의
        Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void NewMethod()
    {
        bool isAKeyDown = Input.GetKey(KeyCode.A);
        bool isSKeyDown = Input.GetKey(KeyCode.S);
        bool isWKeyDown = Input.GetKey(KeyCode.W);   // W 키가 눌렸습니까? -> Input
        bool isDKeyDown = Input.GetKey(KeyCode.D);

        if (isWKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.forward * speed; // 월드 좌표계 기준
            Vector3 localDirection = transform.forward * speed; // 로컬 좌표계 기준

            // 월드좌표의 나의 현재위치 + 방향벡터
            transform.position += localDirection;
        }
        if (isAKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.left * speed;
            Vector3 localDirection = -transform.right * speed;

            // 월드좌표의 나의 현재위치 + 방향벡터
            transform.position += localDirection;

        }
        if (isSKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.back * speed;
            Vector3 localDirection = -transform.forward * speed;

            // 월드좌표의 나의 현재위치 + 방향벡터
            transform.position += localDirection;
        }
        if (isDKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.right * speed;
            Vector3 localDirection = transform.right * speed;

            // 월드좌표의 나의 현재위치 + 방향벡터
            transform.position += localDirection;
        }
    }
}
