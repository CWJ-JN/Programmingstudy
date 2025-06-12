using UnityEngine;


// 시계바늘을 2초 동안 회전시킨다.
// 속성 : 시간
public class SLerpRotation : MonoBehaviour
{
    public float time;
    public float elapsedtime;
    public float startengle;
    public float endengle;
    Quaternion startQ;
    Quaternion endQ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startQ = Quaternion.AngleAxis(startengle, transform.forward);
        endQ = Quaternion.AngleAxis(endengle, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedtime += Time.deltaTime;

        if (elapsedtime > time)
            elapsedtime = 0;

        Quaternion q = Quaternion.Slerp(startQ, endQ, elapsedtime / time);

        transform.rotation = q;
    }
}
