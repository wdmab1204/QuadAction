using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpSlider : MonoBehaviour
{
    public Character character;
    private Slider slider;

    public void UpdateValue(int value)
    {
        int currentHp = value;
        slider.DOValue(currentHp, 3.0f);
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Initialize()
    {
        slider.maxValue = character.MaxHp.Value;
        character.Hp.OnChange += hp => UpdateValue(hp);
        print(character.MaxHp.Value);
        UpdateValue(character.MaxHp.Value);
    }
}