using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.VFX;


public class BallMovement : MonoBehaviour
{
    AudioSource audioSource;
    Vector3 direction;
    [SerializeField] float speed;
    Rigidbody rb;
    Vector3 ground = new Vector3(0, -1, 0);
    public static bool ballFall;
    public VisualEffect vfx;
    float step;
    float targetX;
    [SerializeField] float boundary = 2.5f;
    Vector3 position;
    [SerializeField] float horizontalSpeed=20f;


    public KeyCode dashKeyRight = KeyCode.D;
    public float dashVelocity = 0.01f;
    private Vector3 right;
    public KeyCode dashKeyLeft = KeyCode.A;
    private TargetDirection targetDirection;




    public float x;
    public float y;
    public float z;
    public float w;
    

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
        rb = GetComponent<Rigidbody>();
        ballFall = false;
        StartCoroutine(RandomizeColorVFX());
        right = transform.right;
        

    }

    // Update is called once per frame
    void Update()
    {
        vfx.SetVector4("Color2", new Vector4(x, y, z, w));
        vfx.SetVector4("Color", new Vector4(x, y, z, w));
        if(Input.GetKeyDown(dashKeyLeft))
        {
            SetDirection(TargetDirection.left);
        }


        else if (Input.GetKeyDown(dashKeyRight))
        {

            SetDirection(TargetDirection.right);
            
        }
        BallDash();

    }
    void FixedUpdate()
    {
        Vector3 movement = direction * Time.deltaTime * speed;
        transform.position += movement / 100;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            GameManager.instance.IncrementScore();
            Destroy(collision.gameObject);
            Debug.Log("Im working");
        }
    }








    //-----------------------Functions-----------------------------//


    IEnumerator RandomizeColorVFX()
    {
        while (true)
        {
            x = Random.Range(-100f, 100f);
            y = Random.Range(-100f, 100f);
            z = Random.Range(-100f, 100f);
            yield return new WaitForSeconds(1f);
        }
    }
    
        
      





    private void SetDirection(TargetDirection direction)
    {
        targetDirection = direction;
        step = targetDirection == TargetDirection.left ? -1f : 1f;
        targetX += 2.5f * step;
        targetX= Mathf.Clamp(targetX,-1f*boundary,boundary);
    }

    private void BallDash()
    {
        position = transform.position;

        if (Mathf.Abs(transform.position.x-targetX)>0f)
        {
            transform.position = Vector3.MoveTowards(position, new Vector3(targetX, position.y, position.z),Time.smoothDeltaTime*horizontalSpeed);
        }else
        {
            targetDirection = TargetDirection.stop;
        }

    }









}


public enum TargetDirection
{
    left,
    right,
    stop
}
    

  
