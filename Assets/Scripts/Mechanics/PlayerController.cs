using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using TMPro;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public AudioClip fireShoot;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        // Coin Manager variables :
        // coinCount : Counter set as static for keeping value all along game 
        // coinText : UI Text for coinCount
        public static int coinCount = 10;
        // Using TextMeshPro instead of Text : call to library is TMPro and type is TextMeshProUGUI
        public static TextMeshProUGUI coinText;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        GamepadInput GamepadInputComponent;

        private bool m_FacingRight = true;
    
        public Transform FirePoint;
        public GameObject BulletPrefab;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            GamepadInputComponent = FindObjectOfType<GamepadInput>();

            //Assign specific game object ("Coins") 
            coinText = GameObject.Find("Coins").GetComponent<TextMeshProUGUI>();
        }

        protected override void Update()
        {
            //if (controlEnabled)
            //{
            //    move.x = Input.GetAxis("Horizontal");
            //    if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
            //        jumpState = JumpState.PrepareToJump;
            //    else if (Input.GetButtonUp("Jump"))
            //    {
            //        stopJump = true;
            //        Schedule<PlayerStopJump>().player = this;
            //    }
            //}
            //else
            //{
            //    move.x = 0;
            //}
            //UpdateJumpState();
            //base.Update();

            GamepadController();

            coinText.text = coinCount.ToString();
        }

        void GamepadController()
        {
            // References for the GamePadInputComponent
            if (GamepadInputComponent)
            {
                move.x = GamepadInputComponent.LeftAnalogVector2.x;

                if (move.x > 0.1f && !m_FacingRight)
                {
                    //spriteRenderer.flipX = false;
                    flip();
                }
                else if (move.x < -0.1f && m_FacingRight)
                {
                    //spriteRenderer.flipX = true;
                    flip();
                }

                if (jumpState == JumpState.Grounded && GamepadInputComponent.onButtonHold["Jump"] == true)
                    jumpState = JumpState.PrepareToJump;
                else if (GamepadInputComponent.onButtonHold["Jump"] == false)
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }

                if (GamepadInputComponent.onButtonDown["Fire"]== true)
                {
                    Shoot();
                }
            }
            else
            {
                // The player is in an idle state
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();            
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        // Flip the player for correcting the fire point
        private void flip(){
            m_FacingRight = !m_FacingRight;

            transform.Rotate(0f,180f,0f);
        }

        void Shoot()
        {
            // shooting logic
            // Tokens are used as bullets : check if player has sufficient items before firing
            if(coinCount > 0){
                GameObject cloneBullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
                audioSource.PlayOneShot(fireShoot);
                // Bullet disappears after lifetime ends
                Destroy(cloneBullet, 0.7f);
                // Decrease tokens counter
                coinCount--;
            }
        }

        // Counter system for score
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Token"))
            {
                coinCount++;
            }
        }
    }
}