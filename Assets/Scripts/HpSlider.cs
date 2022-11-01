using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpSlider : MonoBehaviour
{
    private Slider slider;

    public void UpdateValue()
    {
        int currentHp = Player.Instance.GetHp();
        slider.DOValue(currentHp, 3.0f);
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = Player.Instance.GetHp();
        UpdateValue();
    }
}