using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    public int maxUnits;
    public float waitInterval;
    public Hero[] spawnables;

    public SalesCounterTop counterTop;
    public Transform desk;
    public Transform exit;
    public List<Hero> heroes;

	// Use this for initialization
	void Start()
    {
        counterTop = GameObject.FindObjectOfType<SalesCounterTop>();
        heroes = new List<Hero>();
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
                Hero hero = Instantiate(spawnables[index], transform.position, Quaternion.Euler(0,180,0));
                Hero prevHero = heroes.LastOrDefault(x => !x._isExiting);

                Transform target = prevHero == null ? desk : prevHero.transform;
                hero.following = target;
                hero.spawn = this;
                heroes.Add(hero);
            }
        }
    }
}
