using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 temp;
    private SpriteRenderer sprite;
    public static bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        hit = false;
        Destroy(gameObject, 2.5f);
    }

    private void OnMouseDown()
    {
        GameControl.score += 10;
        GameControl.targetsHit += 1;
        hit = true;
        if (gameObject.transform.localScale.x<1)
        {

        }
        FindObjectOfType<AudioManager>().Play("break");
        Destroy(gameObject);

    }
    private void Update()
    {
        temp = transform.localScale;

        temp.x += Time.deltaTime;
        temp.y += Time.deltaTime;
        temp.z += Time.deltaTime;

        if (temp.x > 1.25)
        {
            // Change the 'color' property of the 'Sprite Renderer'
            sprite.color = new Color(1, 0, 0, 1);
        }

        if (temp.x > 2)
        {
            Destroy(gameObject);
        }

        transform.localScale = temp;
    }

}