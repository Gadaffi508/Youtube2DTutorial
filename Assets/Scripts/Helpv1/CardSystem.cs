using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public List<GameObject> cardsHomeWork = new List<GameObject>();
    public List<GameObject> cardsGamePlay = new List<GameObject>();

    [Header("Transform")] [Space] public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    
    [Header("Score")]
    [Space]
    public int _score;
    private int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    
    int random;

    private void Start()
    {
        ListActiveFalse(cardsGamePlay);
        ListActiveFalse(cardsHomeWork);

        cardsGamePlay[GetRandom()].SetActive(true);
        cardsHomeWork[GetRandom()].SetActive(true);

        random = Random.Range(0, 3);

        Debug.Log(random);
        Debug.Log(GetRandom());

        if (random == GetRandom())
        {
            random = Random.Range(0, cardsGamePlay.Count);
        }

        cardsGamePlay[random].SetActive(true);
        cardsHomeWork[random].SetActive(true);

        SaveRandom();

        Debug.Log(random);
        Debug.Log(GetRandom());

        Score++;
        Debug.Log(Score);
    }

    public void ListActiveFalse(List<GameObject> cards)
    {
        foreach (var card in cards)
        {
            card.SetActive(false);
        }
    }

    public void SaveRandom()
    {
        PlayerPrefs.SetInt("random", random);
    }

    public int GetRandom()
    {
        if (PlayerPrefs.HasKey("random"))
        {
            random = PlayerPrefs.GetInt("random");
            return random;
        }
        return 1;
    }
}
