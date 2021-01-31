using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infixtopostfix : MonoBehaviour
{
    string expression;
    string result;
    Stack<string> my_stack = new Stack<string>();
    void Start()
    {
        expression = "K+L-M*N+(O^P)*W/U/V*T+Q";
        expression = "a+b*(c^d-e)^(f+g*h)-i";
        print(change(expression));
    }
    string change(string expression)
    {
        result="";
        foreach(char i in expression)
        {
            string q = i.ToString();
            if(q==" ") { continue; }
            else if(q=="("|| q == ")" || q == "+" || q == "-" || q == "*" || q == "/" || q == "^") { conditions(q); }
            else { result += q; }
        }
        while (my_stack.Count > 0) { result += my_stack.Pop(); }
        return result;
    } 
    void conditions(string q)
    {
        if (my_stack.Count == 0) { my_stack.Push(q); }
        else if(my_stack.Peek() == "(" ) { my_stack.Push(q); }
        else if (q == "(") { my_stack.Push(q); }
        else if (q == ")")
        {
            while (true)
            {
                string z = my_stack.Pop();
                if (z == "(") { break; }
                else { result += z; }
            }
        }
        else if (presidence(q) > presidence(my_stack.Peek())) { my_stack.Push(q); }
        else if (presidence(q) < presidence(my_stack.Peek())) { result += my_stack.Pop();conditions(q); }
        else if (presidence(q) == presidence(my_stack.Peek())) 
        {
            if (q == "^") { my_stack.Push(q); }
            else
            {
                result += my_stack.Pop();
                conditions(q);
            }
        }

    }
    int presidence(string q)
    {
        switch (q)
        {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
                return 2;
            case "^":
                return 3;
            default:
                return 0;

        }
    }

}
