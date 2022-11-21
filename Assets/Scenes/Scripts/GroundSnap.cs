using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class GroundSnap : MonoBehaviour, IDropHandler
{
    public float x_position;

    [SerializeField] GameObject mouse;

    public int speed = 10;
    public bool dropped = false;

    public Vector2 position;
  
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            dropped = true;
      
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector2(transform.position.x+1, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(dropped)
        {
            MoveMouseToReward();
        }
    }

    public void MoveMouseToReward()
    {
        mouse.transform.position = Vector2.MoveTowards(mouse.transform.position, position, speed * Time.deltaTime);
    }
}
