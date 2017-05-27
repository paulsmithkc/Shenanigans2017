using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{

    public int maxUnits;
    public float waitInterval;
    public GameObject[] spawnables;

    public Transform desk;
    public Transform leave;

    List<Transform> heroes;

	// Use this for initialization
	void Start ()
    {
        heroes = new List<Transform>();
        StartCoroutine(SpawnHero());
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Make sure first item in list is targeting desk
        if (heroes.Count != 0)
        {
            heroes[0].gameObject.GetComponent<Hero>().following = desk;
        }
	}

    IEnumerator SpawnHero()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitInterval);
            if (heroes.Count < maxUnits)
            {
                int index = Random.Range(0, spawnables.Length);
                GameObject hero = Instantiate(spawnables[index], transform.position, Quaternion.identity);
                Transform target = heroes.Count == 0 ? desk : heroes[heroes.Count - 1].transform;
                hero.GetComponent<Hero>().following = target;
                heroes.Add(hero.transform);
            }
        }
    }
}
