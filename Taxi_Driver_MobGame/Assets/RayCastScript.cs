using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit2D raycastHit2D;
    Transform clickObject;

    DialougeManage speak;

    public void Start()
    {
        speak = GetComponent<DialougeManage>();
    }
    
        void Update()
        {
            mousePosition = Input.mousePosition;

            Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                raycastHit2D = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
                clickObject = raycastHit2D ? raycastHit2D.collider.transform : null;

                if (clickObject)
                {
                //clickObject.GetComponent<SpriteRenderer>().color = Color.red;
                speak.AdvanceStory();
                }
            }
        }
    


}
