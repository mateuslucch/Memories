using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeMultipleFont : MonoBehaviour
{

    [SerializeField] List<TextMeshProUGUI> textSource;

    void Start()
    {
        float tempFont = 1000;

        foreach (TextMeshProUGUI textBox in textSource)
        {
            if (textBox.fontSize < tempFont)
            {
                tempFont = textBox.fontSize;
            }
        }
        foreach (TextMeshProUGUI textBox in textSource)
        {
            textBox.enableAutoSizing = false;
            textBox.fontSize = tempFont;
        }
    }

}
