
namespace BuffNameSpace
{
    public class SpeedUp : Buff
    {
        public SpeedUp(BuffScriptableObject itemScriptableObject) : base(itemScriptableObject)
        {

        }

        public override void Exit()
        {

        }

        public override void Start()
        {
            user.SetSpeed(user.GetSpeed() + 5.0f);
        }

        public override void Update()
        {

        }

    }
}
