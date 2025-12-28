using TMPro;
using UnityEngine;

public class PasswordLock : MonoBehaviour
{
    [Header("Password Settings")]
    [SerializeField] private string correctPassword = "1234";
    [SerializeField] private int maxPasswordLength = 4;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject passwordPanel;

    private string currentInput = "";
    private bool isPlayerInside = false;
    private bool isPanelOpen = false;

    private void Start()
    {
        UpdateDisplay();

    }

    private void Update()
    {
        if (isPlayerInside && InputManager.Instance != null &&
            InputManager.Instance.interactAction != null &&
            InputManager.Instance.interactAction.WasPressedThisFrame())
        {
            Debug.Log("E tuşuna basıldı (tek frame)");
            TogglePanel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player tag kontrolü
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Player kilit alanına girdi - E'ye bas");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Player ayrıldığında
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

            // Panel açıksa kapat
            if (isPanelOpen)
            {
                ClosePanel();
            }

            Debug.Log("Player kilit alanından çıktı");
        }
    }

    private void TogglePanel()
    {
        if (isPanelOpen)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

    private void OpenPanel()
    {
        if (passwordPanel == null) return;

        isPanelOpen = true;
        passwordPanel.SetActive(true);

        // Input'u temizle
        currentInput = "";
        UpdateDisplay();

        // Cursor'u göster
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Panel açıldı");
    }

    /// <summary>
    /// Panel'i kapat
    /// </summary>
    private void ClosePanel()
    {
        if (passwordPanel == null) return;

        isPanelOpen = false;
        passwordPanel.SetActive(false);

        // Cursor'u kilitle (FPS için)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Panel kapandı");
    }

    public void OnNumberButtonClick(string number)
    {
        if (currentInput.Length >= maxPasswordLength) return;

        currentInput += number;
        UpdateDisplay();
    }

    
    public void OnSubmitButtonClick()
    {
        if (currentInput == correctPassword)
        {
            OnPasswordCorrect();
        }
        else
        {
            OnPasswordIncorrect();
        }
    }

    
    public void OnClearButtonClick()
    {
        currentInput = "";
        UpdateDisplay();
    }

    
    public void OnDeleteButtonClick()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            // Şifreyi göster
            displayText.text = string.IsNullOrEmpty(currentInput) ? "----" : currentInput;

            // Eğer gizli göstermek istersen:
            
        }
    }

    
    private void OnPasswordCorrect()
    {
        Debug.Log(" Doğru şifre!");
        displayText.text = "AÇILDI!";
        displayText.color = Color.green;

        // Buraya istediğin işlemi ekle:
        // - Kapı aç
        // - Level geç
        // - Obje aktif et
        // - vs.
    }

    
    private void OnPasswordIncorrect()
    {
        Debug.Log(" Yanlış şifre!");
        displayText.text = "HATALI!";
        displayText.color = Color.red;

        // Şifreyi temizle
        Invoke(nameof(ResetDisplay), 1f);
    }

    private void ResetDisplay()
    {
        currentInput = "";
        displayText.color = Color.white;
        UpdateDisplay();
    }

    
}
