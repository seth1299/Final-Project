public class HealthSystem
{
    private int Health;
    private int HealthMax;
    
    public HealthSystem(int Health)
    {
        this.Health = Health;
    }

    public int GetHealth()
    {
        return Health;
    }

    public float GetHealthPercent()
    {
        return (float) Health / HealthMax;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0) Health = 0;
    }
}
