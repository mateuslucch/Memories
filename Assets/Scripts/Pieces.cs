using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{
    public class Pieces : MonoBehaviour
    {
        [SerializeField] GameObject backgroundSquare;
        [SerializeField] GameObject figureImageObject;
        [SerializeField] float rotateSpeed = 100f;

        Transform[] figuresChildTransform;

        float rotateSpin;

        GameSession gameSession;

        SpriteRenderer spriteRenderer;

        enum State { Standing, Rotating }
        State state = State.Standing;
        float objectRotateAngle = 0;

        Vector3 parentOriginalScale;
        Vector3 childOriginalScale;

        private void Start()
        {
            figuresChildTransform = new Transform[] {
                backgroundSquare.GetComponent<Transform>(),
                figureImageObject.GetComponent<Transform>()
                };

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            parentOriginalScale = transform.localScale;
            childOriginalScale = figuresChildTransform[0].transform.localScale;

            StartCoroutine(HideImagesCountdown(3f));
            gameSession = FindObjectOfType<GameSession>();
        }

        private void Update()
        {
            if (state == State.Rotating)
            {
                if (rotateSpin == -1) { ShowingAnimation(); }
                else { ChangeScale(); }
            }
        }

        public void ChangeImage(Sprite animalSprite)
        {
            spriteRenderer = figureImageObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = animalSprite;
        }

        private void OnMouseDown()
        {
            gameSession.AddObjectToArray(gameObject);
        }

        public void RevealImage()
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rotateSpin = -1;
            state = State.Rotating;
        }

        // hide images, when start game or when not matched
        public IEnumerator HideImagesCountdown(float timeUntilHide)
        {
            yield return new WaitForSecondsRealtime(timeUntilHide);
            rotateSpin = 1;
            state = State.Rotating;
        }

        private void ChangeScale()
        {
            foreach (Transform pieceChild in figuresChildTransform)
            {
                if (pieceChild.localScale.x <= childOriginalScale.x && pieceChild.localScale.x >= -childOriginalScale.x)
                {
                    pieceChild.localScale = new Vector3(pieceChild.localScale.x - Time.deltaTime * rotateSpeed, pieceChild.localScale.y, pieceChild.localScale.z);
                    if (pieceChild.localScale.x < 0f) { backgroundSquare.GetComponent<SpriteRenderer>().sortingOrder = 2; }
                }
                else
                {
                    state = State.Standing;
                    pieceChild.localScale = new Vector3(childOriginalScale.x, pieceChild.localScale.y, pieceChild.localScale.z);
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        // show piece
        private void ShowingAnimation()
        {
            foreach (Transform pieceChild in figuresChildTransform)
            {
                if (pieceChild.localScale.x <= childOriginalScale.x && pieceChild.localScale.x >= -childOriginalScale.x)
                {
                    pieceChild.localScale = new Vector3(pieceChild.localScale.x - Time.deltaTime * rotateSpeed, pieceChild.localScale.y, pieceChild.localScale.z);
                    if (pieceChild.localScale.x < 0f) { backgroundSquare.GetComponent<SpriteRenderer>().sortingOrder = 0; }
                }
                else
                {
                    state = State.Standing;
                    pieceChild.localScale = new Vector3(childOriginalScale.x, pieceChild.localScale.y, pieceChild.localScale.z);
                    gameSession.CompareObjects();
                }
            }
        }
    }
}