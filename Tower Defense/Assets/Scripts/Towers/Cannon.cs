using Towers;

public class Cannon : Tower
{
    protected override string GetEnemyTag() => "Enemy";
    protected override string GetAttackSound() => "Cannon_Shot";
}