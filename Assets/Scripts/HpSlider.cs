using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    private Slider slider;

    public void UpdateValue()
    {
        int currentHp = Player.Instance.GetHp();
        slider.value = currentHp;
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = Player.Instance.GetHp();
        slider.value = Player.Instance.GetHp();
    }
}