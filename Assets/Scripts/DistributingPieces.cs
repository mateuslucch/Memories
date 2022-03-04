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

        List<Sprite> spritesListTemp;

        private void Start()
        {
            //MountSpritesIndex();
            //MountBoard();
        }

        public void RestartGame(int numberLines, int numberRows)
        {
            this.numberLines = numberLines;
            this.numberRows = numberRows;
            CreateSpritesIndex();
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

        }

        //mount the board with a temporary sprite list
        private void MountBoard()
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

                    Vector2 piecePosition = new Vector2(i - worldCoordinatesOffset.x, j - worldCoordinatesOffset.y);
                    GameObject newPiece = Instantiate(piece, piecePosition, transform.rotation);
                    newPiece.transform.localScale = new Vector3(0.8f, 0.8f, 0f);

                    newPiece.GetComponent<Pieces>().ChangeImage(spritesListTemp[spriteIconIndex]); //add sprite to piece from piecesTempSprite
                    spritesListTemp.RemoveAt(spriteIconIndex); //remove the added piece from temp

                    newPiece.transform.SetParent(piecesGroup.transform); //add piece to a parent(organize in hierarchy)
                }
            }
        }

        public int NumberOfPieces() { return (numberLines * numberRows); }
    }
}