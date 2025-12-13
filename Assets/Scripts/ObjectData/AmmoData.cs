[System.Serializable]
public class LargeCaliberData : AmmoData
{
    public int Damage;
}

[System.Serializable]
public class RedLaserData : AmmoData
{
    public float Length;
}

[System.Serializable]
public abstract class AmmoData
{
    public float Mass;
    public float Friction;
    public float MoveForce;
    public float LiveTime;
}