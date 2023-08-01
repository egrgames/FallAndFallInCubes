using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToForce : MonoBehaviour
{
    public Camera camera;
    private string budyPartTag = "Body";
    public GameObject BudyParent;
    public float ForceOfHit = 15f;

    public GameObject HitExplosion;

    //булевое значение, чтобы ударить стикмена можно было только раз
    public bool once;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !once)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(camera.ScreenPointToRay(Input.mousePosition), Mathf.Infinity);
            foreach (RaycastHit hit in hits)
            {
                //проверяем, есть ли какая-либо часть тела там, куда тыкнул игрок
                if (hit.collider.CompareTag(budyPartTag))
                {
                    //выключаем анимацию
                    BudyParent.GetComponent<Animator>().enabled = false;
                    //даем нашей кукле упасть
                    BudyParent.GetComponent<Rigidbody>().isKinematic = false;
                    //на месте удара создаев эффект удара
                    Instantiate(HitExplosion, hit.point, Quaternion.identity);
                    //сам удар по части тела 
                    hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * ForceOfHit, ForceMode.Impulse);
                    once = true;
                }
            }
        }
    }
}
