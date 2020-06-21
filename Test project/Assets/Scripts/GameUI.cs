using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Slider velocitySlider;
    [SerializeField] Text velocityText;
    GameManager gameManager;

    public void SetGameManager(GameManager gameManager) => this.gameManager = gameManager;

    private void Start()
    {
        OnVelocityChange();
    }

    public void OnVelocityChange()
    {
        velocityText.text = velocitySlider.value.ToString();
        gameManager.SetVelocity(velocitySlider.value);
    }
}
