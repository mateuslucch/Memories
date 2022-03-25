using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GlowControl))]
public class Pieces : MonoBehaviour
{
    [SerializeField] GameObject backgroundSquare;
    [SerializeField] GameObject figureImageObject;
    [SerializeField] GameObject questionMark;
    [SerializeField] float rotateSpeed = 100f;

    Transform[] figuresChildTransform;
    GlowControl glowControl;

    int rotateSpin;

    GameSession gameSession;

    SpriteRenderer spriteRenderer;

    enum State { Standing, Rotating }
    State state = State.Standing;
    float objectRotateAngle = 0;
    bool isFirstClick = false;

    Vector3 parentOriginalScale;
    Vector3 childOriginalScale;
    [SerializeField] Animator pieceAnimation;

    private void Start()
    {
        float timeBeforeHide = TimeToHide.timeBeforeHide;
        glowControl = GetComponent<GlowControl>();
        gameSession = FindObjectOfType<GameSession>();

        figureImageObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        questionMark.SetActive(false);
        figuresChildTransform = new Transform[] {
                backgroundSquare.GetComponent<Transform>(),
                figureImageObject.GetComponent<Transform>()
                };

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        parentOriginalScale = transform.localScale;
        childOriginalScale = figuresChildTransform[0].transform.localScale;

        StartCoroutine(HideImagesCountdown(timeBeforeHide));

    }

    private void FixedUpdate()
    {
        if (state == State.Rotating)
        {
            // Layer is 1 to show, -1 to hide image
            if (rotateSpin == -1) { ShowImage(true, 1); }
            else { ShowImage(false, -1); }
        }
    }

    private void OnMouseDown()
    {
        if (!MenuOpenStatic.menuOpen)
        {
            gameSession.AddObjectToArray(gameObject);
            isFirstClick = true;
        }
    }

    public void RevealImage()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rotateSpin = -1;
        state = State.Rotating;
    }

    // hide images, when starting game or when they not match
    public IEnumerator HideImagesCountdown(float timeUntilHide)
    {
        yield return new WaitForSecondsRealtime(timeUntilHide);
        rotateSpin = 1;
        state = State.Rotating;
    }

    public void RemoveQuestionMark()
    {
        questionMark.SetActive(false);
    }

    private void ShowImage(bool showImage, int layerChange)
    {
        foreach (Transform pieceChild in figuresChildTransform)
        {
            if (pieceChild.localScale.x <= childOriginalScale.x && pieceChild.localScale.x >= -childOriginalScale.x)
            {
                RotateObject(pieceChild, layerChange);
            }
            else
            {
                state = State.Standing;
                pieceChild.localScale = new Vector3(childOriginalScale.x, pieceChild.localScale.y, pieceChild.localScale.z);

                if (showImage)
                {
                    gameSession.CompareObjects();
                }
                else
                {
                    // reveal question mark in the first hide
                    if (!isFirstClick) { questionMark.SetActive(true); }
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }

    private void RotateObject(Transform pieceChild, int sortingOrder)
    {
        pieceChild.localScale = new Vector3(pieceChild.localScale.x - Time.unscaledDeltaTime * rotateSpeed, pieceChild.localScale.y, pieceChild.localScale.z);
        if (pieceChild.localScale.x <= 0f)
        {
            figureImageObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        }
    }

    public void StartAnimation()
    {
        pieceAnimation.SetTrigger("EnterAnimation");
    }

    // DistributingPieces access this method
    public void ChangeImage(Sprite animalSprite)
    {
        spriteRenderer = figureImageObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = animalSprite;
    }
}