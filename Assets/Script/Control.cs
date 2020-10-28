using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Random = UnityEngine.Random;

public class Control : MonoBehaviour
{

    public bool inHand = false;
    public bool goOnTable = false;
    public bool onTable = false;
    public bool inKoloda = true;

    public GameObject table;
    public GameObject Koloda;



    public void Awake()
    {
        table = GameObject.Find("InvisibleT");
        Koloda = GameObject.Find("Koloda");
    }

    public void Update()
    {
       /* if (transform.position.y < table.transform.position.y)
        {
           Koloda.GetComponent<KardControl>().cardCounter -= 1;

            float _vectT = 0.15f;


            for (int i = 0; i <= Koloda.GetComponent<KardControl>().deck.Length - 1; ++i)
            {
                if (!Koloda.GetComponent<KardControl>().deck[i].GetComponent<Control>().inHand && Koloda.GetComponent<KardControl>().deck[i] == gameObject)
                {
                    _vectT += 0.25f;
                    Koloda.GetComponent<KardControl>().deck[i].transform.position = new Vector3(-3.45f, _vectT, -3.2f);
                    Koloda.GetComponent<KardControl>().deck[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                }
            }

            for (int i = 0; i <= Koloda.GetComponent<KardControl>().KIHC.Length - 1; ++i)
            {
                if (Koloda.GetComponent<KardControl>().KIHC[i] == gameObject)
                {
                    Koloda.GetComponent<KardControl>().KIHC[i] = null;
                   // Array.Sort(Koloda.GetComponent<KardControl>().KIHC);
                }

                if (Koloda.GetComponent<KardControl>().KIHC[i] == null)
                {
                    for (int j = 0; j <= Koloda.GetComponent<KardControl>().KIHC.Length - 1; ++j)
                    {
                        if (Koloda.GetComponent<KardControl>().KIHC[j] != null)
                        {
                            Koloda.GetComponent<KardControl>().KIHC[i] = Koloda.GetComponent<KardControl>().KIHC[j];
                        }
                        else
                        {
                            break;
                        }
                    }
                }


                Koloda.GetComponent<KardControl>().cardInHand -= 1;
            }

            Array.Resize(ref Koloda.GetComponent<KardControl>().KIHC, Koloda.GetComponent<KardControl>().cardInHand);//добавляем карту в массив карт в руке и расширяем массив

            goOnTable = false;
            inHand = false;
        }*/
    }


    private void OnCollisionEnter(Collision collision)
    {
        if ((table.name == collision.collider.name) && !inKoloda)
        {
            onTable = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (table.name == collision.collider.name)
        {
            onTable = false;
        }
    }

    private void OnMouseDrag()
    {
        Koloda.GetComponent<KardControl>().CardMove(gameObject);
    }


    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Koloda.GetComponent<KardControl>().CMove = false;
    }

    [SerializeField] private float highlighting = 0.5f;
    private void OnMouseExit()
    {
        if (!inHand && !goOnTable)
        {
            Vector3 _p = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - highlighting, gameObject.transform.position.z);
            gameObject.transform.localPosition = _p;
            inHand = true;
        }
    }
    
    private void OnMouseEnter()
    {
        if (inHand && !goOnTable && !Koloda.GetComponent<KardControl>().CMove)
        {
            Vector3 _p = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + highlighting, gameObject.transform.position.z);
            gameObject.transform.localPosition = _p;
            inHand = false;
        }
    }
}
