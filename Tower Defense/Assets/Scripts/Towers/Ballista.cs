using Towers;

public class Ballista : Tower
{
    protected override string GetEnemyTag() => "FlyingEnemy";
    protected override string GetAttackSound() => "Balista_Shot";
}