using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class KardControl : MonoBehaviour
{
    public GameObject[] deck = new GameObject[15];
    public int _kardCol = 10;//в Awake определяем сколько всего карт выложено в колоду

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

    private void Awake()// заполняем массив колоды картами на сцене
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


    public float _sh = 0;// простой счетчик - костыль
    private void FixedUpdate()
    {

        if (_sh < 2.5f && GoToHand == true)//что бы функция контролирующая позиции карт не использовалась бесконечно
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
                    if (i == 0)
                    {
                        KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionYes2.x - 0.7f + (xCPY * (i)), CPositionYes2.y - 0.1f * (i + 1), CPositionYes2.z + (-zCPY * (i - 1))), speedTP * Time.deltaTime);

                        Quaternion _r = Quaternion.identity;
                        _r.eulerAngles = new Vector3(0, 0, 0);
                        KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);
                    }
                    else
                    {
                        KIHC[i].transform.position = Vector3.Lerp(KIHC[i].transform.position, new Vector3(CPositionYes2.x + (-xCPY * i), CPositionYes2.y - 0.1f * (i + 1), CPositionYes2.z + (-zCPY * i)), speedTP * Time.deltaTime);

                        Quaternion _r = Quaternion.identity;
                        _r.eulerAngles = new Vector3(0, CRotationYes2.y + yCRY * (i + 1), 0);
                        KIHC[i].transform.rotation = Quaternion.Lerp(KIHC[i].transform.rotation, _r, speedRotate * Time.deltaTime);
                    }
            }
        }







    /*        for (int i = 0; i <= KIHC.Length - 1; ++i)//рисуем карты по новым позициям в зависимости от четности их в руке
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
        }*/
    }

    public bool GoToHand = false;
    private void OnMouseDown() //если нажали на колоду
    {
        bool _addKIHC = false;
        cardCounter = 0;

        for (int i = 0; i <= deck.Length - 1; ++i)
        {
            if (deck[i].GetComponent<Control>().inKoloda)
            {
                Debug.Log("карта из колоды № - " + (i + 1).ToString());
                if (deck[i].transform.position.y > deck[cardCounter].transform.position.y)
                {
                    Debug.Log("КАРТА №" + (i + 1).ToString() + " выше всех остальных");
                    cardCounter = i;
                    _addKIHC = true;
                }
                else
                {
                    if (deck[i].transform.position.y == deck[cardCounter].transform.position.y)
                    {
                        Debug.Log("КАРТА №" + (i + 1).ToString() + " сравнивается сама с собой");
                        cardCounter = i;
                        _addKIHC = true;
                    }
                }
            }
        }

        Debug.Log("Прошли сортировку /");
        if (_kardCol >= cardInHand && _addKIHC)
        {
            if (cardInHand != 0)
            {
                Debug.Log("Если cardinHand != 0");
                if (KIHC[cardInHand - 1] != null)
                {
                    cardInHand += 1;
                    deck[cardCounter].GetComponent<Control>().inKoloda = false;
                    deck[cardCounter].GetComponent<Control>().inHand = true;

                    Array.Resize(ref deck, _kardCol - cardInHand);//перезаписываем массив колоды исключая из него карты, которые взяли в руку

                    for (int i = 0; i <= deck.Length - 1; ++i)//здесь мы забираем карту из колоды
                    {
                        bool b = GameObject.Find("Kard (" + (i + 1).ToString() + ")").GetComponent<Control>().inKoloda;
                        if (b)//записываем в массив колоды те карты, которые фактически еще в колоде
                        {
                            deck[i] = GameObject.Find("Kard (" + (i + 1).ToString() + ")");//массив заполняется перед функцией update каждый кадр, определяет сколько машин/Player(номер) зарегистрированно на сцене и записывает их в массив
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Если cardinHand == 0");
                cardInHand += 1;
                deck[cardCounter].GetComponent<Control>().inKoloda = false;
                deck[cardCounter].GetComponent<Control>().inHand = true;

                Array.Resize(ref deck, _kardCol - cardInHand);

                for (int i = 0; i <= deck.Length - 1; ++i)//здесь мы забираем карту из колоды
                {
                    bool b = GameObject.Find("Kard (" + (i + 1).ToString() + ")").GetComponent<Control>().inKoloda;
                    if (b)
                    {
                        deck[i] = GameObject.Find("Kard (" + (i + 1).ToString() + ")");//массив заполняется перед функцией update каждый кадр, определяет сколько машин/Player(номер) зарегистрированно на сцене и записывает их в массив
                    }
                }
            }

            Array.Resize(ref KIHC, cardInHand);//добавляем карту в массив карт в руке и расширяем массив
        }

        Debug.Log("Прошли перезапись и захват карты //");
        if (_addKIHC)
        {
            Debug.Log("Прошли условие _addKIHC");
            for (int i = 0; i <= KIHC.Length - 1; ++i)//перебираем карты в руке и задаем новые позиции
            {
<<<<<<< Updated upstream
                KIHC[i] = GameObject.Find("Kard (" + cardCounter.ToString() + ")");
                KIHC[i].GetComponent<Control>().inHand = true;//показываем что карта теперь в руке
                KIHC[i].GetComponent<Rigidbody>().isKinematic = true;//отключаем физику, что бы карта не падала из руки
                KIHC[i].GetComponent<Collider>().isTrigger = true;//пока карта в руке, она не должна взаимодействовать с другими картами (а то будут разлетаться во все стороны)
                break;
=======
                if (KIHC[i] == null)
                {
                    KIHC[i] = GameObject.Find("Kard (" + (cardCounter + 1).ToString() + ")");
                   // KIHC[i].GetComponent<Control>().inKoloda = false;
                    //KIHC[i].GetComponent<Control>().inHand = true;
                    KIHC[i].GetComponent<Rigidbody>().isKinematic = true;
                    KIHC[i].GetComponent<Collider>().isTrigger = true;
                    break;
                }
>>>>>>> Stashed changes
            }
            _addKIHC = false;
            GoToHand = true;
            _sh = 0;
        }
        Debug.Log("Прошли до финала функции ///");
    }

    private void RandomKard()//мешаем карты в колоде
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
    private void Sort()//сортируем карты в колоде так как они были изначально в самом массиве
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

    private void ResetCardP()// дополнительно для теста, возвращает все карты из руки в колоду
    {
        float _vectT = 0.15f;
        Array.Resize(ref deck, _kardCol);
        for (int i = 0; i <= deck.Length - 1; ++i)
        {
                deck[i] = GameObject.Find("Kard (" + (i + 1).ToString() + ")");//массив заполняется перед функцией update каждый кадр, определяет сколько машин/Player(номер) зарегистрированно на сцене и записывает их в массив
                deck[i].GetComponent<Control>().inKoloda = true;
                _vectT += 0.25f;
                deck[i].transform.position = new Vector3(-3.45f, _vectT, -3.2f);
                deck[i].transform.rotation = new Quaternion(0, 0, 0, 0);

        }

        for (int i = 0; i <= KIHC.Length - 1; ++i)//перебираем карты в руке и задаем новые позиции
        {
            KIHC[i].GetComponent<Control>().inHand = false;
            KIHC[i].GetComponent<Rigidbody>().isKinematic = false;
            KIHC[i].GetComponent<Collider>().isTrigger = false;
            cardInHand = 0;
            KIHC[i] = null;
        }

        Array.Resize(ref KIHC, 0);
    }

    public GameObject cardForMove;
    [SerializeField] private Camera CameraForControl;
    [SerializeField] private Vector3 mPos;
    [SerializeField] private float speedTransformPosition = 5.0f;
    public bool CMove = false;//проверяем двигаем ли мы какую-либо карту

    public void CardMove(GameObject gocfm)
    {
        mPos = CameraForControl.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraForControl.transform.position.y));
        gocfm.transform.position = Vector3.Lerp(gocfm.transform.position, new Vector3(mPos.x, gocfm.transform.position.y, mPos.z), speedTransformPosition * Time.deltaTime);
        gocfm.transform.rotation = new Quaternion(0,gocfm.transform.localRotation.y, 0, 1);
        gocfm.GetComponent<Collider>().isTrigger = false;
        gocfm.GetComponent<Control>().goOnTable = true;
        gocfm.GetComponent<Rigidbody>().isKinematic = true;
        CMove = true;
    }

    private void OnGUI()//кнопки
    {
        if (GUI.Button(new Rect(40, Screen.height / 3 - 20, 100, 30), "Сортировка"))
        {
            Sort();
        }

        if (GUI.Button(new Rect(40, Screen.height / 3 + 10, 100, 30), "Перемешать"))
        {
            RandomKard();
        }

        if (GUI.Button(new Rect(40, Screen.height / 3 + 40, 150, 30), "Вернуть в колоду"))
        {
            if (KIHC[0] != null)
            {
                ResetCardP();
            }
        }
    }
}
