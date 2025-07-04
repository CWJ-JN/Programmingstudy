using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Cube1, 2, 3, 4, 5를 순서대로 1초 간격으로 출발하여
// CylinderA -> B -> C -> D순으로 이동한다.
// 속성 : Cube의 속도, 타겟들
public class CubeManager1 : MonoBehaviour
{
    public float speed = 5;
    public float interval = 2;
    public GameObject cube;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public List<GameObject>  targets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 코르틴 메서드 시작
        StartCoroutine(CoStart());
    }

    // Update is called once per frame
    void Update()
    {
        print("update");
    }

    // 코루틴 메서드 : 프로세스 내에서 잠깐 기다릴 수 있는 기능
    IEnumerator CoStart()
    {
        // 코루틴 메서드2 시작
        StartCoroutine(MoveCubeToTargets(cube4, targets));

        yield return new WaitForSeconds(interval);

        StartCoroutine(MoveCubeToTargets(cube3, targets));

        yield return new WaitForSeconds(interval);

        StartCoroutine(MoveCubeToTargets(cube2, targets));

        yield return new WaitForSeconds(interval);

        StartCoroutine(MoveCubeToTargets(cube1, targets));

        yield return new WaitForSeconds(interval);

        StartCoroutine(MoveCubeToTargets(cube, targets));

        yield return new WaitForSeconds(interval);

    }


    IEnumerator MoveCubeToTargets(GameObject cube, List<GameObject> targets)
    {
        int index = 0;

        print(cube.gameObject.name + "출발!");

        while (true)
        {
            // 1. A에서 B를 향하는 벡터 -> 2. 단위벡터(크기가 1인 벡터) -> 3. 플레이어에게 단위벡터를 더해줌
            Vector3 direction = targets[index].transform.position - cube.transform.position;
            Vector3 nomalizedDir = direction.normalized; // 2. 단위가 1인 벡터

            // 어디까지 갈 것인가? -> cylinderB까지 -> 거리
            float distance = direction.magnitude;
            print(distance);

            if (distance < 0.1f)
            {
                index++;

                if(index == targets.Count)
                {
                    break;
                }
                // CylA -> CylB -> CylC -> CylD
                yield return new WaitForEndOfFrame();
            }
            cube.transform.position += nomalizedDir * speed * Time.deltaTime; // 실행할 때마다 1씩 간다

            yield return null;
        }
    }
}
