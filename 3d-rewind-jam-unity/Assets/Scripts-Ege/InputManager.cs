using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance{get; private set;}

    private PlayerInput playerInput;

    public InputAction rewindAction;


    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // Get PlayerInput
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component bulunamadý!");
            return;
        }
        InitializeActions();
    }

    private void InitializeActions()
    {
        rewindAction = playerInput.actions.FindAction("Player/Rewind");

        if (rewindAction == null)
        {
            Debug.LogError("Rewind action bulunamadý! Action Map: Player, Action: Rewind");
        }
        else
        {
            Debug.Log("Rewind action baþarýyla yüklendi!");
        }
    }
}
