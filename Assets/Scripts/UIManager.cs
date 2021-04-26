using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Declaration of Variables
    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI highScoreText;
    private int highScore;

    public TextMeshProUGUI waveText;
    private int wave;

    public Image[] lifeSprites;
    public Image healthBar;

    public Sprite[] healthBars;

    private static UIManager instance;

    private Color32 active = new Color(1, 1, 1, 1);
    private Color32 inactive = new Color(1, 1, 1, 0.25f);

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //changes the colour of the sprites when life lost
    public static void UpdateLives(int l)
    {
        foreach (Image i in instance.lifeSprites)
            i.color = instance.inactive;

        for (int i = 0; i < l; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }
    }

    //changes health bar sprite

    public static void UpdateHealthBar(int h)
    {
        Debug.Log(h);
        instance.healthBar.sprite = instance.healthBars[h];
    }

    //updates score
    public static void UpdateScore(int s)
    {
        instance.score += s;
        instance.scoreText.text = instance.score.ToString("000,000");
    }

    //resets everything when game started
    public static void ResetUI()
    {
        instance.score = 0;
        instance.wave = 0;
        instance.scoreText.text = instance.score.ToString("000,000");
        instance.waveText.text = instance.wave.ToString();
    }

    //changes wave number
    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString();
    }

}
