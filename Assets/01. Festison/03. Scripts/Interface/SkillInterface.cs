public interface IMoveable
{
    public float Speed { get; set; }
    public void Move();
}

public interface ISkillEffect
{
    public void ActivateSkillEffect();
}
