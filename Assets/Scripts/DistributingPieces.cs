using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{
    public class DistributingPieces : MonoBehaviour
    {
        [SerializeField] GameObject piece; //piece prefab
        [SerializeField] Sprite[] piecesSprites; //all possible images

        [SerializeField] GameObject piecesGroup; //parent to store the pieces, so the hierarchy doesnt look a mess

        [SerializeField] int numberRows = 4;
        [SerializeField] int numberLines = 4;
        [SerializeField] float piecesOffset = 0.1f;

        List<Sprite> spritesListTemp;
        List<Vector2> tempCoordinateList;
        [SerializeField] List<Vector2> pieceCoordinateListTest;

        private void Start()
        {
            tempCoordinateList = new List<Vector2>();
            //MountSpritesIndex();
            //MountBoard();            
        }

        public void RestartGame(int numberLines, int numberRows)
        {
            this.numberLines = numberLines;
            this.numberRows = numberRows;
            CreateSpritesIndex();
            CoordinatesList();
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

        private void CoordinatesList()
        {

            Vector2 pieceCoordinate = new Vector2();
            float pieceSize = piece.GetComponent<BoxCollider2D>().size.x;

            for (int i = 0; i < numberRows; i++)
            {
                for (int j = 0; j < numberLines; j++)
                {
                    print(pieceSize);
                    float scaleFactor = ChangePieceScale();
                    pieceCoordinate = new Vector2(
                        (i * pieceSize - ((numberRows - pieceSize) / 2)) * scaleFactor,
                        (j * pieceSize - ((numberLines - pieceSize) / 2)) * scaleFactor
                        );
                    tempCoordinateList.Add(pieceCoordinate);
                    pieceCoordinateListTest.Add(pieceCoordinate);
                }
            }
        }

        //mount the board with a temporary sprite list
        private async void MountBoard()
        {
            Vector2 worldCoordinatesOffset;
            worldCoordinatesOffset.y = numberLines / 2 - .5f;
            worldCoordinatesOffset.x = numberRows / 2 - .5f;

            //distribute pieces on screen
            for (var i = 0; i < numberRows; i++)
            {
                for (var j = 0; j < numberLines; j++)
                {
                    int spriteIconIndex = Random.Range(0, spritesListTemp.Count);

                    //Vector2 piecePosition = new Vector2(i - worldCoordinatesOffset.x, j - worldCoordinatesOffset.y);

                    int index = Random.Range(0, tempCoordinateList.Count); //sort random index inside coordinates list
                    Vector2 piecePosition = new Vector2(tempCoordinateList[index].x, tempCoordinateList[index].y);
                    tempCoordinateList.RemoveAt(index); // remove the used coordinate

                    GameObject newPiece = Instantiate(piece, piecePosition, transform.rotation);
                    newPiece.transform.localScale = new Vector3(ChangePieceScale() - piecesOffset, ChangePieceScale() - piecesOffset, 1f);

                    newPiece.GetComponent<Pieces>().ChangeImage(spritesListTemp[spriteIconIndex]); //add sprite to piece from piecesTempSprite
                    spritesListTemp.RemoveAt(spriteIconIndex); //remove the added piece from temp

                    newPiece.transform.SetParent(piecesGroup.transform); //add piece to a parent(organize in hierarchy)
                }
            }
        }

        public int NumberOfPieces() { return (numberLines * numberRows); }

        private float ChangePieceScale()
        {
            float pieceScale = 0;

            Camera mainCamera = Camera.main;

            // calculate screen size in units
            float heightCameraUnits = mainCamera.orthographicSize * 2;
            float widthCameraUnits = mainCamera.pixelWidth * heightCameraUnits / mainCamera.pixelHeight; //mainCamera.pixel* change with device

            float sizePiece = (float)numberRows / widthCameraUnits;
            float realPieceSize = piece.GetComponent<BoxCollider2D>().size.x;
            pieceScale = realPieceSize / sizePiece;
            return pieceScale;
            //return 1;
        }
    }

}