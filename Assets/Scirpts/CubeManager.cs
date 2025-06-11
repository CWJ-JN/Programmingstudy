using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Cube1, 2, 3, 4, 5�� ������� 1�� �������� ����Ͽ�
// CylinderA -> B -> C -> D������ �̵��Ѵ�.
// �Ӽ� : Cube�� �ӵ�, Ÿ�ٵ�
public class CubeManager : MonoBehaviour
{
    public float speed;
    public GameObject cube;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public List<GameObject>  targets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        print("Cube ����!"); // Cube1 ���� -> �ݺ��� ����� ���� ����
        yield return MoveCubeToTargets(cube, targets);

        print("Cube ����!"); // Cube2 ����

        yield return MoveCubeToTargets(cube1, targets);

        print("Cube1 ����!"); // Cube3 ����

        yield return MoveCubeToTargets(cube2, targets);

        print("Cube2 ����!"); // Cube4 ����

        yield return MoveCubeToTargets(cube3, targets);

        print("Cube3 ����!"); // Cube5 ����

        yield return MoveCubeToTargets(cube4, targets);

        print("Cube4 ����!"); // Cube5 ����

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
