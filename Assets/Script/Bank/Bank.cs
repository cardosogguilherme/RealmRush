using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;

    public int CurrentBalance { get { return currentBalance; } }

    private void Awake() {
        currentBalance = startingBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdrawn(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        UpdateDisplay();
        
        if (currentBalance < 0)
        {
            // Lose the game
            ReloadScene();
        }
    }

    private void UpdateDisplay()
    {
        displayBalance.text = $"Gold: {currentBalance}";
    }

    private void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
