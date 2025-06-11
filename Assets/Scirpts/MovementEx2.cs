using System;
using UnityEngine;

// Cube를 CylA -> CylB -> CylC -> CylD로 순차적으로 이동
// 속성 : 물체의 속도, 실린더 배열
public class MovementEx2 : MonoBehaviour
{
    [SerializeField] private float speed;
    public GameObject[] targets;
    private int currentIndex = 0;

    void Start()
    {
        // 시작 위치가 targets[0]이라면 별도 초기화 안 해도 됨
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("Target 배열이 비어 있습니다.");
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
            currentIndex = (currentIndex + 1) % targets.Length; // 다음 목표 (순환)
            return;
        }

        transform.position += normalizedDir * speed * Time.deltaTime;
    }
}