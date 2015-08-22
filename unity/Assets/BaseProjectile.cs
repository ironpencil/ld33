using UnityEngine;
using System.Collections;
using System.Text;

public class BaseProjectile : MonoBehaviour
{

    public float damage = 1.0f;

    public EffectSource damageType = EffectSource.Universal;

    public float lifespan;

    public float die = 0.0f;

    public Rigidbody2D shooter;
    public Weapon shooterScript;

    protected Collider2D thisCollider;

    protected Rigidbody2D thisRB;

    protected Vector2 previousVelocity = Vector2.zero;
    protected Vector2 currentVelocity = Vector2.zero;

    protected bool initialized = false;
    protected bool keepVelocityUpdated = true;

    public virtual void Initialize(Vector2 velocity)
    {
        currentVelocity = velocity;
        previousVelocity = velocity;
        initialized = true;
    }

    // Use this for initialization
    public virtual void Start()
    {
        if (lifespan > 0)
        {
            die = Time.time + lifespan;
        }

        thisCollider = GetComponent<Collider2D>();
        thisRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

        if (die > 0 && Time.time > die)
        {
            Destroy(gameObject);
        }
    }

    public virtual void FixedUpdate()
    {

        if (!initialized)
        {
            Initialize(thisRB.velocity);
            thisCollider.enabled = true;
        }
        else
        {
            if (keepVelocityUpdated)
            {
                previousVelocity = currentVelocity;
                currentVelocity = thisRB.velocity;
            }
        }
    }

    //use physics collision if this collider is a physics collider
    public virtual void OnCollisionEnter2D(Collision2D coll)
    {

        if (!thisCollider.isTrigger)
        {
            CollideWithObject(coll);
        }
    }

    protected virtual void CollideWithObject(Collision2D coll)
    {
        TakesDamage enemy = coll.gameObject.GetComponent<TakesDamage>();

        bool doDestroy = true;

        if (enemy != null)
        {
            if (enemy.MarkedForDeath)
            {
                this.thisRB.velocity = previousVelocity;
                doDestroy = false;
            }
            else
            {
                enemy.ApplyDamage(damage, damageType, coll, thisCollider);
            }
        }

        if (doDestroy)
        {
            Destroy(this.gameObject);
        }
    }

}

