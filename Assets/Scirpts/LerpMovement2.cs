using System.Collections;
using UnityEngine;

// 시작이 되면 Sphere가 Capsule A->B->C->D->초기위치로 이동하는데, 
// 조건 : 이동 시 interval은 1초, 초기 위치에서 시작
// 속성 : Capsule들의 transform, interval, originPos(초기위치)
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
        //// 1. 지나간 시간을 계산
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
