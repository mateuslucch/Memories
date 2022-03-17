using UnityEngine;

public class ScreensReference : MonoBehaviour
{
    [SerializeField] GameObject boardObject = null;

    Vector2 cameraSizePixels, cameraSizeUnits, safeAreaPixels, safeAreaUnits, boardSizePixels, boardSizeUnits;

    void Start()
    {
        Camera mainCamera = Camera.main;

        // screen size in pixels
        cameraSizePixels = new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight);

        // calculate screen size in units
        cameraSizeUnits = new Vector2(
            mainCamera.pixelWidth * mainCamera.orthographicSize * 2 / mainCamera.pixelHeight,
            mainCamera.orthographicSize * 2
        );



        if (BoardReference())
        {
            // calculate board size pixels
            boardSizePixels = new Vector2(
                boardObject.GetComponent<RectTransform>().rect.width,
                boardObject.GetComponent<RectTransform>().rect.height
                );

            boardSizeUnits = new Vector2(
                boardSizePixels.x * cameraSizeUnits.x / mainCamera.pixelWidth,
                boardSizePixels.y * cameraSizeUnits.y / mainCamera.pixelHeight
                );
        }
    }

    public Vector2 getCameraPixels()
    {
        return cameraSizePixels;
    }
    public Vector2 getCameraUnits()
    {
        return cameraSizeUnits;
    }

    public Vector2 getSafeAreaPixels()
    {
        return safeAreaPixels;
    }
    public Vector2 getSafeAreaUnits()
    {
        return safeAreaUnits;
    }

    public Vector2 getBoardPixels()
    {
        return boardSizePixels;
    }
    public Vector2 getBoardUnits()
    {
        return boardSizeUnits;
    }

    public bool BoardReference()
    {
        if (boardObject != null)
        {
            return true;
        }
        else { print($@"There is no board as a reference in this scene, 
                    using safe area"); }
        return false;
    }
}
