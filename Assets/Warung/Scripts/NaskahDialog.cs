using UnityEngine;

[System.Serializable]
public class BarisDialog
{
    public Tokoh tokoh;                      // kosongkan untuk narasi
    public Ekspresi ekspresi = Ekspresi.Normal;
    public bool dalamHati = false;           // teks miring, untuk pikiran barista
    [TextArea(2, 5)] public string teks;
}

[CreateAssetMenu(fileName = "Naskah", menuName = "Warung Siber/Naskah Dialog")]
public class NaskahDialog : ScriptableObject
{
    public BarisDialog[] baris;
}