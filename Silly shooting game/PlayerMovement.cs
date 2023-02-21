using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rbMaicao;
    private GameObject maiPunch;

    [SerializeField] int health;
    [SerializeField] float boundsX, boundsY;
    [SerializeField] [Range(0,20)] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] float coolDownShot, durationPunch;
    [SerializeField] float  punchCooldown;

    public float power;

    private float verticalMovement, horizontalMovement;
    private float lastShot, lastPunch;
    private bool isPunching = false;


    // Start is called before the first frame update
    void Start()
    {
        rbMaicao = GetComponent<Rigidbody>();

        maiPunch = GameObject.Find("MaiPunch");

        maiPunch.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        basicMovement();
        Shoot();
        CheckOutOfBounds(gameObject);
        followMouse();
        StartCoroutine(MaiPunch());
        checkHealth();
    }

    private void basicMovement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * Time.deltaTime * verticalMovement * speed, Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalMovement * speed, Space.World);
    }
    private void Shoot()
    {
        if (Input.GetMouseButton(1) && Time.time - lastShot > coolDownShot && !isPunching)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            lastShot = Time.time;
        }
    }

    private void CheckOutOfBounds(GameObject go) //need to optimize later
    {
        if (go.transform.position.x >= boundsX)
        {
            go.transform.position = new Vector3(boundsX, go.transform.position.y, go.transform.position.z);
        }
        if (go.transform.position.x <= -boundsX)
        {
            go.transform.position = new Vector3(-boundsX, go.transform.position.y, go.transform.position.z);
        }
        if (go.transform.position.y >= boundsY)
        {
            go.transform.position = new Vector3(go.transform.position.x, boundsY, go.transform.position.z);
        }
        if (go.transform.position.y <= -boundsY)
        {
            go.transform.position = new Vector3(go.transform.position.x, -boundsY, go.transform.position.z);
        }
    }
    private void followMouse() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; /* gets the camera position in the game and
                                                                                                        * subtracts from the player position to get
                                                                                                        * the distance between the two */

        difference.Normalize(); //makes the difference a value between 1 and 0

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; /* uses Mathf to calculate angles, using the angles of the difference
                                                                                    * between player and mouse positions and than convert it to degrees */

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ+270); // assign the rotation to the previous float + offset


    }

    IEnumerator MaiPunch() //Activate the punch and the collision 
    {
        if (Input.GetMouseButton(0) && Time.time - lastPunch > punchCooldown )
        {
            isPunching = true;

            maiPunch.SetActive(true);

            yield return new WaitForSeconds(durationPunch);

            isPunching = false;

            maiPunch.SetActive(false);
            lastPunch = Time.time;

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet" && !isPunching)
        {
            health = health - 100;
            Destroy(other.gameObject);
        }
    }

    private void checkHealth() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
