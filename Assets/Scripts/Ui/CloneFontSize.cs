using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CloneFontSize : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textSource;

    private void Start()
    {
        CloneTheFontSize();
    }

    private void Update()
    {
        CloneTheFontSize();
    }

    private void OnEnable()
    {
        CloneTheFontSize();
    }

    private void CloneTheFontSize()
    {
        if (gameObject.GetComponent<TextMeshProUGUI>().fontSize != textSource.fontSize)
        {
            gameObject.GetComponent<TextMeshProUGUI>().fontSize = textSource.fontSize;
        }
    }
}
