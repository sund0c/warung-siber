using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelDialog;
    public Image potret;
    public TextMeshProUGUI teksNama;
    public TextMeshProUGUI teksDialog;
    public GameObject indikatorLanjut;

    [Header("Pengaturan")]
    public float hurufPerDetik = 40f;
    public NaskahDialog naskahAwal;   // diputar otomatis saat scene dimulai (untuk uji coba)

    public event Action SaatSelesai;  // dipanggil ketika naskah habis
    public bool SedangBerjalan { get; private set; }

    private NaskahDialog naskah;
    private int indeks;
    private Coroutine ketik;
    private bool sedangMengetik;

    void Start()
    {
        panelDialog.SetActive(false);
        if (naskahAwal != null) Mulai(naskahAwal);
    }

    public void Mulai(NaskahDialog n)
    {
        naskah = n;
        indeks = 0;
        SedangBerjalan = true;
        panelDialog.SetActive(true);
        Tampilkan();
    }

    void Update()
    {
        if (!SedangBerjalan) return;

        bool lanjut =
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Keyboard.current != null && (Keyboard.current.spaceKey.wasPressedThisFrame ||
                                          Keyboard.current.enterKey.wasPressedThisFrame));
        if (!lanjut) return;

        if (sedangMengetik)
        {
            // Klik saat teks masih berjalan = tampilkan seluruh kalimat langsung
            if (ketik != null) StopCoroutine(ketik);
            TandaiSelesaiMengetik();
        }
        else
        {
            indeks++;
            if (indeks < naskah.baris.Length) Tampilkan();
            else Akhiri();
        }
    }

    void Tampilkan()
    {
        BarisDialog b = naskah.baris[indeks];

        if (b.tokoh == null)
        {
            // Narasi: tanpa nama dan potret
            teksNama.text = "";
            potret.gameObject.SetActive(false);
        }
        else
        {
            teksNama.text = b.tokoh.namaTampil;
            teksNama.color = b.tokoh.warnaNama;

            Sprite s = b.tokoh.AmbilPotret(b.ekspresi);
            potret.gameObject.SetActive(true);
            potret.sprite = s;
            potret.color = (s != null) ? Color.white : b.tokoh.warnaPlaceholder;
        }

        teksDialog.text = b.dalamHati ? "<i>" + b.teks + "</i>" : b.teks;

        if (ketik != null) StopCoroutine(ketik);
        ketik = StartCoroutine(Ketik());
    }

    IEnumerator Ketik()
    {
        sedangMengetik = true;
        indikatorLanjut.SetActive(false);

        teksDialog.ForceMeshUpdate();
        int total = teksDialog.textInfo.characterCount;
        teksDialog.maxVisibleCharacters = 0;

        float jeda = 1f / hurufPerDetik;
        for (int i = 1; i <= total; i++)
        {
            teksDialog.maxVisibleCharacters = i;
            yield return new WaitForSeconds(jeda);
        }

        TandaiSelesaiMengetik();
    }

    void TandaiSelesaiMengetik()
    {
        teksDialog.maxVisibleCharacters = 99999;
        sedangMengetik = false;
        indikatorLanjut.SetActive(true);
    }

    void Akhiri()
    {
        SedangBerjalan = false;
        panelDialog.SetActive(false);
        SaatSelesai?.Invoke();
    }
}