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
    public HeroSpawner spawn;

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

        var counterTop = spawn.counterTop;
        IList<Item> items = null;
        bool isAtFrontOfLine = ReferenceEquals(following, spawn.desk);
        if (isAtFrontOfLine)
        {
            items = counterTop.items.Where(x => !x.isBought).ToList();
            foreach (var i in items)
            {
                float v = Mathf.Max(0.0f, CalculatePurchasePoints(i));
                i.itemValue = v;
            }
        }
        if (isAtFrontOfLine && _isAtCounter && !_isExiting)
        {
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
                    counterTop.items.Remove(_itemBought);

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

}
