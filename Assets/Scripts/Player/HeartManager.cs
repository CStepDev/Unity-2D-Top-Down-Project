using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] heartUI;
    public Sprite fullHeart; // Represents heart without damage taken
    public Sprite emptyHeart; // Represents heart after damage taken
    public IntValue heartContainers; // Uses value defined as HeartContainers in ScriptableObjects
    public IntValue playerCurrentHealth; // Uses value defined as 


    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.runTimeValue; i++)
        {
            if (i < heartUI.Length)
            {
                heartUI[i].gameObject.SetActive(true);
                heartUI[i].sprite = fullHeart;
            }
        }
    }


    public void UpdateHearts()
    {
        InitHearts();

        int tempHealth = playerCurrentHealth.runTimeValue;

        for (int i = 0; i < heartContainers.runTimeValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                heartUI[i].sprite = fullHeart;
            }
            else
            {
                heartUI[i].sprite = emptyHeart;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }
}
