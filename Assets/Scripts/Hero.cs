using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    protected const float MAXIMUM_PURCHASE_POINTS = 5.0f;
    private const float STARTING_VALUE = 1.0f;
    private const float BASE_NO_SALE_CHANCE = 2.0f;
    private const float TIME_TO_BUY = 5.0f;

    private Rigidbody _rigidBody;
    private float _stepBuffer = 1.0f;
    private float _stepDistance = 2;
    public Transform following;

    public bool _isAtCounter;
    public bool _isExiting;
    public float _timeTillBuy;
    public Item _itemBought;
    public Slider _thinkingSlider;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _isAtCounter = false;
        _isExiting = false;
        _timeTillBuy = TIME_TO_BUY;
        _itemBought = null;
    }

    // Update is called once per frame
    void Update ()
    {
        if (following != null)
        {
            Vector3 pos = transform.position;
            Vector3 target = following.position;
            target.y = pos.y;
            Vector3 direction = (target - pos).normalized;
            target -= direction * _stepBuffer;
            if (_isExiting && (target - pos).magnitude <= _stepDistance)
            {
                GameObject.Destroy(gameObject);
                return;
            }
            else
            {  
                _rigidBody.MovePosition(
                    Vector3.MoveTowards(pos, target, _stepDistance * Time.deltaTime)
                );
                _rigidBody.MoveRotation(
                    Quaternion.RotateTowards(
                        _rigidBody.rotation,
                        Quaternion.LookRotation(direction, Vector3.up),
                        90 * Time.deltaTime
                    )
                );
            }
        }

        if (_isAtCounter && !_isExiting)
        {
            var counter = GameObject.FindObjectOfType<SalesCounterTop>();
            var items = counter.items.Where(x => !x.isBought).ToList();
            foreach (var i in items)
            {
                float v = Mathf.Max(0.0f, CalculatePurchasePoints(i));
                i.itemValue = v;
            }

            _thinkingSlider.maxValue = TIME_TO_BUY;
            _thinkingSlider.value = _timeTillBuy;
            _thinkingSlider.gameObject.SetActive(true);

            _timeTillBuy -= Time.deltaTime;
            if (_timeTillBuy <= 0.0f)
            {
                _isExiting = true;
                _isAtCounter = false;
                _itemBought = SelectItemToBuy(items);
                if (_itemBought != null)
                {
                    counter.items.Remove(_itemBought);

                    _itemBought.rigidBody.useGravity = false;
                    _itemBought.rigidBody.detectCollisions = false;
                    _itemBought.rigidBody.isKinematic = true;
                    _itemBought.collider.enabled = false;
                    _itemBought.transform.parent = this.transform;
                    _itemBought.rigidBody.position =
                        this.transform.position
                        - 0.5f * this.transform.right
                        + 0.5f * this.transform.forward
                        + 0.2f * this.transform.up;
                }
                
                var spawn = GameObject.FindObjectOfType<HeroSpawner>();
                foreach (var h in spawn.heroes)
                {
                    if (ReferenceEquals(h.following, this.transform))
                    {
                        h.following = spawn.desk;
                    }
                }
                following = spawn.exit;
                spawn.heroes.Remove(this);
            }
        }
        else
        {
            _thinkingSlider.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Buys the item.
    /// Current appends a frequence range for each item 
    /// based on its relative value, then randomly selects from the resulting distribution.
    /// </summary>
    /// <returns>The item.</returns>
    public Item SelectItemToBuy(IList<Item> items)
    {
        Item selectedItem = null;
        
        float sum = BASE_NO_SALE_CHANCE;
        foreach (var i in items)
        {
            float v = Mathf.Max(0.0f, CalculatePurchasePoints(i));
            i.itemValue = v;
            sum += v;
        }

        float rand = Random.Range(0.0f, sum);
        if (rand < BASE_NO_SALE_CHANCE)
        {
            Debug.LogFormat(
                "{0} chose not to buy anything ({1}/{2})", 
                GetType().Name, BASE_NO_SALE_CHANCE, sum
            );
        }
        else if (items.Count > 0)
        {
            float cumulative = BASE_NO_SALE_CHANCE;
            foreach (var i in items)
            {
                cumulative += i.itemValue;
                if (rand < cumulative)
                {
                    selectedItem = i;
                    break;
                }
            }
            if (selectedItem == null)
            {
                items.LastOrDefault();
            }
        }

        if (selectedItem != null)
        {
            Debug.LogFormat(
                "{0} chose to buy {3} {4} ({1}/{2})",
                GetType().Name, selectedItem.itemValue, sum, selectedItem.colorName, selectedItem.modelName
            );
        }
        
        return selectedItem;
    }

    public virtual float CalculatePurchasePoints(Item item)
    {
        var points = STARTING_VALUE;
        var properties = item.itemTag.itemName.Split(new char[] { ' ' });

        foreach (var property in properties)
        {
            points += getPointsForProperty(property.Replace("-Flavored", string.Empty));
        }

        //calculate based on description also / calculate differently?

        return points;
    }

    protected virtual float getPointsForProperty(string property)
    {
        return 0.0f;
    }

	/*
	case ”Blueberry" :
	case  "Chicken" :
	case  "Chocolate" :
	case  "Gerbil" :
	case  "Hemlock" :
	case  "Hemp" :
	case  "Hummus" :
	case  "Vanilla" :
	case  "Cherry"
	case  "Caramel"
	case  "Arsenic"
	case  “Poppy Seed"
	case  "Pork"
	case  "Waffle"
	case "Rusty"
	case  "Shiny"
	case  "Transparent"
	case ”Artificial"
	case  "Natural"
	case  "Organic"
	case  "Synthetic"
	case  "Vegan"
	case “Magnetic"
	case  "Metallic"
	case  "Monopolar"
	case  "Plastic"
	case  "Radioactive"
	case  "Static" 
	case “Double-Edged"
	case  "Single-Edged"
	case  "Chewable"
	case "Flight"
	case  "Dark-Vision"
	case  "Musical"
	case  "Troll-Summoning"
	case “Invisibility"
	case  "Strength-Increasing"
	case  "Stupidity"
	case  "Wisdom"
	case "Potable"
	case  "Weapon" 
	case  "Elixr"
	case  "Potion"
	case  "Sauce"
	case  "Juice"
	case  "Oil"
	case  "Ale"
	case  "Stout"
	case  "Beer"
	case  "Wine"
	case  "Dagger"
	case  "Sword"
	case  "Butter Knife"
	case  "Axe"
	*/

	/*
	case "insanity" :
	case  "mumps" :
	case  "turning orange" :
	case  "lack of confidence" :
	case  "bronchitis" :
	case "bow-leggedness" :
	case  "tone-deafness" :
	case  "gluten allergies" :
	case  "veganism" :
	case  "baldness" :
	case “reduced fashion sense" :
	case  "laziness" :
	case  "extra limbs" :
	case  "emphysema" :
	case  "sleepwalking" :
	case "orcs" :
	case  "kobolds" :
	case  "aardvarks" :
	case  "Elvis" :
	case  "dragons" 
	case  "imaginary beings" 
	case  "walls" :
	case  "Volkswagens" ;
	case  "chariots" :
	case  "pterodactyls" :
	case  "quarks" :
	*/
}
