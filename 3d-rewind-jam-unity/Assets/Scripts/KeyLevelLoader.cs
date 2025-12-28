using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþtirmek için þart

public class KeyLevelLoader : MonoBehaviour
{
    [Header("Gidilecek Level Adý")]
    public string nextLevelName; // Unity Inspector'dan buraya level adýný yaz

    private bool playerYakinda = false; // Oyuncu dibimizde mi?

    void Update()
    {
        // Eðer oyuncu alanýn içindeyse VE 'G' tuþuna basarsa
        if (playerYakinda == true && Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G'ye basýldý, diðer levela gidiliyor...");
            SceneManager.LoadScene(nextLevelName);
        }
    }

    // Oyuncu anahtarýn alanýna girdiðinde
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerYakinda = true;
            Debug.Log("Anahtarýn yanýndasýn. G'ye bas!");
            // Ýstersen burada ekranda "G'ye Bas" yazýsýný aktif edebilirsin.
        }
    }

    // Oyuncu anahtarýn alanýndan çýktýðýnda
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerYakinda = false;
        }
    }
}