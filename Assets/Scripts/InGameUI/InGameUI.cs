using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private float PlayerHealth;

    [SerializeField]
    private float MaxHealth;

    [SerializeField]
    private Image HealthImage;

    private void UpdateHealth()
    {
        HealthImage.fillAmount = PlayerHealth / MaxHealth;
    }

}
