using System;
using UnityEngine;

// Cube를 CylinderA -> CylinderB로 이동시키나
// 속성 : 물체의 속도, 시작점, 목적지
public class MovementEx : MonoBehaviour
{
    // 어트리뷰트 : 인스펙터창에 보여주는 옵션 -> private인데도 밖에서 바꿀 수 있다
    [SerializeField] private float speed;
    public float Speed { get; set; }
    public GameObject cylinderA;
    public GameObject cylinderB;
    public bool isCylinderA = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isCylinderA)
            MoveAtoB(transform.gameObject, cylinderB);
        else
            MoveAtoB(transform.gameObject, cylinderA);
            
        
   
    }

    private void MoveAtoB(GameObject start, GameObject end)
    {
        // 1. A에서 B를 향하는 벡터 -> 2. 단위벡터(크기가 1인 벡터) -> 3. 플레이어에게 단위벡터를 더해줌
        Vector3 direction = end.transform.position - start.transform.position;
        Vector3 nomalizedDir = direction.normalized; // 2. 단위가 1인 벡터

        // 어디까지 갈 것인가? -> cylinderB까지 -> 거리
        float distance = direction.magnitude;
        //print(distance);

        if (distance < 0.1f)
        {
            isCylinderA = !isCylinderA; // false -> true , true -> false
            return;
        }
    
             // 플레이어에게 단위벡터를 더해줌
            transform.position += nomalizedDir * speed * Time.deltaTime; // 실행할 때마다 1씩 간다

    }
        
}       


