using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D controller;
    Animator animator;
    float horizontalMovement;

    [SerializeField] float speed;
    ParticleSystem dust;
    AudioManagerCS audio_;
    IKManager2D ik;

    public GameObject crossHair; // Made public for drag-and-drop
    public GameObject gunCanvas;
    public GameObject gun; // Made public for drag-and-drop
    public Transform gunPoint; // Made public for drag-and-drop

    public GameObject effectorTarget;
    bool canMove = true;
    [SerializeField] GameObject bulletDustPs;
    [SerializeField] GameObject bulletPrefab;
    CapsuleCollider2D collider;

    [SerializeField] Joystick movementJoystick; // Movement joystick
    public Joystick aimJoystick; // Aiming and shooting joystick

    // New variables for firing rate control
    [SerializeField] float fireRate = 0.5f; // Time between shots in seconds
    float lastFireTime; // The last time the player fired

    private Gun currentGun;
    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        dust = gameObject.transform.Find("DustPS").GetComponent<ParticleSystem>();
        audio_ = FindObjectOfType<AudioManagerCS>();
        ik = GetComponent<IKManager2D>();

        collider = GetComponent<CapsuleCollider2D>();
        PlayerPrefs.SetInt("PlayerHasGun", 1); // Use 0 if the player does not have a gun
        PlayerPrefs.Save(); // Save the changes
        
         currentGun = gun.GetComponent<Gun>(); // Retrieve the Gun component on current gun
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("PlayerHasGun") == 0)
        {
            gun.SetActive(false);
            gunCanvas.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("PlayerHasGun") == 1)
        {
            gun.SetActive(true);
            gunCanvas.SetActive(true);
        }
    }

    void Update()
    {
        horizontalMovement = movementJoystick.Horizontal;
      
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));

        CheckSurroundings();
        Jump();
        Aim();
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMovement * speed * Time.deltaTime, false, jump);
    }

    public void CreateDust()
    {
        if(dust == null)
        {
            Debug.Log("Dust particle not found in child of player gameobject");
            return;
        }
        dust.Play();
    }

    public void PlayStepSound()
    {
        int index = Random.Range(1, 4);
        FindObjectOfType<AudioManagerCS>().PlayOneShot("footstep" + index);
    }

    [HideInInspector] public bool aiming = false;

    Vector2 direction;
    void Aim()
    {
        float horizontal = aimJoystick.Horizontal;
        float vertical = aimJoystick.Vertical;

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            direction = new Vector2(horizontal, vertical).normalized;
            aiming = true;
            SetIkWeight(1);
            crossHair.SetActive(true);

            if ((direction.x < 0 && controller.m_FacingRight) || (direction.x > 0 && !controller.m_FacingRight))
            {
                controller.Flip_Aim();
            }

            if (currentGun != null && Time.time > lastFireTime + currentGun.fireRate)
            {
                if (Mathf.Abs(aimJoystick.Horizontal) > 0.7f || Mathf.Abs(aimJoystick.Vertical) > 0.7f)
                {
                    Instantiate(bulletDustPs, gunPoint.position, gunPoint.rotation);
                    SpawnBullet();
                    audio_.PlayOneShot("Fire");
                    lastFireTime = Time.time;
                }
            }

            return;
        }

        aiming = false;
        SetIkWeight(0);
        crossHair.SetActive(false);
    }



    public void SetCurrentGun(Gun selectedGun)
    {
        currentGun = selectedGun;
    }
    
    bool jump = false;

    void Jump()
    {
        if (movementJoystick.Vertical > 0.9f)
        {
            if (!controller.m_Grounded)
            {
                return;
            }

            animator.SetTrigger("takeOff");
            jump = true;
        }
    }

    public void OnLand()
    {
        jump = false;
        controller.m_Grounded = true;
    }

    public void MoveStateOFF()
    {
        speed = 0;
    }

    public void MoveStateOn()
    {
        speed = 70;
    }

    public void JumpGruntSound()
    {
        FindObjectOfType<AudioManagerCS>().Play("jumpGrunt");
    }

    public void LandSound()
    {
        CreateDust();
        FindObjectOfType<AudioManagerCS>().Play("footstep2");
    }

    public void SetIkWeight(float weight)
    {
        ik.weight = weight;
    }

    public void SpawnBullet()
    {
        Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
    }

    public void CheckSurroundings()
    {
        if (controller.m_Grounded)
        {
            jump = false;
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
    }
}

