using System.Collections;
using UnityEngine;

// Label untuk sprite tokoh di scene, sekaligus efek muncul/menghilang perlahan
public class TokohDiScene : MonoBehaviour
{
    public Tokoh tokoh;
    public float durasiFade = 0.6f;

    public void Masuk()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Fade(0f, 1f, false));
    }

    public void Keluar()
    {
        if (!gameObject.activeSelf) return;
        StopAllCoroutines();
        StartCoroutine(Fade(1f, 0f, true));
    }

    IEnumerator Fade(float dari, float ke, bool nonaktifkanSetelahnya)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (float t = 0f; t < durasiFade; t += Time.deltaTime)
        {
            Color c = sr.color;
            c.a = Mathf.Lerp(dari, ke, t / durasiFade);
            sr.color = c;
            yield return null; // tunggu satu frame
        }

        Color akhir = sr.color;
        akhir.a = ke;
        sr.color = akhir;

        if (nonaktifkanSetelahnya) gameObject.SetActive(false);
    }
}