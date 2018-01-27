using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour {

    /*
     * TO:DO
     * > use something other than update to move storyboards
     * > possibility to skip with a mouse click
     * > make storyboards stop moving after a while
     */

    RectTransform canvas;
    RectTransform UItoMove;
    Vector3 startingPosition;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float speed;
    private bool isMoving = false;
    private float distanceToMove;

    #region Story UI variables
    [SerializeField]
    private GameObject storyUI1;
    RectTransform story1;
    private bool isStory1Moving = false;

    [SerializeField]
    private GameObject storyUI2;
    RectTransform story2;
    private bool isStory2Moving = false;

    [SerializeField]
    private GameObject storyUI3;
    RectTransform story3;
    private bool isStory3Moving = false;

    [SerializeField]
    private GameObject storyUI4;
    RectTransform story4;
    private bool isStory4Moving = false;

    [SerializeField]
    private GameObject storyUI5;
    RectTransform story5;
    private bool isStory5Moving = false;

    [SerializeField]
    private GameObject storyUI6;
    RectTransform story6;
    private bool isStory6Moving = false;
    #endregion

    void Start()
    {
        UItoMove = gameObject.GetComponent<RectTransform>();
        story1 = storyUI1.GetComponent<RectTransform>();
        story2 = storyUI2.GetComponent<RectTransform>();
        story3 = storyUI3.GetComponent<RectTransform>();
        story4 = storyUI4.GetComponent<RectTransform>();
        story5 = storyUI5.GetComponent<RectTransform>();
        story6 = storyUI6.GetComponent<RectTransform>();

        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();

        startingPosition = transform.position;
        distanceToMove = -UItoMove.rect.height * 5;
        speed = -10f;
    }

    public void StartMove()
    {
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(0f, speed, 0f);
        }

        if (UItoMove.position.y < distanceToMove)
        {
            isMoving = false;
            isStory1Moving = true;
        }

        if (isStory1Moving)
        {
            story1.transform.Translate(speed, 0f, 0f);
            
        }

        if (story1.position.x < distanceToMove)
        {
            isStory1Moving = false;
            isStory2Moving = true;
        }

        if (isStory2Moving)
        {
            story2.transform.Translate(speed, 0f, 0f);
        }

        if (story2.position.x < distanceToMove)
        {
            isStory2Moving = false;
            isStory3Moving = true;
        }

        if (isStory3Moving)
        {
            story3.transform.Translate(speed * 2, 0f, 0f);
        }

        if (story3.position.x < distanceToMove)
        {
            isStory3Moving = false;
            isStory4Moving = true;
        }

        if (isStory4Moving)
        {
            story4.transform.Translate(speed * 2, 0f, 0f);
        }

        if (story4.position.x < distanceToMove)
        {
            isStory4Moving = false;
            isStory5Moving = true;
        }

        if (isStory5Moving)
        {
            story5.transform.Translate(speed * 2, 0f, 0f);
        }

        if (story5.position.x < distanceToMove)
        {
            isStory5Moving = false;
            isStory6Moving = true;
        }

        if (isStory6Moving)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            storyUI6.SetActive(false);
            player.SetActive(true);
            isStory6Moving = false;
        }

        /*if (story6.position.y > distanceToMove)
        {
            
        }*/

    }
}
