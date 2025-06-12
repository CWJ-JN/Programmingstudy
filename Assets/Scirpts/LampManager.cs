using System.Collections;
using UnityEngine;

// RGB Lamp의 색상을 정한 시간에 따라 순차적으로 깜빡이게 한다.
// 속성 : 시간, Lamp의 색상
public class LampManager : MonoBehaviour
{
    public float time;
    public Renderer RedLamp;
    public Renderer GreenLamp;
    public Renderer YelloLamp;
    Coroutine LampCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 처음 시작 시 색상을 검게 초기화
        // _Color는 Shader의 특정 속성 : 파이프라인에 따라 다르다.
        RedLamp.material.SetColor("_BaseColor", Color.black);
        GreenLamp.material.SetColor("_BaseColor", Color.black);
        YelloLamp.material.SetColor("_BaseColor", Color.black);

        LampCoroutine = StartCoroutine(CoSartLAMP());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StopCoroutine(LampCoroutine);
        }
    }

    // 코루틴을 사용, time 간격으로 점등 정렬하는 Lamp
    IEnumerator CoSartLAMP()
    {
        while (true)
        {
            RedLamp.material.SetColor("_BaseColor", Color.red);
            YelloLamp.material.SetColor("_BaseColor", Color.black);
            GreenLamp.material.SetColor("_BaseColor", Color.black);
            yield return new WaitForSeconds(2);

            YelloLamp.material.SetColor("_BaseColor", Color.yellow);
            RedLamp.material.SetColor("_BaseColor", Color.black);
            GreenLamp.material.SetColor("_BaseColor", Color.black);

            yield return new WaitForSeconds(3);

            RedLamp.material.SetColor("_BaseColor", Color.black);
            YelloLamp.material.SetColor("_BaseColor", Color.black);
            GreenLamp.material.SetColor("_BaseColor", Color.black);

            yield return new WaitForSeconds(1);

            GreenLamp.material.SetColor("_BaseColor", Color.green);
            YelloLamp.material.SetColor("_BaseColor", Color.black);
            RedLamp.material.SetColor("_BaseColor", Color.black);

            yield return new WaitForSeconds(1);

        }

    }
}
