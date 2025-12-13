using UnityEngine;

public sealed class BigAsteroidDieSignal : EnemyDieSignal
{
    private int _value;
    public Vector3 Position;

    public int Value
    {
        get { return _value; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Value cannot be less than zero");
                return;
            }
            
            _value = value;
        }
    }

    public BigAsteroidDieSignal(int points, Vector3 position)
    {
        Position = position;
        Value = points;
    }
}

public sealed class BigAsteroidCompletelyDieSignal : EnemyDieSignal
{
    private int _value;

    public int Value
    {
        get { return _value; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Value cannot be less than zero");
                return;
            }
            
            _value = value;
        }
    }

    public BigAsteroidCompletelyDieSignal(int points)
    {
        Value = points;
    }
}

public class SmallAsteroidDieSignal : EnemyDieSignal
{
    private int _value;

    public int Value
    {
        get { return _value; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Value cannot be less than zero");
                return;
            }
            
            _value = value;
        }
    }

    public SmallAsteroidDieSignal(int points)
    {
        Value = points;
    }
}

public class AlienShipDieSignal : EnemyDieSignal
{
    private int _value;

    public int Value
    {
        get { return _value; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("Value cannot be less than zero");
                return;
            }
            
            _value = value;
        }
    }
    
    public AlienShipDieSignal(int points)
    {
        Value = points;
    }
}

public interface EnemyDieSignal
{
    int Value { get; }
}

public class TogglePauseSignal
{
    
}

public class ToggleLockGameInputSignal
{
    public bool IsLocked;

    public ToggleLockGameInputSignal(bool isLocked)
    {
        IsLocked = isLocked;
    }
}

public class PlayerDieSignal
{
    
}