using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTagGenerator : MonoBehaviour {

    private ItemTooltip itemTooltip;
    private ItemTag itemTag;

	// Use this for initialization
	void Start () {
        itemTooltip = GetComponent<ItemTooltip>();
        DispenseTag();
    }
	
	// Update is called once per frame
	void Update () {
        if (itemTag == null)
        {
            DispenseTag();
        }
	}

    public ItemTag DispenseTag()
    {
        var t = itemTag;
        itemTag = GenerateTag();
        itemTooltip.ShowTag(itemTag);
        return t;
    }

	private const int MAXIMUM_MODIFIER_COUNT = 2;
	private const int MAXIMUM_SIDE_EFFECT_COUNT = 6;

    private readonly string[] _flavors = new string[] {
        "Blueberry", "Chicken", "Chocolate", "Gerbil", "Hemlock", "Hemp", "Hummus", "Vanilla",
		"Cherry", "Caramel", "Arsenic", "Poppy Seed", "Pork", "Waffle",
    };

    private readonly string[][]_exclusiveModifierTable = new string[][] {
        new string[] {"Rusty", "Shiny", "Transparent"},
        new string[] {"Artificial", "Natural", "Organic", "Synthetic", "Vegan", },
        new string[] {"Magnetic", "Metallic", "Monopolar", "Plastic", "Radioactive", "Static" },
		new string[] {"Double-Edged", "Single-Edged", "Chewable"},
    };

    private readonly string[] _functions = new string[] {
        "Flight", "Dark-Vision", "Musical", "Troll-Summoning",
        "Invisibility", "Strength-Increasing", "Stupidity", "Wisdom",
    };

    private readonly string[] _itemTypes = new string[] { "Potable", "Weapon" };

    private readonly Dictionary<string, string[]> _items = new Dictionary<string, string[]>{
        {"Potable", new string[] {"Elixr", "Potion", "Sauce", "Juice", "Oil", "Ale", "Stout", "Beer", "Wine"}},
        {"Weapon", new string[] {"Dagger", "Sword", "Butter Knife", "Axe"}}
    };

    private readonly string[] _potableContainers = new string[] {
        "Beaker", "Vial", "Flask", "Shot", "Bong"
    };

    private readonly string[]_possibleSideEffects = new string[]{
        "insanity", "mumps", "turning orange", "lack of confidence", "bronchitis",
		"bow-leggedness", "tone-deafness", "gluten allergies", "veganism", "baldness",
		"reduced fashion sense", "laziness", "extra limbs", "emphysema", "sleepwalking"
    };

    private readonly string[][] _worksAgainst = new string[][] {
        new string[] {"orcs", "kobolds", "aardvarks", "Elvis", "dragons", "imaginary beings" },
        new string[] {"walls", "Volkswagens", "chariots", "pterodactyls", "quarks" },
    };

    private string RandomElement(string[] options)
    {
        return options[Random.Range(0, options.Length)];
    }
    
    private ItemTag GenerateTag()
    {
		var currentModifierCount = MAXIMUM_MODIFIER_COUNT;
        var titleElements = new List<string>();

        if (Random.Range(0, 10) <= 6)
        {
            titleElements.Add(string.Format("{0}-Flavored", RandomElement(_flavors)));
			--currentModifierCount;
        }

		var usedIndexes = new List<int>{ };
        while (currentModifierCount > 0)
        {
			var index = Random.Range(0, _exclusiveModifierTable.Length); 
			if (!usedIndexes.Contains(index)) {
				titleElements.Add (RandomElement (_exclusiveModifierTable [index]));
				usedIndexes.Add (index);
				--currentModifierCount;
			}
        }

		titleElements.Add (RandomElement (_functions));

		var itemType = RandomElement (_itemTypes);
        titleElements.Add(RandomElement(_items[itemType]));

        switch (itemType)
        {
            case "Potable":
                titleElements.Insert(0, string.Format("{0} of", RandomElement(_potableContainers)));
                break;
            default:
                break;
        }
        
        var descriptionElements = new List<string>();

		usedIndexes = new List<int> ();
		var currentSideEffectCount = Random.Range(1, MAXIMUM_SIDE_EFFECT_COUNT);
        var sideEffects = new List<string>();
		while (sideEffects.Count <= currentSideEffectCount)
		{
        	var index = Random.Range(0, _possibleSideEffects.Length); 
			if (!usedIndexes.Contains (index)) {
				sideEffects.Add (_possibleSideEffects [index]);
				usedIndexes.Add (index);
			}
        }
        descriptionElements.Add(string.Format("Possible Side Effects:  {0}", string.Join(", ", sideEffects.ToArray())));

        List<string> worksAgainst = new List<string>();
        for (var index = Random.Range(0, _worksAgainst.Length); index >= 0; --index)
        {
            worksAgainst.Add(RandomElement(_worksAgainst[index]));
        }
        descriptionElements.Add(string.Format("Effective Against:  {0}", string.Join(", ", worksAgainst.ToArray())));
        
        var title = string.Join(" ", titleElements.ToArray());
        var description = string.Join("\n", descriptionElements.ToArray());
        return new ItemTag
        {
            itemName = title,
            itemDescription = description
        };
    }
}
