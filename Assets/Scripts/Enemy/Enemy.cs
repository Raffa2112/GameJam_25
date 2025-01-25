using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Toothling,
    Plant
}

public class Enemy : MonoBehaviour
{
    public EnemyType Type;
    [HideInInspector]
    public Transform PlayerTransform;

    [Header("Floating Effect Variables")]
    private bool isFloating = false;
    [SerializeField] private float floatHeight = 2f; // Height above the current position
    [SerializeField] private float floatSpeed = 0.5f; // Speed of floating motion
    [SerializeField] private float floatDuration = 5f; // How long the enemy floats before stopping
    [Header("Enemy Stats")]
    public float MoveSpeed = 1f;
    public float AttackRange = 2f;
    public float AttackRate = 2f;

    [Header("Shoot Variables")]
    private float ReloadTimer = 0;
    public float BulletSpeed = 10f;
    public Tooth ToothPrefab;
    public Transform ProjectileSpawnPoint;
    private EnemyBaseState currentState;
    [HideInInspector]
    public EnemyPatrollingState PatrollingState = new EnemyPatrollingState();
    [HideInInspector]
    public EnemyCombatState CombatState = new EnemyCombatState();
    [HideInInspector]
    public EnemyStunnedState StunnedState = new EnemyStunnedState();
    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Type == EnemyType.Toothling)
        {
            currentState = PatrollingState;
        }
        else if (Type == EnemyType.Plant)
        {
            currentState = CombatState;
        }
    }

    // Update is called once per frame
    void Update()
    {

        currentState.UpdateState(this);
        
    }

    //on trigger collider if collided by bubble then activate bubble child object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // Debug.Log("Bubble hit enemy");
            // Start floating effect
            if (!isFloating)
            {
                StartCoroutine(FloatEffect());
            }
            //destroy other gameobject
            Destroy(other.gameObject);

            //change states 

            currentState = StunnedState;
            currentState.EnterState(this);
            
        }

        
    }

    

    public void Shoot()
    {
        Debug.Log(ReloadTimer);
        ReloadTimer -= Time.deltaTime;
        if(ReloadTimer > 0)
        {
            // AudioSource.PlayOneShot(ClipCocking);
            return;
        }
        // Starts reloading
        ReloadTimer = AttackRate;
        // Shoot!
        Tooth Projectilie = Instantiate(ToothPrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
        Projectilie.Speed = BulletSpeed;

        // Sound
        // AudioSource.PlayOneShot(ClipShooting);

        // Screenshake
        // Impulse.GenerateImpulse();

        
    }

    private IEnumerator FloatEffect()
    {
        isFloating = true;
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * floatHeight;

        // Float upwards
        while (elapsedTime < floatDuration / 2f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / (floatDuration / 2f)));
            elapsedTime += Time.deltaTime * floatSpeed;
            yield return null;
        }

        elapsedTime = 0f;

        // Float downwards slightly for a "hover" effect
        while (elapsedTime < floatDuration / 2f)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, (elapsedTime / (floatDuration / 2f)));
            elapsedTime += Time.deltaTime * floatSpeed;
            yield return null;
        }

        // Return to original position (optional)
        transform.position = startPosition;
        isFloating = false;
    }
}
