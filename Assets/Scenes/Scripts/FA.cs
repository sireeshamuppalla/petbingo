using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using System.Linq;
using Fractions; //Nugget Package
using UnityEngine.EventSystems;
using System.Drawing;
using System;
using Unity.VisualScripting;
using JetBrains.Annotations;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine.SceneManagement;

//This is an extension class that helps us create an extension method that allows us to shuffle a list 
public static class Extensions
{
    private static System.Random randomizer = new System.Random();

    public static void Shuffle<T>(this IList<T> values)
    {
        for (int i = values.Count - 1; i > 0; i--)
        {
            int k = randomizer.Next(i + 1);
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
    }
}


public class FA : MonoBehaviour
{
    [SerializeField] List<AudioSource> gameSounds = new List<AudioSource>();
    public static string levelSelect;
    public static FA faInstance;

    public GameObject hintObject, questionObject, buttonObject1, buttonObject2, buttonObject3, buttonObject4
        , buttonObject5, buttonObject6, buttonObject7, buttonObject8, buttonObject9
        , buttonObject10, buttonObject11, buttonObject12, buttonObject13, buttonObject14
        , buttonObject15, buttonObject16, buttonObject17, buttonObject18, buttonObject19
        , buttonObject20, buttonObject21, buttonObject22, buttonObject23, buttonObject24
        , buttonObject25;

    public UnityEngine.UI.Button button1, button2, button3, button4, button5, button6, button7, button8
        , button9, button10, button11, button12, button13, button14, button15, button16
        , button17, button18, button19, button20, button21, button22, button23, button24
        , button25;

    public Text hintTextObject, questionTextObject, textObject1, textObject2, textObject3, textObject4, textObject5, textObject6
        , textObject7, textObject8, textObject9, textObject10, textObject11, textObject12
        , textObject13, textObject14, textObject15, textObject16, textObject17, textObject18
        , textObject19, textObject20, textObject21, textObject22, textObject23, textObject24
        , textObject25;

    public Stack FractionAStack = new Stack(); //this stack will contain the fraction on the left side 
    public Stack FractionBStack = new Stack(); //this stack will contain the fraction on the right side
    public Stack ResultStack = new Stack(); //this stack will contain the result of fraction subtraction operations from elements of the previous two stacks
    Stack CheckGreaterFractionStack = new Stack(); //this stack will help us store the value of CheckGreaterFraction

    public List<Fraction> FractionAList = new List<Fraction>(); //this list will contain a record of the left fraction even after they have been popped out of their stack
    public List<Fraction> FractionBList = new List<Fraction>(); //this list will contain a record of the right fraction after they have been popped out of their stack
    public List<Fraction> ResultList = new List<Fraction>(); //this list will contain a record of the subtraction result fraction after they have been popped out of their stack

    static int RandomNumerator_A = 0; //Random.Range(1, 9); //this will generate a random number between 1 and 9, and it will be the numerator of the fraction on the left side
    static int RandomNumerator_B = 0; //Random.Range(1, 9); //this will generate a random number between 1 and 9, and it will be the numerator of the fraction on the right side
    static int RandomCommonDenominator = 1; //Random.Range(1, 9); //this will generate a random number between 1 and 9, and it will be the denominator for both fractions

    public static Fraction FractionA = new Fraction(RandomNumerator_A, RandomCommonDenominator);//this is a fraction on the left side, created with the values of RandomNumerator_left and CommonDenominator
    public static Fraction FractionB = new Fraction(RandomNumerator_B, RandomCommonDenominator); //this is a fraction on the left side, created with the values of  RandomNumerator_right and RandomCommonDenominator
    public static Fraction ResultFraction = FractionA.Add(FractionB); //this is a fraction created from subtracting RandomNumerator_right from RandomNumerator_left

    decimal CheckGreaterFraction = 0; //this will help calculate the difference between fraction A and B, which we will use to determine where fraction A or B should go in order to avoid negative results 

    public GameObject gameOver, bingo, CongratulatePanel;
    

    public bool bingoPossible = true;
    public bool myBingo = false;
    public static int incrementBingo;
    public static int easyScore;
    public static int medScore;
    public static int hardScore;
    public static int easyProblemCount;
    public static int hardProblemCount;
    public static int medProblemCount;
    public static bool h1 = true;
    public static bool h2 = true;
    public static bool h3 = true;
    public static bool h4 = true;
    public static bool h5 = true;
    public static bool v1 = true;
    public static bool v2 = true;
    public static bool v3 = true;
    public static bool v4 = true;
    public static bool v5 = true;
    public static bool d1 = true;
    public static bool d2 = true;

    public static bool h_1 = false;
    public static bool h_2 = false;
    public static bool h_3 = false;
    public static bool h_4 = false;
    public static bool h_5 = false;
    public static bool v_1 = false;
    public static bool v_2 = false;
    public static bool v_3 = false;
    public static bool v_4 = false;
    public static bool v_5 = false;
    public static bool d_1 = false;
    public static bool d_2 = false;

    public List<Fraction> bingo_h1 = new List<Fraction>(5);
    public List<Fraction> bingo_h2 = new List<Fraction>(5);
    public List<Fraction> bingo_h3 = new List<Fraction>(5);
    public List<Fraction> bingo_h4 = new List<Fraction>(5);
    public List<Fraction> bingo_h5 = new List<Fraction>(5);

    public List<Fraction> bingo_v1 = new List<Fraction>(5);
    public List<Fraction> bingo_v2 = new List<Fraction>(5);
    public List<Fraction> bingo_v3 = new List<Fraction>(5);
    public List<Fraction> bingo_v4 = new List<Fraction>(5);
    public List<Fraction> bingo_v5 = new List<Fraction>(5);

    public List<Fraction> bingo_d1 = new List<Fraction>(5);
    public List<Fraction> bingo_d2 = new List<Fraction>(5);

    void AddElementsToTheirStacks()
    {
        for (int i = 0; i < 25; i++)
        {
            if (levelSelect == "EASY")
            {
                RandomNumerator_A = UnityEngine.Random.Range(1, 10);
                RandomNumerator_B = UnityEngine.Random.Range(1, 10);
                RandomCommonDenominator = 2;
               
            }
            else if (levelSelect == "MEDIUM")
            {
                RandomNumerator_A = UnityEngine.Random.Range(11, 20);
                RandomNumerator_B = UnityEngine.Random.Range(11, 20);
                RandomCommonDenominator = 5;
              
            }
            else if (levelSelect == "HARD")
            {
                RandomNumerator_A = UnityEngine.Random.Range(21, 30);
                RandomNumerator_B = UnityEngine.Random.Range(21, 30);
                RandomCommonDenominator = UnityEngine.Random.Range(1, 10);
               
            }

            FractionA = new Fraction(RandomNumerator_A, RandomCommonDenominator);
            FractionB = new Fraction(RandomNumerator_B, RandomCommonDenominator);
            CheckGreaterFraction = FractionA.ToDecimal() + FractionB.ToDecimal();

            CheckGreaterFractionStack.Push(CheckGreaterFraction);

            if (CheckGreaterFraction < 0) ResultFraction = FractionB.Add(FractionA);
            else ResultFraction = FractionA.Add(FractionB);

            FractionAStack.Push(FractionA);
            FractionAList.Add(FractionA);
            FractionBStack.Push(FractionB);
            FractionBList.Add(FractionB);
            ResultStack.Push(ResultFraction);
            ResultList.Add(ResultFraction);
        }
        populateCorrectBingo();
        ResultList.Shuffle(); //shuffling the list that will be used to display the answers
    }



    // Start is called before the first frame update
    void Start()
    {
        AddElementsToTheirStacks();
        ShowQuestionAndAnswerOptionsToScreen();
        Button btn1 = button1.GetComponent<Button>();
        btn1.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn2 = button2.GetComponent<Button>();
        btn2.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn3 = button3.GetComponent<Button>();
        btn3.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn4 = button4.GetComponent<Button>();
        btn4.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn5 = button5.GetComponent<Button>();
        btn5.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn6 = button6.GetComponent<Button>();
        btn6.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn7 = button7.GetComponent<Button>();
        btn7.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn8 = button8.GetComponent<Button>();
        btn8.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn9 = button9.GetComponent<Button>();
        btn9.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn10 = button10.GetComponent<Button>();
        btn10.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn11 = button11.GetComponent<Button>();
        btn11.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn12 = button12.GetComponent<Button>();
        btn12.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn13 = button13.GetComponent<Button>();
        btn13.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn14 = button14.GetComponent<Button>();
        btn14.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn15 = button15.GetComponent<Button>();
        btn15.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn16 = button16.GetComponent<Button>();
        btn16.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn17 = button17.GetComponent<Button>();
        btn17.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn18 = button18.GetComponent<Button>();
        btn18.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn19 = button19.GetComponent<Button>();
        btn19.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn20 = button20.GetComponent<Button>();
        btn20.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn21 = button21.GetComponent<Button>();
        btn21.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn22 = button22.GetComponent<Button>();
        btn22.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn23 = button23.GetComponent<Button>();
        btn23.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn24 = button24.GetComponent<Button>();
        btn24.onClick.AddListener(ToDoWhenButtonClicked);
        Button btn25 = button25.GetComponent<Button>();
        btn25.onClick.AddListener(ToDoWhenButtonClicked);

    }

    //This method is used to populate the UI with responses on buttons, and questions on the specified text area
    void ShowQuestionAndAnswerOptionsToScreen()
    {
        questionTextObject = questionObject.GetComponent<Text>();
        hintTextObject = hintObject.GetComponent<Text>();
        textObject1 = buttonObject1.GetComponent<Text>();
        textObject2 = buttonObject2.GetComponent<Text>();
        textObject3 = buttonObject3.GetComponent<Text>();
        textObject4 = buttonObject4.GetComponent<Text>();
        textObject5 = buttonObject5.GetComponent<Text>();
        textObject6 = buttonObject6.GetComponent<Text>();
        textObject7 = buttonObject7.GetComponent<Text>();
        textObject8 = buttonObject8.GetComponent<Text>();
        textObject9 = buttonObject9.GetComponent<Text>();
        textObject10 = buttonObject10.GetComponent<Text>();
        textObject11 = buttonObject11.GetComponent<Text>();
        textObject12 = buttonObject12.GetComponent<Text>();
        textObject13 = buttonObject13.GetComponent<Text>();
        textObject14 = buttonObject14.GetComponent<Text>();
        textObject15 = buttonObject15.GetComponent<Text>();
        textObject16 = buttonObject16.GetComponent<Text>();
        textObject17 = buttonObject17.GetComponent<Text>();
        textObject18 = buttonObject18.GetComponent<Text>();
        textObject19 = buttonObject19.GetComponent<Text>();
        textObject20 = buttonObject20.GetComponent<Text>();
        textObject21 = buttonObject21.GetComponent<Text>();
        textObject22 = buttonObject22.GetComponent<Text>();
        textObject23 = buttonObject23.GetComponent<Text>();
        textObject24 = buttonObject24.GetComponent<Text>();
        textObject25 = buttonObject25.GetComponent<Text>();

        //Display the answer options
        textObject1.text = ResultList.ElementAt(0).ToString(); //ResultStack.Pop().ToString();
        textObject2.text = ResultList.ElementAt(1).ToString(); //ResultStack.Pop().ToString();
        textObject3.text = ResultList.ElementAt(2).ToString(); //ResultStack.Pop().ToString();
        textObject4.text = ResultList.ElementAt(3).ToString(); //ResultStack.Pop().ToString();
        textObject5.text = ResultList.ElementAt(4).ToString(); //ResultStack.Pop().ToString();
        textObject6.text = ResultList.ElementAt(5).ToString(); //ResultStack.Pop().ToString();
        textObject7.text = ResultList.ElementAt(6).ToString(); //ResultStack.Pop().ToString();
        textObject8.text = ResultList.ElementAt(7).ToString(); //ResultStack.Pop().ToString();
        textObject9.text = ResultList.ElementAt(8).ToString(); //ResultStack.Pop().ToString();
        textObject10.text = ResultList.ElementAt(9).ToString(); //ResultStack.Pop().ToString();
        textObject11.text = ResultList.ElementAt(10).ToString(); //ResultStack.Pop().ToString();
        textObject12.text = ResultList.ElementAt(11).ToString(); //ResultStack.Pop().ToString();
        textObject13.text = ResultList.ElementAt(12).ToString(); //ResultStack.Pop().ToString();
        textObject14.text = ResultList.ElementAt(13).ToString(); //ResultStack.Pop().ToString();
        textObject15.text = ResultList.ElementAt(14).ToString(); //ResultStack.Pop().ToString();
        textObject16.text = ResultList.ElementAt(15).ToString(); //ResultStack.Pop().ToString();
        textObject17.text = ResultList.ElementAt(16).ToString(); //ResultStack.Pop().ToString();
        textObject18.text = ResultList.ElementAt(17).ToString(); //ResultStack.Pop().ToString();
        textObject19.text = ResultList.ElementAt(18).ToString(); //ResultStack.Pop().ToString();
        textObject20.text = ResultList.ElementAt(19).ToString(); //ResultStack.Pop().ToString();
        textObject21.text = ResultList.ElementAt(20).ToString(); //ResultStack.Pop().ToString();
        textObject22.text = ResultList.ElementAt(21).ToString(); //ResultStack.Pop().ToString();
        textObject23.text = ResultList.ElementAt(22).ToString(); //ResultStack.Pop().ToString();
        textObject24.text = ResultList.ElementAt(23).ToString(); //ResultStack.Pop().ToString();
        textObject25.text = ResultList.ElementAt(24).ToString(); //ResultStack.Pop().ToString();

        //questionTextObject.text = "Fraction B + Fraction A = \n" + c;
        questionTextObject.text = FractionAStack.Peek().ToString() + " + "
                              + FractionBStack.Peek().ToString() + "\n= ?";
        string FractionAString;
        string FractionBString;
        FractionAString = FractionAStack.Peek().ToString();
        FractionBString = FractionBStack.Peek().ToString();
        int LeftNum, RightNum, LeftDen, RightDen;
        if (FractionAString.Contains('/'))
        {
            string[] split = FractionAString.Split('/');
            LeftNum = int.Parse(split[0]);
            LeftDen = int.Parse(split[1]);
            if (FractionBString.Contains('/'))
            {
                string[] split1 = FractionBString.Split('/');
                RightNum = int.Parse(split1[0]);
                RightDen = int.Parse(split1[1]);
            }
            else
            {
                RightNum = int.Parse(FractionBString);
                RightDen = 1;
            }
        }
        else
        {
            LeftNum = int.Parse(FractionAString);
            LeftDen = 1;
            if (FractionBString.Contains('/'))
            {
                string[] split1 = FractionBString.Split('/');
                RightNum = int.Parse(split1[0]);
                RightDen = int.Parse(split1[1]);
            }
            else
            {
                RightNum = int.Parse(FractionBString);
                RightDen = 1;
            }
        }
        hintTextObject.text = "( " + "( " + LeftNum.ToString() + " * " + RightDen.ToString() + " )" + " + " + "( " + RightNum.ToString() + " * " +
            LeftDen.ToString() + " )" + " )" + " / " + "( " + LeftDen.ToString() + " * " + RightDen.ToString() + " )";
    }


    void ToDoWhenButtonClicked()
    {
        

        var textOnCurrentButton = GameObject.Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Button>().GetComponentInChildren<Text>().text;

        string currentButtonName = EventSystem.current.currentSelectedGameObject.name;
        int currentButtonNumber = int.Parse(string.Concat(currentButtonName.Where(char.IsDigit)));

        Debug.Log("ResultStack.Pop().ToString() : " + ResultStack.Peek().ToString());
        Debug.Log("textOnCurrentButton.ToString() : " + textOnCurrentButton.ToString());
        Debug.Log("FractionAStack count : " + FractionAStack.Count);
        Debug.Log("FractionBStack count : " + FractionBStack.Count);
        Debug.Log("Clicked button name: " + currentButtonName);
        Debug.Log("Clicked button number: " + currentButtonNumber);

        if (ResultStack.Pop().ToString().Equals(textOnCurrentButton.ToString()))
        {
           
            FractionAStack.Pop();
            FractionBStack.Pop();
            ResultList[currentButtonNumber - 1] = 00;
            markRight(currentButtonNumber);
            if (levelSelect == "EASY")
            {
                easyScore = easyScore + 1;
                easyProblemCount += 1;
            }
            if (levelSelect == "MEDIUM")
            {
                medScore = medScore + 1;
                medProblemCount += 1;
            }
            if (levelSelect == "HARD")
            {
                hardScore = hardScore + 1;
                hardProblemCount += 1;
            }

            //Play sound for correct answer
            //gameSounds[0].Play();

            //Change display image of button to rabbit image on top of button and make button not clickable
            GameObject.Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Button>().interactable = false;
            //Show next question
            questionTextObject.text = FractionAStack.Peek().ToString() + " + "
                         + FractionBStack.Peek().ToString() + "\n= ? ";
        }

        else
        {
            FractionAStack.Pop();
            FractionBStack.Pop();
            ResultList[currentButtonNumber - 1] = 00;
            markWrong(currentButtonNumber);

            if (levelSelect == "EASY")
            {
                easyProblemCount += 1;
            }
            if (levelSelect == "MEDIUM")
            {
                medProblemCount += 1;
            }
            if (levelSelect == "HARD")
            {
                hardProblemCount += 1;
            }

            //ResultList[currentButtonNumber - 1] = 00;
            //destroy button, make a sound and prevent corresponding fractions from displaying
            EventSystem.current.currentSelectedGameObject.SetActive(false); //UI button destroyed
                                                                            //gameSounds[1].Play(); //Sound playing for wrong answer
                                                                            //Debug.Log(gameSounds[1]);

            //show next question
            questionTextObject.text = FractionAStack.Peek().ToString() + " + "
                            + FractionBStack.Peek().ToString() + "\n= ? ";

        }
       
        isBingo();
        isBingoPossible();

        string FractionAString;
        string FractionBString;
        FractionAString = FractionAStack.Peek().ToString();
        FractionBString = FractionBStack.Peek().ToString();
        int LeftNum, RightNum, LeftDen, RightDen;
        if (FractionAString.Contains('/'))
        {
            string[] split = FractionAString.Split('/');
            LeftNum = int.Parse(split[0]);
            LeftDen = int.Parse(split[1]);
            if (FractionBString.Contains('/'))
            {
                string[] split1 = FractionBString.Split('/');
                RightNum = int.Parse(split1[0]);
                RightDen = int.Parse(split1[1]);
            }
            else
            {
                RightNum = int.Parse(FractionBString);
                RightDen = 1;
            }
        }
        else
        {
            LeftNum = int.Parse(FractionAString);
            LeftDen = 1;
            if (FractionBString.Contains('/'))
            {
                string[] split1 = FractionBString.Split('/');
                RightNum = int.Parse(split1[0]);
                RightDen = int.Parse(split1[1]);
            }
            else
            {
                RightNum = int.Parse(FractionBString);
                RightDen = 1;
            }
        }
        hintTextObject.text = "( " + "( " + LeftNum.ToString() + " * " + RightDen.ToString() + " )" + " + " + "( " + RightNum.ToString() + " * " +
             LeftDen.ToString() + " )" + " )" + " / " + "( " + LeftDen.ToString() + " * " + RightDen.ToString() + " )";
    }


    public void isBingo()
    {

        for (int i = 0; i < 5; i++)
        {
            Debug.Log(bingo_h1[i]);
        }

        h_1 = bingo_h1.TrueForAll(p => p == 00);
        h_2 = bingo_h2.TrueForAll(p => p == 00);
        h_3 = bingo_h3.TrueForAll(p => p == 00);
        h_4 = bingo_h4.TrueForAll(p => p == 00);
        h_5 = bingo_h5.TrueForAll(p => p == 00);
        v_1 = bingo_v1.TrueForAll(p => p == 00);
        v_2 = bingo_v2.TrueForAll(p => p == 00);
        v_3 = bingo_v3.TrueForAll(p => p == 00);
        v_4 = bingo_v4.TrueForAll(p => p == 00);
        v_5 = bingo_v5.TrueForAll(p => p == 00);
        d_1 = bingo_d1.TrueForAll(p => p == 00);
        d_2 = bingo_d2.TrueForAll(p => p == 00);

        if (h_1 || h_2 || h_3 || h_4 || h_5 || v_1 || v_2 || v_3 || v_4 || v_5 || d_1 || d_2)
        {
            myBingo = true;
            bingo.SetActive(true);
            incrementBingo = incrementBingo + 1;
            Debug.Log("increment bongo" + incrementBingo);
            questionTextObject.text = "";
            Debug.Log("Bingo!");
            resetVariables();
            clearEverything();
            string bingoOrNot = "BINGO";
            StartCoroutine(SceneLoader(bingoOrNot));
        }
    }

    public void isBingoPossible()
    {
        if (h1 == false && h2 == false && h3 == false && h4 == false && h5 == false && v1 == false && v2 == false && v3 == false
            && v4 == false && v5 == false && d1 == false && d2 == false)
        {
            bingoPossible = false;
            questionTextObject.text = "";
            gameOver.SetActive(true);
            Debug.Log("Game Over!");
            resetVariables();
            clearEverything();
            string bingoOrNot = "NOT BINGO";
            StartCoroutine(SceneLoader(bingoOrNot));
        }
    }

    public void markWrong(int currentButtonNumber)
    {
        if (currentButtonNumber-1 == 0)
        {
            if (h1 == true)
            {
                h1 = false;
            }
            if (v1 == true)
            {
                v1 = false;
            }
            if (d1 == true)
            {
                d1 = false;
            }
        }
        else if (currentButtonNumber-1 == 1)
        {
            if (h1 == true)
            {
                h1 = false;
            }
            if (v2 == true)
            {
                v2 = false;
            }
        }
        else if (currentButtonNumber-1 == 2)
        {
            if (h1 == true)
            {
                h1 = false;
            }
            if (v3 == true)
            {
                v3 = false;
            }
        }
        else if (currentButtonNumber-1 == 3)
        {
            if (h1 == true)
            {
                h1 = false;
            }
            if (v4 == true)
            {
                v4 = false;
            }
        }
        else if (currentButtonNumber-1 == 4)
        {
            if (h1 == true)
            {
                h1 = false;
            }
            if (v5 == true)
            {
                v5 = false;
            }
            if (d2 == true)
            {
                d2 = false;
            }
        }
        else if (currentButtonNumber-1 == 5)
        {
            if (h2 == true)
            {
                h2 = false;
            }
            if (v1 == true)
            {
                v1 = false;
            }
        }
        else if (currentButtonNumber-1 == 6)
        {
            if (h2 == true)
            {
                h2 = false;
            }
            if (v2 == true)
            {
                v2 = false;
            }
            if (d1 == true)
            {
                d1 = false;
            }
        }
        else if (currentButtonNumber-1 == 7)
        {
            if (h2 == true)
            {
                h2 = false;
            }
            if (v3 == true)
            {
                v3 = false;
            }
        }
        else if (currentButtonNumber-1 == 8)
        {
            if (h2 == true)
            {
                h2 = false;
            }
            if (v4 == true)
            {
                v4 = false;
            }
            if (d2 == true)
            {
                d2 = false;
            }
        }
        else if (currentButtonNumber-1 == 9)
        {
            if (h2 == true)
            {
                h2 = false;
            }
            if (v5 == true)
            {
                v5 = false;
            }
        }
        else if (currentButtonNumber-1 == 10)
        {
            if (h3 == true)
            {
                h3 = false;
            }
            if (v1 == true)
            {
                v1 = false;
            }
        }
        else if (currentButtonNumber-1 == 11)
        {
            if (h3 == true)
            {
                h3 = false;
            }
            if (v2 == true)
            {
                v2 = false;
            }
        }
        else if (currentButtonNumber-1 == 12)
        {
            if (h3 == true)
            {
                h3 = false;
            }
            if (v3 == true)
            {
                v3 = false;
            }
            if (d1 == true)
            {
                d1 = false;
            }
            if (d2 == true)
            {
                d2 = false;
            }
        }
        else if (currentButtonNumber-1 == 13)
        {
            if (h3 == true)
            {
                h3 = false;
            }
            if (v4 == true)
            {
                v4 = false;
            }
        }
        else if (currentButtonNumber-1 == 14)
        {
            if (h3 == true)
            {
                h3 = false;
            }
            if (v5 == true)
            {
                v5 = false;
            }
        }
        else if (currentButtonNumber-1 == 15)
        {
            if (h4 == true)
            {
                h4 = false;
            }
            if (v1 == true)
            {
                v1 = false;
            }
        }
        else if (currentButtonNumber-1 == 16)
        {
            if (h4 == true)
            {
                h4 = false;
            }
            if (v2 == true)
            {
                v2 = false;
            }
            if (d2 == true)
            {
                d2 = false;
            }
        }
        else if (currentButtonNumber-1 == 17)
        {
            if (h4 == true)
            {
                h4 = false;
            }
            if (v3 == true)
            {
                v3 = false;
            }
        }
        else if (currentButtonNumber-1 == 18)
        {
            if (h4 == true)
            {
                h4 = false;
            }
            if (v4 == true)
            {
                v4 = false;
            }
            if (d1 == true)
            {
                d1 = false;
            }
        }
        else if (currentButtonNumber-1 == 19)
        {
            if (h4 == true)
            {
                h4 = false;
            }
            if (v5 == true)
            {
                v5 = false;
            }
        }
        else if (currentButtonNumber-1 == 20)
        {
            if (h5 == true)
            {
                h5 = false;
            }
            if (v1 == true)
            {
                v1 = false;
            }
            if (d2 == true)
            {
                d2 = false;
            }
        }
        else if (currentButtonNumber-1 == 21)
        {
            if (h5 == true)
            {
                h5 = false;
            }
            if (v2 == true)
            {
                v2 = false;
            }
        }
        else if (currentButtonNumber-1 == 22)
        {
            if (h5 == true)
            {
                h5 = false;
            }
            if (v3 == true)
            {
                v3 = false;
            }
        }
        else if (currentButtonNumber-1 == 23)
        {
            if (h5 == true)
            {
                h5 = false;
            }
            if (v4 == true)
            {
                v4 = false;
            }
        }
        else if (currentButtonNumber-1 == 24)
        {
            if (h5 == true)
            {
                h5 = false;
            }
            if (v5 == true)
            {
                v5 = false;
            }
            if (d1 == true)
            {
                d1 = false;
            }
        }
    }

    public void markRight(int currentButtonNumber)
    {
        if (currentButtonNumber-1 == 0)
        {
            bingo_h1[0] = 00;
            bingo_v1[0] = 00;
            bingo_d1[0] = 00;
        }
        else if (currentButtonNumber-1 == 1)
        {
            bingo_h1[1] = 00;
            bingo_v2[0] = 00;
        }
        else if (currentButtonNumber-1 == 2)
        {
            bingo_h1[2] = 00;
            bingo_v3[0] = 00;
        }
        else if (currentButtonNumber-1 == 3)
        {
            bingo_h1[3] = 00;
            bingo_v4[0] = 00;
        }
        else if (currentButtonNumber-1 == 4)
        {
            bingo_h1[4] = 00;
            bingo_v5[0] = 00;
            bingo_d2[0] = 00;
        }
        else if (currentButtonNumber-1 == 5)
        {
            bingo_h2[0] = 00;
            bingo_v1[1] = 00;
        }
        else if (currentButtonNumber-1 == 6)
        {
            bingo_h2[1] = 00;
            bingo_v2[1] = 00;
            bingo_d1[1] = 00;
        }
        else if (currentButtonNumber-1 == 7)
        {
            bingo_h2[2] = 00;
            bingo_v3[1] = 00;
        }
        else if (currentButtonNumber-1 == 8)
        {
            bingo_h2[3] = 00;
            bingo_v4[1] = 00;
            bingo_d2[1] = 00;
        }
        else if (currentButtonNumber-1 == 9)
        {
            bingo_h2[4] = 00;
            bingo_v5[1] = 00;
        }
        else if (currentButtonNumber-1 == 10)
        {
            bingo_h3[0] = 00;
            bingo_v1[2] = 00;
        }
        else if (currentButtonNumber-1 == 11)
        {
            bingo_h3[1] = 00;
            bingo_v2[2] = 00;
        }
        else if (currentButtonNumber-1 == 12)
        {
            bingo_h3[2] = 00;
            bingo_v3[2] = 00;
            bingo_d1[2] = 00;
            bingo_d2[2] = 00;
        }
        else if (currentButtonNumber-1 == 13)
        {
            bingo_h3[3] = 00;
            bingo_v4[2] = 00;
        }
        else if (currentButtonNumber-1 == 14)
        {
            bingo_h3[4] = 00;
            bingo_v5[2] = 00;
        }
        else if (currentButtonNumber-1 == 15)
        {
            bingo_h4[0] = 00;
            bingo_v1[3] = 00;
        }
        else if (currentButtonNumber-1 == 16)
        {
            bingo_h4[1] = 00;
            bingo_v2[3] = 00;
            bingo_d2[3] = 00;
        }
        else if (currentButtonNumber-1 == 17)
        {
            bingo_h4[2] = 00;
            bingo_v3[3] = 00;
        }
        else if (currentButtonNumber-1 == 18)
        {
            bingo_h4[3] = 00;
            bingo_v4[3] = 00;
            bingo_d1[3] = 00;
        }
        else if (currentButtonNumber-1 == 19)
        {
            bingo_h4[4] = 00;
            bingo_v5[3] = 00;
        }
        else if (currentButtonNumber-1 == 20)
        {
            bingo_h5[0] = 00;
            bingo_v1[4] = 00;
            bingo_d2[4] = 00;
        }
        else if (currentButtonNumber-1 == 21)
        {
            bingo_h5[1] = 00;
            bingo_v2[4] = 00;
        }
        else if (currentButtonNumber-1 == 22)
        {
            bingo_h5[2] = 00;
            bingo_v3[4] = 00;
        }
        else if (currentButtonNumber-1 == 23)
        {
            bingo_h5[3] = 00;
            bingo_v4[4] = 00;
        }
        else if (currentButtonNumber-1 == 24)
        {
            bingo_h5[4] = 00;
            bingo_v5[4] = 00;
            bingo_d1[4] = 00;
        }
    }

    public void resetVariables()
    {
        h1 = true;
        h2 = true;
        h3 = true;
        h4 = true;
        h5 = true;
        v1 = true;
        v2 = true;
        v3 = true;
        v4 = true;
        v5 = true;
        d1 = true;
        d2 = true;

        h_1 = false;
        h_2 = false;
        h_3 = false;
        h_4 = false;
        h_5 = false;
        v_1 = false;
        v_2 = false;
        v_3 = false;
        v_4 = false;
        v_5 = false;
        d_1 = false;
        d_2 = false;

        bingoPossible = true;
        myBingo = false;
    }
    IEnumerator SceneLoader(string bingoOrNot)
    {
        if (bingoOrNot == "BINGO")
        {
            yield return new WaitForSeconds(2f);
            CongratulatePanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Scene3");
        }
    }


    public void clearEverything()
    {
        Debug.Log("clear everything()");
        ResultList.Clear();
        bingo_h1.Clear();
        bingo_h2.Clear();
        bingo_h3.Clear();
        bingo_h4.Clear();
        bingo_h5.Clear();
        bingo_v1.Clear();
        bingo_v2.Clear();
        bingo_v3.Clear();
        bingo_v4.Clear();
        bingo_v5.Clear();
        bingo_d1.Clear();
        bingo_d2.Clear();
    }

    public void populateCorrectBingo()
    {
        Debug.Log("populate correctBingo()");

        
        for (int i = 0; i < 5; i++)
        {
            bingo_h1.Insert(i, ResultList[i]);
            bingo_h2.Insert(i, ResultList[i + 5]);
            bingo_h3.Insert(i, ResultList[i + 10]);
            bingo_h4.Insert(i, ResultList[i + 15]);
            bingo_h5.Insert(i, ResultList[i + 20]);
        }

        bingo_v1.Add(ResultList[0]);
        bingo_v2.Add(ResultList[1]);
        bingo_v3.Add(ResultList[2]);
        bingo_v4.Add(ResultList[3]);
        bingo_v5.Add(ResultList[4]);

        bingo_v1.Add(ResultList[5]);
        bingo_v2.Add(ResultList[6]);
        bingo_v3.Add(ResultList[7]);
        bingo_v4.Add(ResultList[8]);
        bingo_v5.Add(ResultList[9]);

        bingo_v1.Add(ResultList[10]);
        bingo_v2.Add(ResultList[11]);
        bingo_v3.Add(ResultList[12]);
        bingo_v4.Add(ResultList[13]);
        bingo_v5.Add(ResultList[14]);

        bingo_v1.Add(ResultList[15]);
        bingo_v2.Add(ResultList[16]);
        bingo_v3.Add(ResultList[17]);
        bingo_v4.Add(ResultList[18]);
        bingo_v5.Add(ResultList[19]);

        bingo_v1.Add(ResultList[20]);
        bingo_v2.Add(ResultList[21]);
        bingo_v3.Add(ResultList[22]);
        bingo_v4.Add(ResultList[23]);
        bingo_v5.Add(ResultList[24]);

        bingo_d1.Add(ResultList[0]);
        bingo_d1.Add(ResultList[6]);
        bingo_d1.Add(ResultList[12]);
        bingo_d1.Add(ResultList[18]);
        bingo_d1.Add(ResultList[24]);

        bingo_d2.Add(ResultList[4]);
        bingo_d2.Add(ResultList[8]);
        bingo_d2.Add(ResultList[12]);
        bingo_d2.Add(ResultList[16]);
        bingo_d2.Add(ResultList[20]);
    }

}
