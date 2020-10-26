using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class KardControl : MonoBehaviour
{
    public GameObject[] deck = new GameObject[15];
    public int _kardCol = 10;

    public bool cardGoToHand = false;//карта движется в руку?
    public int cardCounter = 0;//счетчик какая карта пойдет в руку?
    public int cardInHand = 0;

    public float speedRotate = 5.0f;
    public float speedTP = 5.0f;

    public Vector3 CPositionNo2 = new Vector3(0, 0.5f, -6.0f);
    public Quaternion CRotationNo2 = new Quaternion(0, 0, 0, 1);

    public float xCPN = 0.5f;//на какое расстояние сдвигаем карту по X, если их количество в руке четное
    public float zCPN = 0.1f;//на какое расстояние сдвигаем карту по Z, если их количество в руке четное
    public float yCRN = 5.0f;//изменяем угол наклона карты 

    public Vector3 CPositionYes2 = new Vector3(0.6f, 0.5f, -6.0f);
    public Quaternion CRotationYes2 = new Quaternion(0, 0, 0, 1);

    public float xCPY = -0.5f;//на какое расстояние сдвигаем карту по X, если их количество в руке не четное
    public float zCPY = 0.1f;//на какое расстояние сдвигаем карту по Z, если их количество в руке не четное
    public float yCRY = 5.0f;//изменяем угол наклона карты 




    [SerializeField] private GameObject T;

    private void Awake()
    {

        for (int i = 0; i <= deck.Length - 1; ++i)
        {
            deck[i] = GameObject.Find("Kard (" + (i + 1).ToString() + ")");//массив заполняется перед функцией update каждый кадр, определяет сколько машин/Player(номер) зарегистрированно на сцене и записывает их в массив

            if (deck[i] == null)
            {
                _kardCol = i;
                break;
            }
        }

        Array.Resize(ref deck, _kardCol);//меняем размер массива в соответствии с количеством карт в колоде
    }


    public float _sh = 0;
    private void FixedUpdate()
    {

        if (_sh < 2.5f && GoToHand == true)
        {
            KardInHandControl();
            _sh += 1 * Time.deltaTime;
        }
        else
        {
            _sh = 0;
            GoToHand = false;
        }
    }

    public GameObject[] KIHC = new GameObject[0];
    private void KardInHandControl()
    {

        for (int i = 0; i <= KIHC.Length - 1; ++i)//рисуем карты по новым позициям в зависимости от четности их в руке
        {
            if (KIHC[i].GetComponent<Control>().inHand)
            {
                if (cardInHand % 2 == 0)
                {
                    if (cardInHand != 0)
                    {
                        //сценарий первый (четное количество)
                        if (i == 0)
                        {
                            Debug.Log("первая карта");
                            KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionNo2.x, CPositionNo2.y - 0.1f * (i + 1), CPositionNo2.z), speedTP * Time.deltaTime);

                            //KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, new Quaternion(CRotationNo2.x, CRotationNo2.y, CRotationNo2.z, CRotationNo2.w), speedRotate * Time.deltaTime);
                            //KIHC[i].transform.LookAt(T.transform.position, KIHC[i].transform.forward);
                            Quaternion _r = Quaternion.identity;
                            _r.eulerAngles = new Vector3(0, CPositionNo2.y - yCRN * (i + 1), 0);
                            KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);
                        }
                        else
                        {
                            if (i % 2 != 0)
                            {
                                Debug.Log("четная карта №" + i.ToString());
                                KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionNo2.x + (xCPN * i), CPositionNo2.y - 0.1f * (i + 1), CPositionNo2.z + (-zCPN * i)), speedTP * Time.deltaTime);

                                //KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, new Quaternion(CRotationNo2.x, CRotationNo2.y, CRotationNo2.z, CRotationNo2.w), speedRotate * Time.deltaTime);
                                //KIHC[i].transform.LookAt(T.transform.position, KIHC[i].transform.forward);
                                Quaternion _r = Quaternion.identity;
                                _r.eulerAngles = new Vector3(0, CPositionNo2.y + yCRN * (i + 1), 0);
                                KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);
                            }
                            else
                            {
                                Debug.Log("нечетная карта №" + i.ToString());
                                KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionNo2.x + (-xCPN * (i - 1)), CPositionNo2.y - 0.1f * (i + 1), CPositionNo2.z + (-zCPN * (i - 1))), speedTP * Time.deltaTime);

                                //KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, new Quaternion(CRotationNo2.x, CRotationNo2.y, CRotationNo2.z, CRotationNo2.w), speedRotate * Time.deltaTime);
                                //KIHC[i].transform.LookAt(T.transform.position, KIHC[i].transform.forward);
                                Quaternion _r = Quaternion.identity;
                                _r.eulerAngles = new Vector3(0, CPositionNo2.y - yCRN * (i + 1), 0);
                                KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);

                            }
                        }
                    }
                }
                else
                {
                    //сценарий второй (не четное количество карт в руке)
                    if (cardInHand != 0)
                    {
                        if (i == 0)
                        {
                            Debug.Log("первая карта");
                            KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionYes2.x - 0.7f + (xCPY * (i)), CPositionYes2.y - 0.1f * (i + 1), CPositionYes2.z + (-zCPY * (i - 1))), speedTP * Time.deltaTime);

                            //KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, new Quaternion(0, 0, 0, 0), speedRotate * Time.deltaTime);
                            // KIHC[i].transform.LookAt(T.transform.position, KIHC[i].transform.forward);
                            Quaternion _r = Quaternion.identity;
                            _r.eulerAngles = new Vector3(0, 0, 0);
                            KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);
                        }
                        else
                        {
                            if (i % 2 != 0 && i != 0)
                            {// i не четная, карта четная
                                Debug.Log("четная 2 карта №" + i.ToString());
                                KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionYes2.x + (-xCPY * i), CPositionYes2.y - 0.1f * (i + 1), CPositionYes2.z + (-zCPY * i)), speedTP * Time.deltaTime);

                                //KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, new Quaternion(CRotationYes2.x, CRotationYes2.y, CRotationYes2.z, CRotationYes2.w), speedRotate * Time.deltaTime);
                                //KIHC[i].transform.LookAt(T.transform.position, KIHC[i].transform.forward);
                                Quaternion _r = Quaternion.identity;
                                _r.eulerAngles = new Vector3(0, CRotationYes2.y + yCRY * (i + 1), 0);
                                KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);
                            }
                            else
                            {// i четная, карта не четная
                                Debug.Log("не четная 2 карта №" + i.ToString());
                                KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionYes2.x - 0.7f + (xCPY * (i)), CPositionYes2.y - 0.1f * (i + 1), CPositionYes2.z + (-zCPY * (i - 1))), speedTP * Time.deltaTime);

                                //KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, new Quaternion(CRotationYes2.x, CRotationYes2.y + (yCRY * i), CRotationYes2.z, CRotationYes2.w), speedRotate * Time.deltaTime);
                                //KIHC[i].transform.LookAt(T.transform.position, KIHC[i].transform.forward);
                                Quaternion _r = Quaternion.identity;
                                _r.eulerAngles = new Vector3(0, CRotationYes2.y - yCRY * (i + 1), 0);
                                KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);
                            }
                        }
                    }
                }
            }
        }
    }

    public bool GoToHand = false;
    private void OnMouseDown()
    {
        cardCounter = 0;
        for (int i = 0; i <= deck.Length - 1; ++i)
        {
            if ((!deck[i].GetComponent<Control>().inHand) && (deck[cardCounter].transform.position.y >= deck[i].transform.position.y))
            {
                    cardCounter = i + 1;
            }
        }

        if (deck.Length > cardInHand)
        {
            cardInHand += 1;
        }

        Array.Resize(ref KIHC, cardInHand);//добавляем карту в массив карт в руке и расширяем массив

        for (int i = 0; i <= KIHC.Length - 1; ++i)//перебираем карты в руке и задаем новые позиции
        {
            if (KIHC[i] == null)
            {
                KIHC[i] = GameObject.Find("Kard (" + cardCounter.ToString() + ")");
                KIHC[i].GetComponent<Control>().inHand = true;
                KIHC[i].GetComponent<Rigidbody>().isKinematic = true;
                KIHC[i].GetComponent<Collider>().isTrigger = true;
                break;
            }
        }
        GoToHand = true;
        _sh = 0;
    }

    private void RandomKard()
    {
        for (int i = 0; i <= deck.Length - 1; ++i)
        {
            if (!deck[i].GetComponent<Control>().inHand)
            {

                float _vect = deck[i].transform.position.y + Random.Range(1.0f, 2.7f);
                deck[i].transform.position = new Vector3(-3.45f, _vect, -3.2f);
                deck[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    private void Sort()
    {
        float _vectT = 0.15f;
        for (int i = 0; i <= deck.Length - 1; ++i)
        {
            if (!deck[i].GetComponent<Control>().inHand)
            {
                _vectT += 0.25f;
                deck[i].transform.position = new Vector3(-3.45f, _vectT, -3.2f);
                deck[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }

    private void ResetCardP()
    {
        for (int i = 0; i <= KIHC.Length - 1; ++i)//перебираем карты в руке и задаем новые позиции
        {
            KIHC[i].GetComponent<Control>().inHand = false;
            KIHC[i].GetComponent<Rigidbody>().isKinematic = false;
            KIHC[i].GetComponent<Collider>().isTrigger = false;
            cardInHand = 0;
            KIHC[i] = null;
        }

        float _vectT = 0.15f;
        for (int i = 0; i <= deck.Length - 1; ++i)
        {
            if (!deck[i].GetComponent<Control>().inHand)
            {
                _vectT += 0.25f;
                deck[i].transform.position = new Vector3(-3.45f, _vectT, -3.2f);
                deck[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }

    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(40, Screen.height / 3 - 20, 100, 30), "Сортировка"))
        {
            Sort();
        }

        if (GUI.Button(new Rect(40, Screen.height / 3 + 10, 100, 30), "Перемешать"))
        {
            RandomKard();
        }

        if (GUI.Button(new Rect(40, Screen.height / 3 + 40, 100, 40), "Вернуть в колоду"))
        {
            if (KIHC[0] != null)
            {
                ResetCardP();
            }
        }
    }
}
