using System.Collections;
using UnityEngine;

// RGB Lamp�� ������ ���� �ð��� ���� ���������� �����̰� �Ѵ�.
// �Ӽ� : �ð�, Lamp�� ����
public class LampManager : MonoBehaviour
{
    public float time;
    public Renderer RedLamp;
    public Renderer GreenLamp;
    public Renderer YelloLamp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ó�� ���� �� ������ �˰� �ʱ�ȭ
        // _Color�� Shader�� Ư�� �Ӽ� : ���������ο� ���� �ٸ���.
        RedLamp.material.SetColor("_BaseColor", Color.black);
        GreenLamp.material.SetColor("_BaseColor", Color.black);
        YelloLamp.material.SetColor("_BaseColor", Color.black);

        StartCoroutine(CoSartLAMP());
    }

    // �ڷ�ƾ�� ���, time �������� ���� �����ϴ� Lamp
    IEnumerator CoSartLAMP()
    {
        RedLamp.material.SetColor("_BaseColor", Color.red);
        YelloLamp.material.SetColor("_BaseColor", Color.black);
        GreenLamp.material.SetColor("_BaseColor", Color.black);
        yield return new WaitForSeconds(1);

        YelloLamp.material.SetColor("_BaseColor", Color.yellow);
        RedLamp.material.SetColor("_BaseColor", Color.black);
        GreenLamp.material.SetColor("_BaseColor", Color.black);

        yield return new WaitForSeconds(1);

        GreenLamp.material.SetColor("_BaseColor", Color.green);
        YelloLamp.material.SetColor("_BaseColor", Color.black);
        RedLamp.material.SetColor("_BaseColor", Color.black);

        
    }
}
