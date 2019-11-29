using Math = System.Math;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshPro damageText;
    public TextMeshProUGUI healthText;
    public Slider playerHealthSlider;
    public Image healthImage;
    public Color maxHealthColor;
    public Color minHealthColor;
    private static PlayerUI instance;

    private readonly Vector3 textSpeed = new Vector3(0, 80, 0);
    private readonly float textInstancePositionRange = 0.5f;
    public static PlayerUI Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    public void UpdateScore()
    {
        scoreText.text = string.Format($"Score: {GameManager.instance.score}");
    }
    public void ShowDamage(Transform instantiatePosition, float damage)
    {
        float random = Random.Range(-textInstancePositionRange, textInstancePositionRange);
        Vector3 randomPosition = instantiatePosition.position + new Vector3(random, random, 0);
        TextMeshPro damageTextInstance = Instantiate(damageText, randomPosition, Quaternion.identity);
        damageTextInstance.GetComponent<Rigidbody2D>().AddForce(textSpeed);
        damageTextInstance.text = string.Format($"-{Math.Round(damage,2)}");
        Destroy(damageTextInstance.gameObject, 0.35f);
    }
    public void UpdateHealth(float maxHP,float currentHP)
    {
        playerHealthSlider.value = currentHP / maxHP;
        healthImage.color = Color.Lerp(minHealthColor, maxHealthColor,currentHP / maxHP);
        healthText.text = $"{Math.Round(currentHP,2)} / {Math.Round(maxHP,2)}";
    }
}
