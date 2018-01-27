using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour {

    RectTransform canvas;
    RectTransform UItoMove;
    Vector3 startingPosition;

    [SerializeField]
    private float speed;
    private bool isMoving = false;
    private float distanceToMove;

    [SerializeField]
    private GameObject storyUI1;
    RectTransform story1;
    private bool isStory1Moving = false;

    void Start()
    {
        UItoMove = gameObject.GetComponent<RectTransform>();
        story1 = storyUI1.GetComponent<RectTransform>();
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

        
        //transform.position = new Vector3(startingPosition.x, canvas.rect.height + UItoMove.rect.height, startingPosition.z);
    }
}
