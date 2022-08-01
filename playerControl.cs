using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playerControl : MonoBehaviour
{
  
    public Rigidbody2D rb;
    public float speed;
    public Animator animator;
    public bool yerdeMi;
    public Transform yerKontrol;
    public float yaricap;
    public LayerMask yer;
    public float ziplamaGucu;
    public bool ciftZipla;
    public bool cZAnm;
    public bool saldiri;
    public float saldiriSuresi;
    public float sureGecisi;
    public CircleCollider2D saldirCollider;
    public Text altin;
    public int altinPuan;
    public Image anahtar;
    public bool kapiAnahtari1;
    public bool kapiAnahtari2;
    public bool kapiAnahtari3;
    public int yildizSayisi;
    public GameObject yildizPrefab;
    public GameObject efekt;
    public bool yildiz;
    public Text yildizSayisTexti;
    public bool sagSol;
    public float can;
    public float maxCan=100f;
    public Image canBoyutu;
   
    public bool duvarZipla;
    public Text levelText;
    public GameObject oyunBitti;
    // Start is called before the first frame update
    void Start()
    {
        levelText.enabled = false;
        saldirCollider.enabled = false;
        anahtar.enabled = false;
        kapiAnahtari1 = false;
        kapiAnahtari2 = false;
        kapiAnahtari3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        yerdeMi = Physics2D.OverlapCircle(yerKontrol.position, yaricap,yer);
        //float moveInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("grounded", yerdeMi);
        animator.SetBool("ciftZipla", cZAnm);
        animator.SetBool("saldiri", saldiri);
        animator.SetBool("yildiz", yildiz);

        altin.text = altinPuan.ToString();
        yildizSayisTexti.text = yildizSayisi.ToString();

        canBoyutu.fillAmount = can / maxCan;
        if (yerdeMi)
        {
            cZAnm = false;
        }

      

        if (can <= 0)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (yerdeMi || duvarZipla)
            {
                rb.velocity = Vector2.up * ziplamaGucu;
                ciftZipla = true;
                cZAnm = false;
            }
            else
            {
                if (ciftZipla == true)
                {
                    ciftZipla = false;
                    rb.velocity = Vector2.up * ziplamaGucu;
                    cZAnm = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !saldiri)
        {
            saldiri = true;
            saldiriSuresi = sureGecisi;
        }

        if (saldiri)
        {
            saldirCollider.enabled = true;
            if (saldiriSuresi > 0)
            {
                saldiriSuresi -= Time.deltaTime;
            }
            else
            {
                saldiri = false;
            }
        }
        else
        {
            saldirCollider.enabled = false;
        }

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    audioSource.Play();
        //    yildiz = true;
        //    saldiriSuresi = sureGecisi;
        //    if (yildizSayisi > 0)
        //    {
        //        Instantiate(yildizPrefab, transform.position, Quaternion.identity);
        //        yildizSayisi--;
        //    }
        //}

        //if (yildiz)
        //{

        //    if (saldiriSuresi > 0)
        //    {
        //        saldiriSuresi -= Time.deltaTime;
        //    }
        //}
        //else
        //{
        //    yildiz = false;
        //}

      
        if (rb.velocity.x > 0.1)
        {
            sagSol = true;
            transform.localScale = new Vector2(1, 1);
        }
        if (rb.velocity.x < -0.1)
        {
            transform.localScale = new Vector2(-1, 1);
            sagSol = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("duvar"))
        {
            duvarZipla = true;
        }

        if (col.gameObject.tag.Equals("kapi") && kapiAnahtari1)
        {
            levelText.enabled = true;
            FindObjectOfType<Ses>().Play("son");
            FindObjectOfType<Ses>().Play("levelBitti");
            FindObjectOfType<Ses>().Dur("Arkaplan");
            oyunBitti.SetActive(true);
            //Application.LoadLevel("level");
            //Debug.Log(Application.loadedLevelName);
            //int sonrakiLevel = int.Parse(Application.loadedLevelName) + 1;
            //PlayerPrefs.SetInt(sonrakiLevel.ToString(), 1);
        }

        if (col.gameObject.tag.Equals("kapi1") && kapiAnahtari1)
        {
            levelText.enabled = false;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.CompareTag("altin"))
        {
            FindObjectOfType<Ses>().Play("altin");
            Destroy(other.gameObject);
            altinPuan++;
        }

        if (other.CompareTag("anahtar1"))
        {
            Destroy(other.gameObject);
            anahtar.enabled = true;
            kapiAnahtari1 = true;
        }

        if (other.CompareTag("anahtar2"))
        {
            Destroy(other.gameObject);
            anahtar.enabled = true;
            kapiAnahtari2 = true;
        }

        if (other.CompareTag("anahtar3"))
        {
            Destroy(other.gameObject);
            anahtar.enabled = true;
            kapiAnahtari3 = true;
        }

        if (other.CompareTag("yildiz"))
        {
            yildizSayisi += 15;
           
                Destroy(other.gameObject);

           
        }

        if (other.CompareTag("dusman"))
        {
            can -=5;
        }

        if (can <= 0)
        {
          FindObjectOfType<Ses>().Play("olum");
          FindObjectOfType<Ses>().Dur("Arkaplan");
            Destroy(gameObject);
            Instantiate(efekt, transform.position, Quaternion.identity);
            Instantiate(altin, transform.position, Quaternion.identity);
          

        }
    }

    public void YildizFirlat()
    {
        FindObjectOfType<Ses>().Play("yildizFirlat");
        yildiz = true;
  saldiriSuresi = sureGecisi;
   if (yildizSayisi > 0)
  {
       Instantiate(yildizPrefab, transform.position, Quaternion.identity);
       yildizSayisi--;
   }
        if (yildiz)
        {

            if (saldiriSuresi > 0)
            {
                saldiriSuresi -= Time.deltaTime;
            }
        }
        else
        {
            yildiz = false;
        }
    }

    public void SolaGit()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    public void SagaGit()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public void Zipla()
    {
        if (yerdeMi)
        {
            FindObjectOfType<Ses>().Play("zipla");
            rb.velocity = Vector2.up * ziplamaGucu;
            ciftZipla = true;
             cZAnm = false;
        }
        else
        {
            FindObjectOfType<Ses>().Play("zipla");
            if (ciftZipla == true)
                {
                    ciftZipla = false;
                    rb.velocity = Vector2.up * ziplamaGucu;
                    cZAnm = true;
                }
            }
        }

    }

