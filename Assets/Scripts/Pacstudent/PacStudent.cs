using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudent : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;
    public int m_pacstudentState = 0;
    public static int Pacstudent_Normal = 0;
    public static int Pacstudent_Invicible = 1;
    public static int Pacstudent_Hurt = 2;
    public static int m_life = 3;
    public static int m_maxLife = 3;
    public const float m_invicibleTime = 5;
    public const float m_invicibleFlashTime = 2.5f;
    private float m_invicibleTimer = 0;
    private float m_invicibleFlashTimer = 0;

    public static int m_PacstudentMoveState = 0;
    public static int MOVE_NONE = 0;
    public static int MOVE_UP = 1;
    public static int MOVE_DOWN = 2;
    public static int MOVE_LEFT = 3;
    public static int MOVE_RIGHT = 4;
    public ParticleSystem particleSystem;

    public static bool PACSTUDENT_CANMOVE = true;

    public AudioSource audioSource;
    public AudioClip moveClip;
    public AudioClip eatClip;
    public AudioClip deathClip;
    public AudioClip collidesClip;

    // Use this for initialization
    void Start()
    {
        dest = transform.position;
        m_pacstudentState = Pacstudent_Normal;
        
    }

    // Update is called once per frame
    void Update()
    {
        particleSystem.Play();
       
        //particleSystem.stop();
        if (m_pacstudentState == Pacstudent_Invicible)
        {
            m_invicibleTimer -= Time.deltaTime;
            if (m_invicibleTimer <= m_invicibleFlashTime)
            {
                m_invicibleFlashTimer += Time.deltaTime;
                if (m_invicibleFlashTimer >= 0.5f)
                {
                    if (GetComponent<SpriteRenderer>().color == Color.red)
                    {
                        GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    m_invicibleFlashTimer = 0;
                }
            }
            if (m_invicibleTimer <= 0)
            {
                m_invicibleTimer = 0;
                ChangeState(Pacstudent_Normal);
            }
        }
        if (m_PacstudentMoveState == MOVE_UP)
        {
            PACSTUDENT_CANMOVE = valid((Vector2.up + Vector2.left * 0.3f) * 2) && valid((Vector2.up + Vector2.right * 0.3f) * 2);
        }
        else if (m_PacstudentMoveState == MOVE_RIGHT)
        {
            PACSTUDENT_CANMOVE = valid((Vector2.right + Vector2.up * 0.3f) * 2) && valid((Vector2.right + Vector2.down * 0.3f) * 2);
        }
        else if (m_PacstudentMoveState == MOVE_DOWN)
        {
            PACSTUDENT_CANMOVE = valid((Vector2.down + Vector2.left * 0.3f) * 2) && valid((Vector2.down + Vector2.right * 0.3f) * 2);
        }
        else if (m_PacstudentMoveState == MOVE_LEFT)
        {
            PACSTUDENT_CANMOVE = valid((Vector2.left + Vector2.up * 0.3f) * 2) && valid((Vector2.left + Vector2.down * 0.3f) * 2);
        }
        
    }

    void FixedUpdate()
    {
       
        if (GameManager.m_paused == true)
        {
            return;
        }
        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        transform.position = p;
        // Check for Input if not moving
        if ((Vector2)transform.position == dest)
        {
            print("press Move:"+m_PacstudentMoveState);
            if (m_PacstudentMoveState == MOVE_UP && valid((Vector2.up + Vector2.left * 0.3f) * 2) && valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2)transform.position + Vector2.up;
            if (m_PacstudentMoveState == MOVE_RIGHT && valid((Vector2.right + Vector2.up * 0.3f) * 2) && valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2)transform.position + Vector2.right;
            if (m_PacstudentMoveState == MOVE_DOWN && valid((Vector2.down + Vector2.left * 0.3f) * 2) && valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2)transform.position - Vector2.up;
            if (m_PacstudentMoveState == MOVE_LEFT && valid((Vector2.left + Vector2.up * 0.3f) * 2) && valid((Vector2.left + Vector2.down * 0.3f) * 2))
                dest = (Vector2)transform.position - Vector2.right;



            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (valid((Vector2.up + Vector2.left * 0.3f) * 2) && valid((Vector2.up + Vector2.right * 0.3f) * 2))
                {
                    dest = (Vector2)transform.position + Vector2.up;
                    PlayClip("move", 1);
                }
                else
                    PlayClip("collides", 1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (valid((Vector2.right + Vector2.up * 0.3f) * 2) && valid((Vector2.right + Vector2.down * 0.3f) * 2))
                {
                    dest = (Vector2)transform.position + Vector2.right;
                    PlayClip("move", 1);
                }
                else
                    PlayClip("collides", 1);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (valid((Vector2.down + Vector2.left * 0.3f) * 2) && valid((Vector2.down + Vector2.right * 0.3f) * 2)) { 
                    dest = (Vector2)transform.position - Vector2.up;
                    PlayClip("move", 1);
                }
                else
                    PlayClip("collides", 1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (valid((Vector2.left + Vector2.up * 0.3f) * 2) && valid((Vector2.left + Vector2.down * 0.3f) * 2))
                {
                    dest = (Vector2)transform.position - Vector2.right;

                    PlayClip("move", 1);
                }
                else
                    PlayClip("collides", 1);

            }
        }
        // Animation Parameters
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        
    }

    bool valid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);

        if (hit.collider == null)
        {
            return true;
        }
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("wall") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            return false;
        }

        return true;
    }

    public void ChangeState(int state)
    {
        m_pacstudentState = state;
        if (state == Pacstudent_Normal)
        {
            GetComponent<SpriteRenderer>().color = Color.white;

        }
        else if (state == Pacstudent_Invicible)
        {
            m_invicibleTimer += m_invicibleTime;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (state == Pacstudent_Hurt)
        {
            StartCoroutine(Hurt());

        }
    }

    IEnumerator Invicible()
    {
        yield return new WaitForSeconds(5);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        ChangeState(Pacstudent_Normal);

    }

    IEnumerator Hurt()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        ChangeState(Pacstudent_Normal);

    }

    public void PlayClip(string clip_name, float duration)
    {
        if (audioSource != null)
        {
            if(clip_name == "move")
                audioSource.clip = moveClip;
            else if (clip_name == "eat")
                audioSource.clip = eatClip;
            else if (clip_name == "death")
                audioSource.clip = deathClip;
            else if (clip_name == "collides")
                audioSource.clip = collidesClip;

            audioSource.Play(); 

            Invoke("StopClip", duration);
        }
    }

    void StopClip()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); 
        }

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag.CompareTo("Dot") == 0)
        {
            GameManager.Instant.AddScore(1);
            GameManager.Instant.EatDot();
            Destroy(collider.gameObject);
        }
        else if (collider.tag.CompareTo("Ghost") == 0)
        {
            int pacstudentState = m_pacstudentState;
            if (pacstudentState == Pacstudent_Normal)
            {
                //m_Restart.SetActive(true);
                //Destroy(collider.gameObject);
                m_life--;
                if (m_life <= 0)
                {
                    GameManager.Instant.SaveHighScore();
                    GameObject.Find("Main Camera").GetComponent<GameManager>().GameOver();
                }
                else
                {
                    PlayClip("death", 3);
                    ChangeState(Pacstudent_Hurt);
                }
            }
            else if (pacstudentState == Pacstudent_Invicible)
            {
                GameManager.Instant.AddScore(100);
                GameManager.Instant.GhostRevenge(gameObject);
            }
            else if (pacstudentState == Pacstudent_Hurt)
            {

            }

        }
     
    }

}
