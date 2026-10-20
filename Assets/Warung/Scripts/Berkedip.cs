using UnityEngine;
using UnityEngine.UI;

public class Berkedip : MonoBehaviour
{
    public float kecepatan = 4f;
    private Graphic grafik;

    void Awake() { grafik = GetComponent<Graphic>(); }

    void Update()
    {
        Color c = grafik.color;
        c.a = 0.3f + 0.7f * Mathf.Abs(Mathf.Sin(Time.unscaledTime * kecepatan));
        grafik.color = c;
    }
}