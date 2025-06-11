using System;
using UnityEngine;

// Cube�� CylinderA -> CylinderB�� �̵���Ű��
// �Ӽ� : ��ü�� �ӵ�, ������, ������
public class MovementEx : MonoBehaviour
{
    // ��Ʈ����Ʈ : �ν�����â�� �����ִ� �ɼ� -> private�ε��� �ۿ��� �ٲ� �� �ִ�
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
        // 1. A���� B�� ���ϴ� ���� -> 2. ��������(ũ�Ⱑ 1�� ����) -> 3. �÷��̾�� �������͸� ������
        Vector3 direction = end.transform.position - start.transform.position;
        Vector3 nomalizedDir = direction.normalized; // 2. ������ 1�� ����

        // ������ �� ���ΰ�? -> cylinderB���� -> �Ÿ�
        float distance = direction.magnitude;
        //print(distance);

        if (distance < 0.1f)
        {
            isCylinderA = !isCylinderA; // false -> true , true -> false
            return;
        }
    
             // �÷��̾�� �������͸� ������
            transform.position += nomalizedDir * speed * Time.deltaTime; // ������ ������ 1�� ����

    }
        
}       


