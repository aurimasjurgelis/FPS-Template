using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Singleton:
    public static PlayerController instance;
    public void Awake()
    {
        instance = this;
    }

    [Header("Player Movement")]
    public CharacterController characterController;
    public float moveSpeed = 8f, gravityModifier, jumpPower, runSpeed = 12f;
    private Vector3 moveInput;
    public Transform cameraTransform;

    public float cameraSensitivity;
    public bool invertX;
    public bool invertY;

    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatLayerToCheck;

    public Animator animator;

    public GameObject bullet;
    public Transform firePoint;

    public Gun activeGun;

    public void Start()
    {
        UIController.instance.ammoText.text = "Ammo: " + activeGun.currentAmmo;
    }
    private void Update()
    {



        //store y velocity
        float yStore = moveInput.y;
        Vector3 verticalMove = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 horizontalMove = transform.right * Input.GetAxisRaw("Horizontal");

        moveInput = (verticalMove + horizontalMove);
        moveInput.Normalize();
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput *= moveSpeed;
        }

        moveInput.y = yStore;
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (characterController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatLayerToCheck).Length > 0;

        if (canJump)
        {
            canDoubleJump = false;
        }

        //Handle Jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
        }
        else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Double Jumping...");
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }

        characterController.Move(moveInput * Time.deltaTime);


        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * cameraSensitivity;
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles + new Vector3(mouseInput.y, 0f, 0f));


        //Single shots
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 50f))
            {
                if (Vector3.Distance(cameraTransform.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 50f));
            }

            FireShot();
        }


        //repeats shots
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }

        animator.SetFloat("moveSpeed", moveInput.magnitude);
        animator.SetBool("onGround", canJump);
    }


    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {

            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
            UIController.instance.ammoText.text = "Ammo: " + activeGun.currentAmmo;

        }

    }
}
