[System.Serializable]
public class SpawnerBigAsteroidData
{
    public float SecondsBetweenSpawner;
    public int StartAmount;
    public int MaxAmount;
    public float MinSpawnRadius;
    public float MaxSpawnRadius;
}

[System.Serializable]
public class SpawnerSmallAsteroidData
{
    public float SecondsBetweenSpawner;
    public int StartAmount;
    public int MaxAmount;
    public float MinSpawnRadius;
    public float MaxSpawnRadius;
}

[System.Serializable]
public class SpawnerAlienShipData
{
    public float SecondsBetweenSpawner;
    public int StartAmount;
    public int MaxAmount;
    public float MinSpawnRadius;
    public float MaxSpawnRadius;
}

[System.Serializable]
public class SpawnerLargeCaliberData
{
    public int MaxAmount;
}

[System.Serializable]
public class SpawnerRedLaserData
{
    public int MaxAmount;
}

[System.Serializable]
public class BigAsteroidDeathHandlerData
{
    public int MinAmountSmallAsteroids;
    public int MaxAmountSmallAsteroids;
}


