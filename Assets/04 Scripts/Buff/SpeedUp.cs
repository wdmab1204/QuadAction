
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
            user.Speed.Value = user.Speed.Value + 5.0f;
        }

        public override void Update()
        {

        }

        public override void Upgrade()
        { 
            user.Speed.Value = user.Speed.Value + 2.5f;
        }
    }
}
