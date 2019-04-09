using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
//ensure that the playermotor script is found so this script can be used
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController: MonoBehaviour
{
	//Reference to the camera
	public Camera cam;
	//Player move speed value
    public float moveSpeed;
	//Input value
    private Vector3 input;
	//Player velocity
    public Vector3 playerVelocity;

    Vector3 currentPosition;
    Vector3 lastPosition;
	
	//Reference to the PlayerMotor Script
	private PlayerMotor thePlayerMotor;
	//Player LayerMask reference
	public LayerMask movementMask;
	
	//Players rigidbody
    private Rigidbody rb;

	//Keep track of what is being focused on
	public Interactable focus;

    /*
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawLine(transform.position, thePlayerMotor.target.);
    }
    */

    void Start()
    {
		//get the attached rigidbody component
        rb = GetComponent<Rigidbody>();
		//Find the camera in the scene & attach it to the player
		cam = FindObjectOfType<Camera>();
		//Get the playermotor script attached to the player
		thePlayerMotor = GetComponent<PlayerMotor>();
        //sets the position for gamemanger
         transform.position = GameManager.instance.nextPlayerPosition;
    }

    void Update()
    {
		//check if we are hovering over the ui, if we are then stop the player from moving
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		//input variable  = movement made on the horizontal or vertical axis
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		//set the players velocity to the detected input multiplied by the move speed
		playerVelocity = input * moveSpeed;

		//LEFT MOUSE BUTTON USED FOR MOVEMENT

		//check if the left mouse button has been pressed
		if (Input.GetMouseButtonDown(0))
		{
			//Cast a ray from the camera towards whatever we have clicked on
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			//store the info detected in a raycast variable
            
			RaycastHit hit;
			//if the ray hits something then run the next lines of code
			if (Physics.Raycast(ray, out hit, 100, movementMask))
			{
				//move the player to a point
                
				thePlayerMotor.MoveToPoint(hit.point);
				Debug.Log(("We hit" + hit.collider.name + " " + hit.point));
				StopFocusing();
				//stop focusing on any objects
			}
		}
		//RIGHT MOUSE BUTTON USED FOR INTERACTING
	//if the right mouse button is pressed
	if (Input.GetMouseButtonDown(1))
	{
		//Cast a ray from the camera towards whatever we have clicked on
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		//store the info detected in a raycast variable
		RaycastHit hit;
		//if the ray hits something then run the next lines of code
		if (Physics.Raycast(ray, out hit, 100))
		{
			//Debug.Log(("We hit" + hit.collider.name + " " + hit.point));

			//Check if we hit an interactable object
			hit.collider.GetComponent<Interactable>();
			Interactable interactable = hit.collider.GetComponent<Interactable>();
			//if we did set it as our focus
			if (interactable != null)
			{
				//call the set focus function and state that the selected object is interactable
				SetFocus(interactable);
			}

			//focus on an object
			//Collect an item
			//stop focusing on any objects
		}
	}

}
	

    void FixedUpdate()
    {
		//set the rigidbodys velocity to the player velocity
        rb.velocity = playerVelocity;

        //controls encounter zone stuff
        currentPosition = transform.position;
        if(currentPosition == lastPosition)
        {
            GameManager.instance.isWalking = false;
        }
        else
        {
            GameManager.instance.isWalking = true;
        }
        lastPosition = currentPosition;
        
       
        /*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        */
    }
	//Set the object to be focused on
	void SetFocus(Interactable newFocus)
	{
		//error check in case there is already something that is focused on
		if (newFocus != focus)
		{
			//in case there was no object initally focused on 
			if(focus != null)
			{
				//set the current object that is focused to DeFocused
				focus.OnDeFocused();
			}
			//set the current focused object to the newly focused object
			focus = newFocus;
			//follow the newly selected object.
			thePlayerMotor.FollowTarget(newFocus);
		}
		//check that the focused object can be interacted with from where the players current position is
		newFocus.OnFocused(transform);
	}
	//function to stop the player focusing on an object
	void StopFocusing()
	{
		//in case there was no object initally focused on 
		if (focus != null)
		{
			//set the focused object to not be focused on by calling the OnDeFocused function from the Interactable script, therefore not being interacted with
			focus.OnDeFocused();
		}
		//set focus to null, so the player no longer focuses on the object
		focus = null;
		//call the stop following the target function to stop the player from facing the object
		thePlayerMotor.StopFollowingTarget();
	}

    private void OnTriggerEnter(Collider other)
    {
        /* //also make tag for scene name
        if(other.tag == "SceneNameHere")
        {
            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            GameManager.instance.nextPlayerPosition = col.spawnPoint.transform.position;
            GameManager.instance.sceneToLoad = col.sceneToLoad;
            GameManager.instance.LoadNextScene();
        }
        */
        if(other.tag == "EncounterZone")
        {
            RegionData region = other.gameObject.GetComponent<RegionData>();

            GameManager.instance.currentRegion = region;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EncounterZone")
        {
            GameManager.instance.canEncounter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EncounterZone")
        {
            GameManager.instance.canEncounter = false;
        }
    }
}
