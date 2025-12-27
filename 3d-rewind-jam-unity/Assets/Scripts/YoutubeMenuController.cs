using UnityEngine;
using UnityEngine.UI; // UI elemanlarý için gerekli

public class YouTubeMenuController : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public GameObject fakeVideoPanel; // Tüm arayüzü kapsayan panel
    public Slider volumeSlider;
    public Button playButton;
    public Button fullscreenButton;

    void Start()
    {
        // 1. Oyunu baþlatýr baþlatmaz zamaný donduruyoruz.
        Time.timeScale = 0f;

        // 2. Ses slider'ýný mevcut ses seviyesine eþitliyoruz.
        volumeSlider.value = AudioListener.volume;

        // 3. Butonlara týklama olaylarýný baðlýyoruz (Kodla yapmak daha temizdir).
        playButton.onClick.AddListener(PlayGame);
        fullscreenButton.onClick.AddListener(ToggleFullscreen);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // PLAY TUÞU FONKSÝYONU
    void PlayGame()
    {
        // UI Panelini kapat (Sahne deðiþmiyor, sadece perde kalkýyor)
        fakeVideoPanel.SetActive(false);

        // Zamaný normal akýþýna döndür (Oyun baþlar)
        Time.timeScale = 1f;

        // Eðer oyunun FPS tarzýysa mouse'u kilitlemek gerekebilir:
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    // TAM EKRAN FONKSÝYONU
    void ToggleFullscreen()
    {
        // Tam ekransa pencereye, pencereyse tam ekrana geçer
        Screen.fullScreen = !Screen.fullScreen;
    }

    // SES FONKSÝYONU
    void SetVolume(float volume)
    {
        // Oyunun genel sesini ayarlar
        AudioListener.volume = volume;
    }
}