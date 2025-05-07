using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int progressAmount = 0;
    public Slider progressSlider;

    public static ScoreManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        progressSlider.value = 0;
        Gem.OnGemCollect += IncreaseProgressAmount;
    }

    public void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (progressAmount >= 100)
        {
            Debug.Log("level complete");
        }
    }
    public void DecreaseProgressAmount(int amount)
    {
        progressAmount -= amount;
        progressSlider.value = progressAmount;

    }
}
