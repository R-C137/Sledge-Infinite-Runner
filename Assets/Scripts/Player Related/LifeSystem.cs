using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public int lives = 3;

    public List<GameObject> hearts = new();

    public bool canLoseLife = true;

    public void Start()
    {
        for (int i = 0; i < lives; i++)
        {
            hearts[i].SetActive(true);
        }

        Utility.instance.onRunEnded += () => canLoseLife = false;
    }

    public void AddLife()
    {
        if(lives == 3)
            return;

        int index = lives;

        hearts[index].SetActive(true);

        if (LeanTween.isTweening(hearts[index]))
            LeanTween.cancel(hearts[index]);

        LeanTween.size(hearts[index].transform as RectTransform, new Vector2(150, 150), .2f).setOnComplete(() =>
        {
            LeanTween.size(hearts[index].transform as RectTransform, new Vector2(100, 100), .3f);
        });

        lives++;
    }

    public void RemoveLife()
    {
        if(!canLoseLife)
            return;

        int index = lives - 1;

        if (LeanTween.isTweening(hearts[index]))
            LeanTween.cancel(hearts[index]);

        LeanTween.size(hearts[index].transform as RectTransform, new Vector2(150, 150), .2f).setOnComplete(() =>
        {
            LeanTween.size(hearts[index].transform as RectTransform, new Vector2(5, 5), .3f)
                .setOnComplete(() => hearts[index].SetActive(false));
        });

        if (lives == 1)
        {
            Utility.instance.SetGameOver();
            return;
        }

        lives--;
    }
}