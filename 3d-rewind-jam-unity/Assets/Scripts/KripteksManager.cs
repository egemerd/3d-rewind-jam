using UnityEngine;

public class KripteksManager : MonoBehaviour
{
    [Header("Halkalarý Buraya Sürükle")]
    // Sahnedeki bütün halkalarýn (üzerinde KripteksDurdurucu olanlarýn) listesi
    public KripteksDurdurucu[] halkalar;

    private bool oyunBitti = false;

    void Update()
    {
        // Eðer oyun zaten bittiyse tekrar tekrar kontrol etme
        if (oyunBitti) return;

        // Her karede "Acaba hepsi doðru mu?" diye kontrol et
        if (TumHalkalarDogruMu())
        {
            oyunuKazan();
        }
    }

    bool TumHalkalarDogruMu()
    {
        // Listedeki her bir halkayý tek tek gez
        foreach (KripteksDurdurucu halka in halkalar)
        {
            // Eðer listedeki HERHANGÝ BÝR halka yanlýþ yerdeyse...
            if (halka.dogruYerdeMi == false)
            {
                return false; // ...direkt "Hayýr, henüz bitmedi" de.
            }
        }

        // Döngüden çýkabildiyse demek ki hepsi doðrudur
        return true;
    }

    void oyunuKazan()
    {
        oyunBitti = true;
        Debug.Log(">>> TEBRÝKLER! BÜTÜN KÝLÝTLER AÇILDI! <<<");

        // Buraya kapý açýlma animasyonu, ses efekti veya sonraki level kodu gelecek
        // Örnek: SceneManager.LoadScene("Level2");
    }
}