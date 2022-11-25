using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpSlider : MonoBehaviour
{
    public Character character;
    private Slider slider;

    public void UpdateValue()
    {
        int currentHp = character.GetHp();
        slider.DOValue(currentHp, 3.0f);
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = character.GetHp();
        //UpdateValue();
    }
}