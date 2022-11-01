
namespace ItemNameSpace
{
    public class SpeedUp : Item
    {
        public SpeedUp(Item item)
        {
            this.name = item.name;
            this.icon = item.icon;
            this.itemType = item.itemType;
        }
        public override void Use(Character user)
        {
            user.SetSpeed(user.GetSpeed() + 5.0f);

            //이펙트나 효과음 추가
        }
    }
}
