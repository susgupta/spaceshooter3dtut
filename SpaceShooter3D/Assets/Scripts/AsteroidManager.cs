using UnityEngine;
using System.Collections;

public class AsteroidManager : MonoBehaviour {

    //grid management
    [SerializeField]
    int numberOfAsteroidsOnAnAxis = 10;

    [SerializeField]
    int gridSpacing = 100;

    //prefab reference explicit for asteroid
    [SerializeField]
    Asteroid asteroid;

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += PlaceAsteroids;
    }

    void OnDisable()
    {
        //remove delegate to un0subscribe
        EventManager.onStartGame -= PlaceAsteroids;
    }

    //void Start()
    //{
    //    PlaceAsteroids();
    //}

	//place asteroids
    void PlaceAsteroids()
    {
        //first along x-axis
        for (int x = 0; x < numberOfAsteroidsOnAnAxis; x++)
        {
            //then along y-axis
            for (int y = 0; y < numberOfAsteroidsOnAnAxis; y++)
            {
                //then along z-axis
                for (int z = 0; z < numberOfAsteroidsOnAnAxis; z++)
                {
                    InstantiateAsteroid(x, y, z);
                }
            }                
        }
    }

    //create asteroid
    void InstantiateAsteroid(int x, int y, int z)
    {
        Instantiate(asteroid, 
                    new Vector3(
                        transform.position.x + (x * gridSpacing) + AsteroidOffSet(), 
                        transform.position.y + (y * gridSpacing) + AsteroidOffSet(), 
                        transform.position.z + (z * gridSpacing) + AsteroidOffSet()), 
                    Quaternion.identity, 
                    transform);
    }

    //offset for asteroid
    float AsteroidOffSet()
    {
        return Random.Range(-gridSpacing / 2f, gridSpacing/2);
    }
}
