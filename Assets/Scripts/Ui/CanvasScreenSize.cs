using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Adjust the size of canvas for different screen sizes

[ExecuteInEditMode]
public class CanvasScreenSize : MonoBehaviour
{
    private void Awake()
    {
        if (gameObject.GetComponent<CanvasScaler>().referenceResolution != new Vector2(Screen.safeArea.width, Screen.safeArea.height))
        {
            gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.safeArea.width, Screen.safeArea.height);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (gameObject.GetComponent<CanvasScaler>().scaleFactor != Screen.safeArea.width / Screen.safeArea.height)
        {
            gameObject.GetComponent<CanvasScaler>().scaleFactor = Screen.safeArea.width / Screen.safeArea.height;
        }
    }
#endif
}
