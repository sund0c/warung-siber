using UnityEngine;

public enum Ekspresi { Normal, Senang, Sedih, Kaget, Bingung, Serius }

[CreateAssetMenu(fileName = "Tokoh", menuName = "Warung Siber/Tokoh")]
public class Tokoh : ScriptableObject
{
    public string namaTampil = "Nama";
    public Color warnaNama = Color.white;
    public Color warnaPlaceholder = Color.gray; // dipakai selama belum ada gambar potret

    [System.Serializable]
    public class PotretEkspresi
    {
        public Ekspresi ekspresi;
        public Sprite sprite;
    }

    public PotretEkspresi[] potret;

    // Cari potret sesuai ekspresi; kalau tidak ada, pakai Normal; kalau tidak ada juga, null
    public Sprite AmbilPotret(Ekspresi e)
    {
        foreach (var p in potret)
            if (p.ekspresi == e && p.sprite != null) return p.sprite;
        foreach (var p in potret)
            if (p.ekspresi == Ekspresi.Normal && p.sprite != null) return p.sprite;
        return null;
    }
}