using System.Collections;
using UnityEngine;

// ������ �Ǹ� Sphere�� Capsule A->B->C->D->�ʱ���ġ�� �̵��ϴµ�, 
// ���� : �̵� �� interval�� 1��, �ʱ� ��ġ���� ����
// �Ӽ� : Capsule���� transform, interval, originPos(�ʱ���ġ)
public class LerpMovement2 : MonoBehaviour
{
    [Range(0, 5f)] public float ratio;
    [SerializeField] float time;
    public float elapsedTime;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    
    private Vector3 originPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPos = transform.position;
        StartCoroutine(MoveSequence());
    }    

    IEnumerator MoveSequence()
    {   
        yield return CoMoveAtoB(originPos, pointA.position, time);
        yield return CoMoveAtoB(pointA.position, pointB.position, time);
        yield return CoMoveAtoB(pointB.position, pointC.position, time);
        yield return CoMoveAtoB(pointC.position, pointD.position, time);
        yield return CoMoveAtoB(pointD.position, originPos, time);
    }

    // Update is called once per frame
    void Update()
    {
        //// 1. ������ �ð��� ���
        //elapsedTime = Time.deltaTime;

        //if (elapsedTime > time)
        //    elapsedTime = 0;

        //Vector3 position = Vector3.Lerp(pointA.position, pointB.position, ratio);
        //transform.position = position;
    }

    IEnumerator CoMoveAtoB(Vector3 a,  Vector3 b, float t)
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > time)
            {
                elapsedTime = 0;
                break;
            }
            Vector3 position = Vector3.Lerp(a, b, elapsedTime / time);
            transform.position = position;

            yield return new WaitForEndOfFrame();
        }
    }
}
