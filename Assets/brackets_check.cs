using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brackets_check : MonoBehaviour
{
    Stack<string> my_stack = new Stack<string>();
    string expression;
    void Start()
    {
        expression = "2 +[7 -{ 8*(6/3)}+a";
        print(check_expression(expression));
    }
    bool check_expression(string expression)
    {
        foreach(char q in expression)
        {
            string i = q.ToString();
            if (i.Equals("(") || i.Equals("{") || i.Equals("["))
            {
                my_stack.Push(i);
            }
            else if(i.Equals(")") || i.Equals("}") || i.Equals("]"))
            {
                string check = condition(i);
                if (check.Equals(my_stack.Peek())) { my_stack.Pop(); }
                else { return false; }
            }
        }
        if (my_stack.Count > 0) { return false; }
        return true;
    }

    string condition(string i)
    {
        if (i == ")") { return "("; }
        else if (i == "}") { return "{"; }
        else if (i == "]") { return "["; }
        return null;
    }
}
