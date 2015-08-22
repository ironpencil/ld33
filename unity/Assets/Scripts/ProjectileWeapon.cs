using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public GameObject bulletPrefab;
    public Rigidbody2D shooter;

    public int bulletsPerShot = 1;    

    public Vector2 bulletRotationRange = new Vector2(0.0f, 0.0f);

    public Vector2 bulletMinMaxForce = new Vector2(5000.0f, 6000.0f);

    public Vector2 shooterForce = new Vector2(-40000, 0);

    public CameraShaker cameraShaker;

    public ParticleSystem particleSystem;
    public Vector2 minMaxParticles = new Vector2(6, 12);

    public bool autoFire = true;
    
    public bool varyFireDelay = false;
    public float fireDelayVariantRange = 0.01f;
    protected float actualFireDelayVariant = 0.0f;

    protected float lastShot = 0.0f;

    protected bool doShoot = false;

    protected bool didShoot = false;

    protected bool stoppedShooting = false;

    [SerializeField]
    private bool continuousFire = false;

    public virtual void Awake()
    {
        if (cameraShaker == null)
        {
            cameraShaker = gameObject.GetComponent<CameraShaker>();
        }

        if (attackSound == null)
        {
            attackSound = gameObject.GetComponent<SoundEffectHandler>();
        }
    }

    public virtual void Update()
    {
        if (!didShoot)
        {
            stoppedShooting = true;
        }

        doShoot = didShoot;
        didShoot = false;
    }

    public virtual void FixedUpdate()
    {
        if (continuousFire)
        {
            doShoot = true;
            stoppedShooting = true;
        }

        if (doShoot)
        {
            if (Time.fixedTime > lastShot + attackDelay + actualFireDelayVariant)
            {
                //if the weapon has auto mode or if they had previously stopped shooting
                if (autoFire || stoppedShooting)
                {

                    for (int i = 0; i < bulletsPerShot; i++)
                    {
                        Fire();
                    }

                    if (attackSound != null)
                    {
                        attackSound.PlayEffect();
                    }

                    shooter.AddRelativeForce(shooterForce);

                    if (cameraShaker != null)
                    {
                        cameraShaker.Shake();
                    }

                    if (particleSystem != null)
                    {
                        particleSystem.Emit(Random.Range((int)minMaxParticles.x, (int)minMaxParticles.y + 1));
                    }

                    lastShot = Time.fixedTime;
                    if (varyFireDelay && fireDelayVariantRange > 0.0f)
                    {
                        actualFireDelayVariant = Random.Range(0.0f, fireDelayVariantRange);
                    }
                }
            }

            stoppedShooting = false;
        }

        doShoot = false;
    }

    public override void Attack()
    {
        didShoot = true;
    }

    protected virtual BaseProjectile Fire()
    {

        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        BaseProjectile bulletScript = bullet.GetComponent<BaseProjectile>();

        bulletScript.shooter = shooter;
        bulletScript.shooterScript = this;

        //TODO: Put bullets in a dynamic object store
        //bullet.transform.parent = ObjectManager.Instance.dynamicObjects.transform;

        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        float rotationAdjust = Random.Range(bulletRotationRange.x, bulletRotationRange.y);

        bulletRB.rotation += rotationAdjust;

        //bulletRB.velocity = transform.forward * bulletSpeed;

        Vector2 bulletDirection = new Vector2(1.0f, 0.0f);

        float bulletSpeed = Random.Range(bulletMinMaxForce.x, bulletMinMaxForce.y);

        Vector2 bulletVelocity = bulletDirection * bulletSpeed;
        //bulletScript.Initialize(bulletVelocity);

        bulletRB.AddRelativeForce(bulletVelocity);

        return bulletScript;

        //DebugLogger.Log("Fire() pv=" + previousVelocity + " v=" + bulletRB.velocity);
    }
}
