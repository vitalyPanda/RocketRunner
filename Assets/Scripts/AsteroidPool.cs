using UnityEngine;

public static class AsteroidPool
{
    public static GameObject GetAsteroid()
    {
        //TODO: Make pool
        return Object.Instantiate(Resources.Load<GameObject>("Asteroid"));
    }
}
