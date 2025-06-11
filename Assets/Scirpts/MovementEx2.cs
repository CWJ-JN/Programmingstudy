using System;
using UnityEngine;

// Cube�� CylA -> CylB -> CylC -> CylD�� ���������� �̵�
// �Ӽ� : ��ü�� �ӵ�, �Ǹ��� �迭
public class MovementEx2 : MonoBehaviour
{
    [SerializeField] private float speed;
    public GameObject[] targets;
    private int currentIndex = 0;

    void Start()
    {
        // ���� ��ġ�� targets[0]�̶�� ���� �ʱ�ȭ �� �ص� ��
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("Target �迭�� ��� �ֽ��ϴ�.");
            enabled = false;
        }
    }

    void Update()
    {
        MoveToTarget(transform.gameObject, targets[currentIndex]);
    }

    private void MoveToTarget(GameObject start, GameObject end)
    {
        Vector3 direction = end.transform.position - start.transform.position;
        Vector3 normalizedDir = direction.normalized;
        float distance = direction.magnitude;

        if (distance < 0.1f)
        {
            currentIndex = (currentIndex + 1) % targets.Length; // ���� ��ǥ (��ȯ)
            return;
        }

        transform.position += normalizedDir * speed * Time.deltaTime;
    }
}