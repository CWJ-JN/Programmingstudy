using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System.Collections.Generic;

// 방향(CW, CCW)에 따라서 물체를 밀어서 이동시킨다.
// 속성 : 방향, 물체, 물체의 이동속도, 작동여부
public class Conveyor : MonoBehaviour
{
    public static Conveyor Instance;  // 싱글턴 패턴

    public bool isOn;
    public bool isCW = true;
    public bool isCCW = false;
    public float speed;
    public List<Dragger> draggers;
    public Transform startPos; // dragger 시작 위치
    public Transform endPos; // dragger 끝 위치

    public enum Direction
    {
        CW, // 정방향
        CCW // 역방향
    }
    public Direction direction = Direction.CW;

    // 초기화를 가장 빠르게 하는 LifeCycle 메서드
    private void Awake()
    {
        if(Instance == null)  
            Instance = this;  // 싱글턴 초기화
    }

    private void Start()
    {
        RotateConveyor(); // Conveyor 내부의 Dragger 활성화
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
