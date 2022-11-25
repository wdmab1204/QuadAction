
namespace ItemNameSpace
{
    public class SpeedUp : Item
    {
        public SpeedUp(ItemInfo iteminfo) : base(iteminfo)
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

        public override void Upgrade()
        {
            user.SetSpeed(user.GetSpeed() + 5.0f);
        }
    }
}
