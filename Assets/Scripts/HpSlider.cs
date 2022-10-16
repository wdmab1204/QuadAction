using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    [SerializeField] private Player player;
    private Slider slider;

    public void SetValue(int hp)
    {
        slider.value = hp;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = player.GetHp();
        slider.value = player.GetHp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
