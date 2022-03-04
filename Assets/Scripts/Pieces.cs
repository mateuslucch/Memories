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
            figuresChildTransform = new Transform[] { backgroundSquare.GetComponent<Transform>(), figureImageObject.GetComponent<Transform>() };

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            parentOriginalScale = transform.localScale;
            childOriginalScale = figuresChildTransform[0].transform.localScale;

            print(childOriginalScale);
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
            
            /* CHANGE ENTIRE PIECE SCALE
            if (transform.localScale.x <= originalScale.x && transform.localScale.x >= -originalScale.x)
            {
                transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * rotateSpeed, transform.localScale.y, transform.localScale.z);
                if (transform.localScale.x < 0f) { backgroundSquare.GetComponent<SpriteRenderer>().sortingOrder = 2; }
            }
            else
            {
                state = State.Standing;
                transform.localScale = new Vector3(originalScale.x, transform.localScale.y, transform.localScale.z);
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            */

        }

        private void ShowingAnimation()
        {
            /* if (transform.localScale.x <= originalScale.x && transform.localScale.x >= -originalScale.x)
            {
                transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * rotateSpeed, transform.localScale.y, transform.localScale.z);
                if (transform.localScale.x < 0f) { backgroundSquare.GetComponent<SpriteRenderer>().sortingOrder = 0; }
            }
            else
            {
                state = State.Standing;
                transform.localScale = new Vector3(originalScale.x, transform.localScale.y, transform.localScale.z);
                gameSession.CompareObjects();
            } */
            foreach (Transform pieceObjects in figuresChildTransform)
            {
                if (pieceObjects.localScale.x <= childOriginalScale.x && pieceObjects.localScale.x >= -childOriginalScale.x)
                {
                    pieceObjects.localScale = new Vector3(pieceObjects.localScale.x - Time.deltaTime * rotateSpeed, pieceObjects.localScale.y, pieceObjects.localScale.z);
                    if (pieceObjects.localScale.x < 0f) { backgroundSquare.GetComponent<SpriteRenderer>().sortingOrder = 0; }
                }
                else
                {
                    state = State.Standing;
                    pieceObjects.localScale = new Vector3(childOriginalScale.x, pieceObjects.localScale.y, pieceObjects.localScale.z);
                    gameSession.CompareObjects();
                }
            }
        }
    }
}