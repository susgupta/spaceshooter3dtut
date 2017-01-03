using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour {

    //grid management
    [SerializeField]
    int numberOfAsteroidsOnAnAxis = 10;

    [SerializeField]
    int gridSpacing = 100;

    //prefab reference explicit for asteroid
    [SerializeField]
    Asteroid asteroidPreFab;

    //list of created asteroids
    List<Asteroid> asteroidPreFabList = new List<Asteroid>();
    
    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += PlaceAsteroids;
        EventManager.onPlayerDeath += DestroyAsteroids;
    }

    void OnDisable()
    {
        //remove delegate to un0subscribe
        EventManager.onStartGame -= PlaceAsteroids;
        EventManager.onPlayerDeath -= DestroyAsteroids;
    }
    
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
        Asteroid createdAsteroid = Instantiate(asteroidPreFab, 
                                                new Vector3(
                                                    transform.position.x + (x * gridSpacing) + AsteroidOffSet(), 
                                                    transform.position.y + (y * gridSpacing) + AsteroidOffSet(), 
                                                    transform.position.z + (z * gridSpacing) + AsteroidOffSet()), 
                                                Quaternion.identity, 
                                                transform)
                                                as Asteroid;

        //add to list
        asteroidPreFabList.Add(createdAsteroid);
    }

    //destroy asteroids from list
    void DestroyAsteroids()
    {
        foreach(Asteroid currentAsteroid in asteroidPreFabList)
        {
            //must check if asteroid was not previously destroyed by say the player
            if (currentAsteroid != null)
            {
                currentAsteroid.SelfDestruct();
            }            
        }

        //purge list
        asteroidPreFabList.Clear();
    }

    //offset for asteroid
    float AsteroidOffSet()
    {
        return Random.Range(-gridSpacing / 2f, gridSpacing/2);
    }
}
