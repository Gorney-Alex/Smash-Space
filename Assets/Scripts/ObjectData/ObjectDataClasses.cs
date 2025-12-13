[System.Serializable]
public class LimitsData
{
    public float MinSpeed;
    public float MaxSpeed;
    public float BounceForce;
}

[System.Serializable]
public class PlayerData
{
    public float Mass;
    public float Friction;
    public float MoveForce;
    public float RotateForce;
    public int StartHealth;
    public int StartPoints;
    public float InvulnerabilitySeconds;
    public float BlinkSpeed;
    public float AlphaMin;
    public float AlphaMax;
}

[System.Serializable]
public class BigAsteroidData : EnemyData
{

}

[System.Serializable]
public class SmallAsteroidData : EnemyData
{

}

[System.Serializable]
public class AlienShipData : EnemyData
{

}


[System.Serializable]
public abstract class EnemyData
{
    public float Mass;
    public float Friction;
    public float MoveForce;
    public float RotateForce;
    public int StartHealth;
    public int Damage;
    public int Points;
}


