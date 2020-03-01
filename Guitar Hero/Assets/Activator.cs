using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note, gm;
    Color old;
    public bool CreateNote;
    public GameObject n;

    
    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gm = GameObject.Find("GameManager");
        old = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(CreateNote){
            if(Input.GetKeyDown(key))
            Instantiate(n, transform.position, Quaternion.identity);
        }
        else {
            if (Input.GetKeyDown(key))
            {
                StartCoroutine(Pressed());
            }

            if (Input.GetKeyDown(key) && active)
            {
                Destroy(note);
                AddScore();
                gm.GetComponent<GameManager>().AddStreak();
                active = false;
            }
            else if (Input.GetKeyDown(key) && !active)
            {
                gm.GetComponent<GameManager>().ResetStreak();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;

        if (collision.gameObject.tag =="Note")
        {
            note = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
        //gm.GetComponent<GameManager>().ResetStreak();
    }

    void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gm.GetComponent<GameManager>().GetScore());
    }



    IEnumerator Pressed()
    {
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}
