using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpspeed;
    private CharacterController cc;
    private Vector3 moveDirection;
    public Animator am;
    private HealthSystem healthSystem;
    private HungerSystem hungerSystem;
    private Backpack backpack;
    public UIManager UIManager;
    void Start()
    {
        Vector3 moveDirection = Vector3.zero;
    }
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        healthSystem = GetComponent<HealthSystem>(); 
        hungerSystem = GetComponent<HungerSystem>();
        hungerSystem.SetHealthSystem(healthSystem);
        backpack = GetComponent<Backpack>();
    }
    public  void Consume(Item item)
    {
        var foodData = item.Data as FoodData;
        Debug.Log(foodData);
        if (foodData != null)
        {
            healthSystem.IncreaseHealth(foodData.health);
            hungerSystem.DecreaseHungerLevel(foodData.hunger);
        }
    }
    public  void Drop(GameObject dropped)
    {
        var go = Instantiate(dropped) as GameObject;
        var spawnPoint = transform.position + (transform.forward * 10);
        spawnPoint.y += 1000;
        var ray = new Ray(spawnPoint, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point);
            spawnPoint.y = hit.point.y + go.transform.localScale.y * 0.5f;
        }
        go.transform.position = spawnPoint;
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*if (hit.gameObject.CompareTag("Food"))
        {
            var food = hit.gameObject.GetComponent<Food>();
            Destroy(hit.gameObject);
            healthSystem.IncreaseHealth(food.health); 
            hungerSystem.DecreaseHungerLevel(food.hunger);

        }*/
        if (hit.gameObject.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (backpack.AddItem(hit.gameObject))
                {
                    Destroy(hit.gameObject);
                }
            }
        }
        else if (hit.gameObject.CompareTag("Obstacle"))
        {
            var food = hit.gameObject.GetComponent<Obstacle>();
            healthSystem.DecreaseHealth(food.health);
        }
    }
    //void OnController
    /*void OnControllerCollider
    {
        if (other.gameObject.CompareTag("Food"))
        {
            collided = null;
            panel.SetActive(false);
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        am.SetFloat("speed",cc.velocity.magnitude);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
            am.SetBool("canRun", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
            am.SetBool("canRun", false);
        }
        if (cc.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpspeed;
                Debug.Log(moveDirection.y);
                am.SetBool("Jump", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            am.SetBool("Jump", false);
        }
        transform.Rotate(0, Input.GetAxis("Mouse X") * 60 * Time.deltaTime, 0);
        moveDirection.y -= 9.8f;
        cc.Move(moveDirection * speed * Time.deltaTime);
        var magnitude = new Vector2(cc.velocity.x, cc.velocity.y).magnitude;
        am.SetFloat("speed", magnitude);
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.ToggleInventory();
        }
    }
}
