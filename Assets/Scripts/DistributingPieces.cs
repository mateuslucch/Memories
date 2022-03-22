using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScreensReference))]
public class DistributingPieces : MonoBehaviour
{
    [SerializeField] GameObject piecePrefab; //piece prefab
    [SerializeField] Sprite[] piecesSprites; //all possible images

    [SerializeField] GameObject piecesGroup; //parent to store the pieces, so the hierarchy doesnt look a mess

    [SerializeField] int numberRows = 4;
    [SerializeField] int numberLines = 4;
    [SerializeField] float piecesOffset = 0.1f;
    [SerializeField] GameObject boardArea;

    List<Sprite> spritesListTemp;
    List<Vector2> tempCoordinateList;
    ScreensReference screensSizes;

    [SerializeField] List<Vector2> pieceCoordinateListTest;

    private void Start()
    {
        tempCoordinateList = new List<Vector2>();
        screensSizes = GetComponent<ScreensReference>();      
    }

    public void StartMountBoard(int numberLines, int numberRows)
    {
        this.numberLines = numberLines;
        this.numberRows = numberRows;

        if ((numberLines * numberRows) > piecesSprites.Length * 2)
        {
            print("There is no sprites enough for this table");
            return;
        }

        CreateSpritesIndex();
        ListOfCoordinates();
        MountBoard();
    }

    private void CreateSpritesIndex()
    {

        //mount a temporary (small) list of sprites
        spritesListTemp = new List<Sprite>();
        for (var i = 0; i < numberLines * numberRows / 2; i++)
        {
            bool imageChoosen = false;
            while (imageChoosen == false)
            {
                int randomImageIndex = Random.Range(0, piecesSprites.Length);

                //mount a list of random sprites to be used in game, with 2 of each other
                if (!spritesListTemp.Contains(piecesSprites[randomImageIndex]))
                {
                    spritesListTemp.Add(piecesSprites[randomImageIndex]);
                    spritesListTemp.Add(piecesSprites[randomImageIndex]);
                    imageChoosen = true;
                }
            }
        }
    }

    private void ListOfCoordinates()
    {
        Vector2 pieceCoordinate = new Vector2();
        float pieceSize = piecePrefab.GetComponent<BoxCollider2D>().size.x;
        float scaleFactor = ChangePieceScale();

        for (int i = 0; i < numberRows; i++)
        {
            for (int j = 0; j < numberLines; j++)
            {
                pieceCoordinate = new Vector2(
                    (i * pieceSize - ((numberRows - pieceSize) / 2)) * scaleFactor,
                    (j * pieceSize - ((numberLines - pieceSize) / 2)) * scaleFactor
                    );

                // sums the position of board reference to centralize pieces
                pieceCoordinate += new Vector2(
                    boardArea.GetComponent<RectTransform>().position.x,
                    boardArea.GetComponent<RectTransform>().position.y);
                tempCoordinateList.Add(pieceCoordinate);
                pieceCoordinateListTest.Add(pieceCoordinate);
            }
        }
    }

    //mount the board with a temporary sprite list
    private void MountBoard()
    {
        float pieceScale = ChangePieceScale();

        //distribute pieces on screen
        for (var i = 0; i < numberRows; i++)
        {
            for (var j = 0; j < numberLines; j++)
            {
                int spriteIconIndex = Random.Range(0, spritesListTemp.Count);

                //sort random index inside coordinates list
                int index = Random.Range(0, tempCoordinateList.Count);
                Vector2 piecePosition = new Vector2(tempCoordinateList[index].x, tempCoordinateList[index].y);
                // remove the used coordinate
                tempCoordinateList.RemoveAt(index);

                GameObject newPiece = Instantiate(piecePrefab, piecePosition, transform.rotation);
                newPiece.transform.localScale = new Vector3(pieceScale - piecesOffset, pieceScale - piecesOffset, 1f);

                //add sprite to piece from piecesTempSprite
                newPiece.GetComponent<Pieces>().ChangeImage(spritesListTemp[spriteIconIndex]);
                //remove the added sprite from sprite list (not repeat)
                spritesListTemp.RemoveAt(spriteIconIndex);
                //add piece to a parent(organize in hierarchy)
                newPiece.transform.SetParent(piecesGroup.transform);

            }
        }
    }

    public int NumberPieces() { return (numberLines * numberRows); }

    private float ChangePieceScale()
    {
        float pieceScale = 0;
        float newSizePiece;

        float boardSizeHeightUnits = screensSizes.getBoardUnits().y;
        float boardSizeWidthUnits = screensSizes.getBoardUnits().x;

        float gridRazao = (float)numberLines / (float)numberRows;
        float screenRazao = screensSizes.getBoardUnits().y / screensSizes.getBoardUnits().x;

        // test to see if all pieces together will be bigger than board size, then change newPieceSize again if it is
        if (gridRazao < screenRazao || gridRazao / screenRazao == 1)
        {
            newSizePiece = (float)numberRows / boardSizeWidthUnits;
        }
        else
        {
            newSizePiece = (float)numberLines / boardSizeHeightUnits;

        }

        // size piece (1 unit usually, from collider (not good i think))
        float realPieceSize = piecePrefab.GetComponent<BoxCollider2D>().size.x;

        pieceScale = realPieceSize / newSizePiece;
        return pieceScale;
    }
}