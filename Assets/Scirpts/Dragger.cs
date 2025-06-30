using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    Coroutine moveCoroutine;
    bool isMoving;
    public void Move()
    {
        isMoving = !isMoving;

        if (isMoving)
        {
            if (Conveyor.Instance.direction == Conveyor.Direction.CW)
                moveCoroutine = StartCoroutine(Move(true));
            else
                moveCoroutine = StartCoroutine(Move(false));

        }
    }

    public void Stop()
    {
        StopCoroutine(moveCoroutine);
    }

    IEnumerator Move(bool isCW)
    {
        while (true)
        {
            if (Conveyor.Instance.isCW)
            {
                Vector3 dir = Conveyor.Instance.endPos.position - transform.position;
                Vector3 normalizedDir = dir.normalized;
                float distance = dir.magnitude;

                if (distance < 0.1f)
                {
                   

                    transform.position = Conveyor.Instance.startPos.position;
                    print("���� ��ġ�� �̵�");
                    
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).SetParent(null);
                    }

                }

                transform.position += normalizedDir * Conveyor.Instance.speed * Time.deltaTime;
            }
            else if(Conveyor.Instance.isCCW)
            {
                Vector3 dir = Conveyor.Instance.startPos.position - transform.position;
                Vector3 normalizedDir = dir.normalized;
                float distance = dir.magnitude;

                if (distance < 0.1f)
                {
                    transform.position = Conveyor.Instance.endPos.position;
                    print("���� ��ġ�� �̵�");

                    for(int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).SetParent(null);
                    }
                }

                transform.position += normalizedDir * Conveyor.Instance.speed * Time.deltaTime;
            }



            yield return new WaitForEndOfFrame();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("�ö�ƽ") || other.tag.Contains("�ݼ�"))
           other.gameObject.transform.SetParent(this.transform);
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("�ö�ƽ") || other.tag.Contains("�ݼ�"))
            other.gameObject.transform.SetParent(null);
    }
}
