﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb2d;
    public  Animator animator;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool run = false;
    public float BaseSpeed=100f;
    public Text BaseSpeedtxt;


    public float TargetSpeed=150;
    public Text TargetSpeedTxt;

    public float MaxRunForce=50;
    public Text MaxRunForceTxt;


    public float MinRunForce;
    public GameObject btn;

    public GameObject startbtn;



    public float FallMultiplier;
    public float LowJumpMultiplayer;
    public GameObject MainPanel;


    public void SinglePlayer()
    {
        MainPanel.gameObject.SetActive(false);
        startbtn.gameObject.SetActive(true);

    }
    public Vector3 FrontCheckOffset;
    public Vector3 BackCheckOffset;

    private void Start()
    {
        FrontCheckOffset = this.transform.position - frontCheck.transform.position;
        BackCheckOffset = this.transform.position - BackCheck.transform.position;
        //  PlayerPrefs.DeleteAll();
        if(PlayerPrefs.GetFloat("BaseSpeed")==0.0f)
        {
            PlayerPrefs.SetFloat("BaseSpeed",25f);
            BaseSpeedtxt.text = "25";
        }
        else
        {
            BaseSpeed = PlayerPrefs.GetFloat("BaseSpeed");
            BaseSpeedtxt.text = BaseSpeed.ToString();
        }


        if (PlayerPrefs.GetFloat("TargetSpeed") == 0.0f)
        {
            PlayerPrefs.SetFloat("TargetSpeed", 50f);
            TargetSpeedTxt.text = "50";
        }
        else
        {
            TargetSpeed = PlayerPrefs.GetFloat("TargetSpeed");
            TargetSpeedTxt.text = TargetSpeed.ToString();
        }

        if (PlayerPrefs.GetFloat("MaxRunForce") == 0.0f)
        {
            PlayerPrefs.SetFloat("MaxRunForce", 100f);
            MaxRunForceTxt.text = "100";
        }
        else
        {
            MaxRunForce = PlayerPrefs.GetFloat("MaxRunForce");
            MaxRunForceTxt.text = MaxRunForce.ToString();
        }


        if (PlayerPrefs.GetFloat("m_JumpForce") == 0.0f)
        {
            PlayerPrefs.SetFloat("m_JumpForce", 25f);
            GetComponent<CharacterController2D>().m_JumpForce = 25f;
            GetComponent<CharacterController2D>().m_JumpForceTxt.text = "25";
        }
        else
        {
            GetComponent<CharacterController2D>().m_JumpForce = PlayerPrefs.GetFloat("m_JumpForce");
            GetComponent<CharacterController2D>().m_JumpForceTxt.text = GetComponent<CharacterController2D>().m_JumpForce.ToString();
        }
        

        if (PlayerPrefs.GetFloat("playerGravityScale") == 0.0f)
        {
            PlayerPrefs.SetFloat("playerGravityScale", 5f);
            GetComponent<CharacterController2D>().playerGravityScaleTxt.text = "5";
            GetComponent<Rigidbody2D>().gravityScale = 5f;

        }
        else
        {
            GetComponent<CharacterController2D>().playerGravityScale = PlayerPrefs.GetFloat("playerGravityScale");
            GetComponent<CharacterController2D>().playerGravityScaleTxt.text = GetComponent<CharacterController2D>().playerGravityScale.ToString();
            GetComponent<Rigidbody2D>().gravityScale = GetComponent<CharacterController2D>().playerGravityScale;
        }


        if (PlayerPrefs.GetFloat("terminalVelocity") == 0.0f)
        {
            PlayerPrefs.SetFloat("terminalVelocity", -100f);
            GetComponent<CharacterController2D>().terminalVelocityTxt.text = "-100";
         

        }
        else
        {
            GetComponent<CharacterController2D>().terminalVelocity = PlayerPrefs.GetFloat("terminalVelocity");
            GetComponent<CharacterController2D>().terminalVelocityTxt.text = GetComponent<CharacterController2D>().terminalVelocity.ToString();
            
        }

        if (PlayerPrefs.GetFloat("m_WallJumpForce") == 0.0f)
        {
            PlayerPrefs.SetFloat("m_WallJumpForce", -50);
            GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = "-50";
        }
        else
        {
            GetComponent<CharacterController2D>().m_WallJumpForce = PlayerPrefs.GetFloat("m_WallJumpForce");
            GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();
        }

        if (PlayerPrefs.GetFloat("walljumpAmplitudeLeft") == 0.0f)
        {
            PlayerPrefs.SetFloat("walljumpAmplitudeLeft", 3);
           walljumpAmplitudeLeftTxt.text = "3";
            walljumpAmplitudeLeft = 3;
        }
        else
        {
            walljumpAmplitudeLeft = PlayerPrefs.GetFloat("walljumpAmplitudeLeft");
            walljumpAmplitudeLeftTxt.text = walljumpAmplitudeLeft.ToString();
        }

        if (PlayerPrefs.GetFloat("walljumpAmplitudeRight") == 0.0f)
        {
            PlayerPrefs.SetFloat("walljumpAmplitudeRight", 40);
            walljumpAmplitudeRightTxt.text = "40";
            walljumpAmplitudeRight = 40;

        }
        else
        {
            walljumpAmplitudeRight = PlayerPrefs.GetFloat("walljumpAmplitudeRight");
            walljumpAmplitudeRightTxt.text = walljumpAmplitudeRight.ToString();
        }

        if (PlayerPrefs.GetFloat("walljumpForceLeft") == 0.0f)
        {
            PlayerPrefs.SetFloat("walljumpForceLeft", 12.5f);
            walljumpForceLeftTxt.text = "12.5";
            walljumpForceLeft = 12.5f;

        }
        else
        {
            walljumpForceLeft = PlayerPrefs.GetFloat("walljumpForceLeft");
            walljumpForceLeftTxt.text = walljumpForceLeft.ToString();
        }

        if (PlayerPrefs.GetFloat("walljumpForceRight") == 0.0f)
        {
            PlayerPrefs.SetFloat("walljumpForceRight", 40);
            walljumpForceRightTxt.text = "40";
        }
        else
        {
            walljumpForceRight = PlayerPrefs.GetFloat("walljumpForceRight");
            walljumpForceRightTxt.text = walljumpForceRight.ToString();
        }


        if (PlayerPrefs.GetFloat("WallSlideGravity") == 0.0f)
        {
            PlayerPrefs.SetFloat("WallSlideGravity", 10f);

            WallSlideGravityTxt.text = "10f";
          //  UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            WallSlideGravity = PlayerPrefs.GetFloat("WallSlideGravity");
            WallSlideGravityTxt.text = WallSlideGravity.ToString();
        }

    }

    // Update is called once per frame
    public void WallJumpForceFunc(string num)
    {
        GetComponent<CharacterController2D>().m_WallJumpForce = float.Parse(num);
        PlayerPrefs.SetFloat("m_WallJumpForce", GetComponent<CharacterController2D>().m_WallJumpForce);
        GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    public void BaseSpeedFunc(string num)
    {
        BaseSpeed =float.Parse( num);
        PlayerPrefs.SetFloat("BaseSpeed", BaseSpeed);
        BaseSpeedtxt.text = BaseSpeed.ToString();


    }
    public void TargetSpeedFunc(string num)
    {
        TargetSpeed = float.Parse(num);
        PlayerPrefs.SetFloat("TargetSpeed", TargetSpeed);
        TargetSpeedTxt.text = TargetSpeed.ToString();
    }
    public void MaxRunForceFunc(string num)
    {
        MaxRunForce = float.Parse(num);

        PlayerPrefs.SetFloat("MaxRunForce", MaxRunForce);
        MaxRunForceTxt.text = MaxRunForce.ToString();
    }
    public void MinRunForceFunc(string num)
    {
        MinRunForce = float.Parse(num);
    }
    public void JumpSpeed(string num)
    {
        GetComponent<CharacterController2D>().m_JumpForce = float.Parse(num);
        PlayerPrefs.SetFloat("m_JumpForce", GetComponent<CharacterController2D>().m_JumpForce);
        GetComponent<CharacterController2D>().m_JumpForceTxt.text = GetComponent<CharacterController2D>().m_JumpForce.ToString();


    }
    public void TerminalFallSpeed(string num)
    {
        GetComponent<CharacterController2D>().terminalVelocity = float.Parse(num);
        PlayerPrefs.SetFloat("terminalVelocity", GetComponent<CharacterController2D>().terminalVelocity);
        GetComponent<CharacterController2D>().terminalVelocityTxt.text = GetComponent<CharacterController2D>().terminalVelocity.ToString();
    }
    public void PlayerWeight(string num)
    {
      //  GetComponent<Rigidbody2D>().gravityScale = float.Parse(num);
      GetComponent<CharacterController2D>().playerGravityScale = float.Parse(num);
        GetComponent<Rigidbody2D>().gravityScale = GetComponent<CharacterController2D>().playerGravityScale;
        PlayerPrefs.SetFloat("playerGravityScale", GetComponent<CharacterController2D>().playerGravityScale);
        GetComponent<CharacterController2D>().playerGravityScaleTxt.text = GetComponent<CharacterController2D>().playerGravityScale.ToString();
    }

    public void JumpSPeedReductiom(string num)
    {
        GetComponent<CharacterController2D>().RunSpeedReduction = float.Parse(num);
    }

    public void Startx()
    {
        runSpeed = 10;
        MinRunForce = MaxRunForce / 10.0f;
   

        run = true;
        btn.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.touchCount == 0)
        {
            DOTouchCount = true;
        }
        frontCheck.transform.position = this.transform.position - FrontCheckOffset;
        BackCheck.transform.position = this.transform.position - BackCheckOffset;
          
        if (run)
        {
          //  runSpeed = Mathf.Lerp(100, 25, Time.deltaTime * 20f);
          if(runSpeed> BaseSpeed && runSpeed < TargetSpeed)
            {
                runSpeed += Time.deltaTime * MinRunForce;
            }

            if (runSpeed < BaseSpeed && runSpeed > 0f)
            {
                runSpeed += Time.deltaTime * MaxRunForce;
              //  Debug.Log(runSpeed);
            }

        }
        else
        {
            runSpeed = 0;
        }
        horizontalMove = runSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && runSpeed == 0)
        {
            runSpeed = 10;
            run = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
         //   rb2d.AddTorque(1000.0f, ForceMode2D.Impulse);
         //   rb2d.AddForce(Vector2.right * 100.0f, ForceMode2D.Impulse);

            //animator.SetTrigger("2To4");
        }

        if (Input.touchCount > 1 || Input.GetKey(KeyCode.DownArrow))
        {
            print("down");
            crouch = true;
            animator.SetTrigger("2To4");

        }
        else
        {
            
        }
        if((Input.touchCount > 0 &&  Input.touchCount < 2) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            print("down");

            animator.ResetTrigger("2To4");
            animator.SetTrigger("4To2");
            crouch = false;

        }

        if (Input.GetMouseButton(0) )
        {
            jump = true;
            //animator.SetTrigger("Jump");
        }
        else
        {
            jump = false;
        }
    }
    public Transform wallCheckPoint;
    public LayerMask WallLayer;
    public Collider2D[] res;
    public int num;
    public bool WallJumpActive;
    public float walljumpAmplitudeLeft;
    public Text walljumpAmplitudeLeftTxt;
    public float walljumpAmplitudeRight;
    public Text walljumpAmplitudeRightTxt;

    public float walljumpForceLeft;
    public Text walljumpForceLeftTxt;

    public float walljumpForceRight;
    public Text walljumpForceRightTxt;

    public float WallSlideGravity;
    public Text WallSlideGravityTxt;

    public GameObject frontCheck;
    public GameObject BackCheck;
    public bool DOTouchCount;


    public void WallJumpAmpLeftFunc(string num)
    {
       walljumpAmplitudeLeft = float.Parse(num);
        walljumpAmplitudeLeftTxt.text = walljumpAmplitudeLeft.ToString();

         PlayerPrefs.SetFloat("walljumpAmplitudeLeft", walljumpAmplitudeLeft);
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    public void WallJumpforceLeftFunc(string num)
    {
        walljumpForceLeft = float.Parse(num);
        walljumpForceLeftTxt.text = walljumpForceLeft.ToString();

        PlayerPrefs.SetFloat("walljumpForceLeft", walljumpForceLeft);
        // PlayerPrefs.SetFloat("m_WallJumpForce", GetComponent<CharacterController2D>().m_WallJumpForce);
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    public void WallJumpAmpRightFunc(string num)
    {
        walljumpAmplitudeRight = float.Parse(num);

        walljumpAmplitudeRightTxt.text = walljumpAmplitudeRight.ToString();

        PlayerPrefs.SetFloat("walljumpAmplitudeRight", walljumpAmplitudeRight);
        // PlayerPrefs.SetFloat("m_WallJumpForce", GetComponent<CharacterController2D>().m_WallJumpForce);
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    public void WallJumpforceRightFunc(string num)
    {
        walljumpForceRight = float.Parse(num);
        walljumpForceRightTxt.text = walljumpForceRight.ToString();

        PlayerPrefs.SetFloat("walljumpForceRight", walljumpForceRight);
        // PlayerPrefs.SetFloat("m_WallJumpForce", GetComponent<CharacterController2D>().m_WallJumpForce);
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    public void WallSlideGravityFunc(string num)
    {
        WallSlideGravity = float.Parse(num);
        WallSlideGravityTxt.text = WallSlideGravity.ToString();

        PlayerPrefs.SetFloat("WallSlideGravity", WallSlideGravity);
        // PlayerPrefs.SetFloat("m_WallJumpForce", GetComponent<CharacterController2D>().m_WallJumpForce);
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    IEnumerator WallJumpRoutine()
    {
        WallJumpActive = true;
        yield return null;
      //  yield return new WaitForSeconds(0.5f);
        WallJumpActive = false;

    }
    public bool NormalMove;

    public bool straigtJump;
    private void FixedUpdate()
    {
      
            res = Physics2D.OverlapCircleAll(wallCheckPoint.transform.position, 0.15f, WallLayer);

        if (GetComponent<CharacterController2D>().m_Grounded == false && res.Length == 0)
        {
              if(NormalMove==false)
            {
                    controller.Move(horizontalMove * Time.deltaTime, crouch, false);

            }

        }else
        {
            controller.Move(horizontalMove * Time.deltaTime, crouch, false);

        }

        if (!GetComponent<CharacterController2D>().m_Grounded && res.Length != 0)
        {
            if(straigtJump)
            {
              //  print("ssss");
                StartCoroutine(WallJumpRoutine());

                straigtJump = false;
            }
        }
        if (GetComponent<CharacterController2D>().m_Grounded )
        {
            if(straigtJump==false)
            {
                straigtJump = true;
            }
            if (this.GetComponent<SpriteRenderer>().flipX)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;

            }
            if (res.Length==0)
            {
                animator.SetBool("wallslide", false);

                if (Input.touchCount==1 && !MainPanel.activeInHierarchy && !btn.activeInHierarchy || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (DOTouchCount)
                    {
                            controller.Move(0 * Time.deltaTime, crouch, true);
                        DOTouchCount = false;
                    }
                   // controller.Move(horizontalMove * Time.deltaTime, crouch, true);

                }else
                {
                    if(wallCheckPoint!=frontCheck)
                    {
                        wallCheckPoint = frontCheck.transform;
                    }
               //     controller.Move(horizontalMove * Time.deltaTime, crouch, false);
                    if (NormalMove == true)
                    {
                        NormalMove = false;
                    }
                }


              }else
            {
                
                if (Input.touchCount == 1 || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (DOTouchCount)
                    {
                            if(NormalMove==false)
                             {
                            print("as");

                            runSpeed = BaseSpeed-1;
                            NormalMove = true;

                             }
                        controller.Move(0 * Time.deltaTime, crouch, true);
                                 //  StartCoroutine(WallJumpRoutine());
                                    print("jump");
                                   DOTouchCount = false;
                    }
                }

                }

        }
        else if (GetComponent<CharacterController2D>().m_Grounded == false && res.Length != 0)
        {
            if (NormalMove == false)
            {
                print("as");
                runSpeed = BaseSpeed-1;
                NormalMove = true;

            }

            if (Input.touchCount == 1 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (DOTouchCount)
                {
                    if (WallJumpActive == false)
                {
                    if (wallCheckPoint.gameObject==BackCheck)
                    {

                        if (WallJumpActiveBool == false)
                        {
                           // if (res[0].GetComponent<WallDirectionCheck>().Flip == false)
                                this.GetComponent<SpriteRenderer>().flipX = false;
                            print("walljump1" + res[0]);
                            BoxCollider2D temp = res[0].GetComponent<BoxCollider2D>();

                                StartCoroutine(WallJumpRoutine());

                            StartCoroutine(Walljumpactivate(true, temp));
                            WallJumpActiveBool = true;

                        }

                    }
                    else if(wallCheckPoint.gameObject == frontCheck)
                    {
                        if (WallJumpActiveBool == false)
                        {
                         //   if (res[0].GetComponent<WallDirectionCheck>().Flip == false)
                                this.GetComponent<SpriteRenderer>().flipX = true;

                            BoxCollider2D temp = res[0].GetComponent<BoxCollider2D>();


                                StartCoroutine(Walljumpactivate(false, temp));
                            StartCoroutine(WallJumpRoutine());

                            print("walljump2" + res[0]);
                            WallJumpActiveBool = true;
                        }


                    }
                }
            }
            }

        }
        if (GetComponent<CharacterController2D>().m_Grounded == false && res.Length != 0)
        {

            if (rb2d.velocity.y < 0)
            {
                //  print(GetComponent<CharacterController2D>().m_Grounded);
                animator.SetBool("wallslide",true);
                rb2d.velocity = new Vector2(rb2d.velocity.x, -WallSlideGravity);
                //  rb2d.velocity = new Vector2(rb2d.velocity.x, GetComponent<CharacterController2D>().terminalVelocity);
            }else
            {
                animator.SetBool("wallslide", false);

            }

        }else if(GetComponent<CharacterController2D>().m_Grounded == false && res.Length == 0)
        {
            animator.SetBool("wallslide", false);

        }
        else if (GetComponent<CharacterController2D>().m_Grounded == true && res.Length != 0)
        {
            animator.SetBool("wallslide", false);

        }



        jump = false;
    }
    IEnumerator coliderOff(BoxCollider2D temp)
    {
        
      //  Debug.LogError(temp.enabled);
        yield return new WaitForSeconds(0.2f);
        if (temp.GetComponent<BoxCollider2D>())
        {
            temp.enabled = true;
        }
        WallJumpActiveBool = false;


    }
    IEnumerator lateFlip(Vector3 temp)
    {
       // yield return new WaitForSeconds(0.3f);
        //while(res.Length!=0)
        //{
        //    yield return null;
        //}
        yield return new WaitForSeconds(0.3f);
        this.transform.eulerAngles = temp;

    }
    public bool WallJumpActiveBool;
    IEnumerator Walljumpactivate(bool front, BoxCollider2D temp)
    {
        if(temp.GetComponent<BoxCollider2D>())
        {
            temp.enabled = false;

        }
        yield return null;
        if (front)
        {
            transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
            Debug.LogError("i");
            rb2d.velocity = new Vector2(0, 0);
            rb2d.angularVelocity = 0f;
            rb2d.AddForce(new Vector2((walljumpForceLeft * 2.5f * 1000f * Time.deltaTime) , (GetComponent<CharacterController2D>().m_JumpForce * walljumpAmplitudeLeft * 1000f * Time.deltaTime)));
            
            wallCheckPoint = frontCheck.transform;

        }
        else
        {
            transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
            rb2d.velocity = new Vector2(0, 0);
            rb2d.angularVelocity = 0f;
            rb2d.AddForce(new Vector2((-walljumpForceLeft * 1000f * 2.5f * Time.deltaTime), (GetComponent<CharacterController2D>().m_JumpForce * walljumpAmplitudeLeft * 1000f * Time.deltaTime)));
            
            wallCheckPoint = BackCheck.transform;

        }
        yield return null;
        StartCoroutine(coliderOff(temp));

    }
    public void Crouch()
    {
        crouch = true;
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void ReloadApp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
