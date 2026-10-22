using UnityEngine;

[System.Serializable]
public class BarisDialog
{
    public Tokoh tokoh;                      // kosongkan untuk narasi
    public Ekspresi ekspresi = Ekspresi.Normal;
    public bool dalamHati = false;           // teks miring, untuk pikiran barista
    [TextArea(2, 5)] public string teks;

    [Header("Kejadian (opsional)")]
    public Tokoh munculkan;                  // tokoh yang masuk ke scene saat baris ini tampil
    public Tokoh sembunyikan;                // tokoh yang pergi dari scene
    public AudioClip suara;                  // efek suara saat baris ini tampil
}

[CreateAssetMenu(fileName = "Naskah", menuName = "Warung Siber/Naskah Dialog")]
public class NaskahDialog : ScriptableObject
{
    public BarisDialog[] baris;
}