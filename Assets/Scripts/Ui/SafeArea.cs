using UnityEngine;

// Adjust size of canvas to the same of Safe Area (mobiles)
// Offset will change the distance from borders (change on inspector)

[ExecuteInEditMode]
public class SafeArea : MonoBehaviour
{

    [Range(0, 0.5f)][SerializeField] float offsetX, offsetY;

    private void Start()
    {
        SafeAreaAnchor();
    }

#if UNITY_EDITOR
    private void Update()
    {
        SafeAreaAnchor();
    }
#endif

    private void SafeAreaAnchor()
    {
        Camera mainCamera = Camera.main;

        // calculate screen size in units
        float heightCameraUnits = mainCamera.orthographicSize * 2;
        float widthCameraUnits = mainCamera.pixelWidth * heightCameraUnits / mainCamera.pixelHeight; //mainCamera.pixel* change with device

        // store safe area pixels size in a vector2
        Vector2 safeAreaSizePixels = new Vector2(Screen.safeArea.width, Screen.safeArea.height);

        // size of safe area in units
        Vector2 safeAreaSizeUnits = new Vector2(
            safeAreaSizePixels.x * widthCameraUnits / mainCamera.pixelWidth,
            safeAreaSizePixels.y * heightCameraUnits / mainCamera.pixelHeight);


        Vector2 safeAreaPositionUnits = new Vector2(
            safeAreaSizeUnits.x * Screen.safeArea.position.x / safeAreaSizePixels.x,
            safeAreaSizeUnits.y * Screen.safeArea.position.y / safeAreaSizePixels.y);

        // safe area anchors
        Vector2 safeAreaMin = new Vector2(
            (safeAreaPositionUnits.x / widthCameraUnits) + offsetX,
            (safeAreaPositionUnits.y / heightCameraUnits) + offsetY);

        Vector2 safeAreaMax = new Vector2(
            (safeAreaPositionUnits.x + safeAreaSizeUnits.x) / widthCameraUnits - offsetX,
            (safeAreaPositionUnits.y + safeAreaSizeUnits.y) / heightCameraUnits - offsetY);

        gameObject.GetComponent<RectTransform>().anchorMin = safeAreaMin;
        gameObject.GetComponent<RectTransform>().anchorMax = safeAreaMax;

    }
}
