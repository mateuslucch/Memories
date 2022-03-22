using UnityEngine;

public class ScreensReference : MonoBehaviour
{
    [SerializeField] GameObject boardObjectReference = null;

    Vector2
    cameraSizePixels,
    cameraSizeUnits,
    safeAreaPixels,
    safeAreaUnits,
    boardSizePixels,
    boardSizeUnits,
    boardPosition;

    void Awake()
    {
        Camera mainCamera = Camera.main;

        // screen size in pixels
        cameraSizePixels = new Vector2(
            mainCamera.pixelWidth,
            mainCamera.pixelHeight);

        // calculate screen size in units
        cameraSizeUnits = new Vector2(
            mainCamera.pixelWidth * mainCamera.orthographicSize * 2 / mainCamera.pixelHeight,
            mainCamera.orthographicSize * 2
        );

        safeAreaPixels = new Vector2(
            Screen.safeArea.width,
            Screen.safeArea.height);

        safeAreaUnits = new Vector2(
            safeAreaPixels.x * cameraSizeUnits.x / mainCamera.pixelWidth,
            safeAreaPixels.y * cameraSizeUnits.y / mainCamera.pixelHeight);

        if (BoardReference())
        {
            StoreBoardValues(mainCamera);
        }
        else
        {
            print("<color=green>There is no board as a reference in this object, using safe area as reference.</color>");
            boardSizePixels = new Vector2(
                safeAreaPixels.x,
                safeAreaPixels.y
                );

            boardSizeUnits = new Vector2(
                safeAreaUnits.x,
                safeAreaUnits.y
                );

            boardPosition = new Vector2(
                Screen.safeArea.position.x,
                Screen.safeArea.position.y
                );
        }
    }

    public void StoreBoardValues(Camera mainCamera)
    {
        // calculate board size pixels
        boardSizePixels = new Vector2(
            boardObjectReference.GetComponent<RectTransform>().rect.width,
            boardObjectReference.GetComponent<RectTransform>().rect.height
            );

        boardSizeUnits = new Vector2(
            boardSizePixels.x * cameraSizeUnits.x / mainCamera.pixelWidth,
            boardSizePixels.y * cameraSizeUnits.y / mainCamera.pixelHeight
            );

        boardPosition = new Vector2(
            boardObjectReference.GetComponent<RectTransform>().position.x,
            boardObjectReference.GetComponent<RectTransform>().position.y
            );
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

    public Vector2 getBoardPosition()
    {
        return boardPosition;
    }

    public bool BoardReference()
    {
        if (boardObjectReference != null)
        {
            return true;
        }
        return false;
    }
}
