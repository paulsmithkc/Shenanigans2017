using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    protected const double MAXIMUM_PURCHASE_POINTS = 5.0;
    private const double STARTING_VALUE = 5.0;
    private const double BASE_NO_SALE_CHANCE = 5.0;
    private const float TIME_TO_BUY = 2.0f;

    private Rigidbody _rigidBody;
    public float stepBuffer = 1.0f;
    public float stepDistance = 0.1f;
    public Transform following;

    public bool _isAtCounter;
    public float _timeTillBuy;
    public Item _itemBought;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _isAtCounter = false;
        _timeTillBuy = TIME_TO_BUY;
    }

    // Update is called once per frame
    void Update ()
    {
        if (following != null)
        {
            Vector3 direction = (following.position - transform.position).normalized;
            Vector3 target = following.position - direction * stepBuffer;
            target.y = transform.position.y;
            _rigidBody.MovePosition(
                Vector3.MoveTowards(transform.position, target, stepDistance)
            );
        }
        if (_isAtCounter)
        {
            _timeTillBuy -= Time.deltaTime;
            if (_timeTillBuy <= 0.0f)
            {
                _itemBought = SelectItemToBuy();
                _itemBought.transform.parent = 
                    this.transform;
                _itemBought.transform.position =
                    this.transform.position 
                    + 0.5f * this.transform.right
                    - 0.5f * this.transform.forward;
                _itemBought.rigidBody.useGravity = false;
                //_itemBought.rigidBody.isKinematic = true;
                _isAtCounter = false;
            }
        }
    }

    /// <summary>
    /// Buys the item.
    /// Current appends a frequence range for each item 
    /// based on its relative value, then randomly selects from the resulting distribution.
    /// </summary>
    /// <returns>The item.</returns>
    public Item SelectItemToBuy()
    {
        Item selectedItem = null;

        var counter = GameObject.FindObjectOfType<SalesCounterTop>();
        var items = counter.items;
        selectedItem = items[0];

        //var counter = GameObject.FindObjectOfType<SalesCounter>();
        //var distribution = new List<Item>();
        //var items = counter.items;

        //foreach (var item in items)
        //{
        //    var frequency = (int)CalculatePurchasePoints(item);

        //    if (frequency >= BASE_NO_SALE_CHANCE)
        //    {
        //        distribution.AddRange(System.Linq.Enumerable.Repeat(item, frequency));
        //    }
        //}

        //if (distribution.Count > 0)
        //{
        //    selectedItem = distribution[Random.Range(0, distribution.Count)];
        //}

        return selectedItem;
    }

    public virtual double CalculatePurchasePoints(Item item)
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

    protected virtual double getPointsForProperty(string property)
    {
        return 0.0;
    }

}
