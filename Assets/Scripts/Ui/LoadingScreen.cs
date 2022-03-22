using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] List<GameObject> animalsObjects;
    [SerializeField] List<Sprite> animalSprites;
    List<Sprite> tempAnimalSprites;

    bool loadingScreenLoad = false;

    void Start()
    {
        //tempAnimalSprites = new List<Sprite>();
    }

    private void OnEnable()
    {
        tempAnimalSprites = new List<Sprite>();
        foreach (Sprite animalSprite in animalSprites)
        {
            tempAnimalSprites.Add(animalSprite);
        }

        loadingScreenLoad = true;
        int index = 0;

        foreach (GameObject animal in animalsObjects)
        {
            index = Random.Range(0, tempAnimalSprites.Count);
            animal.GetComponent<SpriteRenderer>().sprite = tempAnimalSprites[index];
            tempAnimalSprites.RemoveAt(index);
        }
    }

    void Update()
    {
        if (loadingScreenLoad)
        {
            // maybe animate something like the text
        }
    }
}
