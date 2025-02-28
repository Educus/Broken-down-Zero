public interface IHitable   // 피해를 입는 모든 대상
{
    public void IDamage(float damage);
}

public interface IInteraction   // 상호작용 가능한 모든 대상
{
    public void Interact();
}