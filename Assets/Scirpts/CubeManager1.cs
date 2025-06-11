using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Cube1, 2, 3, 4, 5�� ������� 1�� �������� ����Ͽ�
// CylinderA -> B -> C -> D������ �̵��Ѵ�.
// �Ӽ� : Cube�� �ӵ�, Ÿ�ٵ�
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
        // �ڸ�ƾ �޼��� ����
        StartCoroutine(CoStart());
    }

    // Update is called once per frame
    void Update()
    {
        print("update");
    }

    // �ڷ�ƾ �޼��� : ���μ��� ������ ��� ��ٸ� �� �ִ� ���
    IEnumerator CoStart()
    {
        // �ڷ�ƾ �޼���2 ����
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

        print(cube.gameObject.name + "���!");

        while (true)
        {
            // 1. A���� B�� ���ϴ� ���� -> 2. ��������(ũ�Ⱑ 1�� ����) -> 3. �÷��̾�� �������͸� ������
            Vector3 direction = targets[index].transform.position - cube.transform.position;
            Vector3 nomalizedDir = direction.normalized; // 2. ������ 1�� ����

            // ������ �� ���ΰ�? -> cylinderB���� -> �Ÿ�
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
            cube.transform.position += nomalizedDir * speed * Time.deltaTime; // ������ ������ 1�� ����

            yield return null;
        }
    }
}
