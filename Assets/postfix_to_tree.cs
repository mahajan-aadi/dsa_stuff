using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postfix_to_tree : MonoBehaviour
{
    string expression;
    void Start()
    {
        expression = "ab*c/ef/g*+k+xy*-";
        tree(expression);
    }
    void tree(string expression)
    {
        Stack<binary_tree<string>.binary_node<string>> my_stack = new Stack<binary_tree<string>.binary_node<string>>();
        foreach(char i in expression)
        {
            string q = i.ToString();
            binary_tree<string>.binary_node<string> temp = new binary_tree<string>.binary_node<string>(q);
            if (q=="*"|| q == "/" || q == "+" || q == "-" || q == "^")
            {
                temp.left = my_stack.Pop();
                temp.right = my_stack.Pop();
            }
            my_stack.Push(temp);

        }
        binary_tree<string> bt = new binary_tree<string>(my_stack.Pop());
    }

}
