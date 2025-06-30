using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System.Collections.Generic;

// ����(CW, CCW)�� ���� ��ü�� �о �̵���Ų��.
// �Ӽ� : ����, ��ü, ��ü�� �̵��ӵ�, �۵�����
public class Conveyor : MonoBehaviour
{
    public static Conveyor Instance;  // �̱��� ����

    public bool isOn;
    public bool isCW = true;
    public bool isCCW = false;
    public float speed;
    public List<Dragger> draggers;
    public Transform startPos; // dragger ���� ��ġ
    public Transform endPos; // dragger �� ��ġ

    public enum Direction
    {
        CW, // ������
        CCW // ������
    }
    public Direction direction = Direction.CW;

    // �ʱ�ȭ�� ���� ������ �ϴ� LifeCycle �޼���
    private void Awake()
    {
        if(Instance == null)  
            Instance = this;  // �̱��� �ʱ�ȭ
    }

    private void Start()
    {
        RotateConveyor(); // Conveyor ������ Dragger Ȱ��ȭ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOn = true;

            foreach(Dragger dragger in draggers)
            {
                dragger.Move();
            }

        }  
    }

           
   
    public void RotateConveyor()
    {


        foreach (Dragger dragger in draggers)
        {
            dragger.Move();
        }
    }


    //public void StopConveyor()
    //{
    //    isOn = false;

    //    foreach (Dragger dragger in draggers)
    //    {
    //        dragger.Move();
    //    }
    //}
}
