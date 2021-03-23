using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_black_tree : binary_search_tree
{
    public red_black_tree(int data) : base(data) { }
    public override void insert(int data)
    {
        binary_search_node temp = new binary_search_node(data);
        temp.Colors = binary_node<int>.Colour.red;
        insert_data(root, temp, null);
        check_conditions(temp);
    }

    private void check_conditions(binary_node<int> temp)
    {
        List<binary_node<int>> path_node = path(temp);
        binary_node<int> parent = path_node[path_node.Count - 2];
        if (parent.Colors == binary_node<int>.Colour.black) { return; }
        binary_node<int> grand_parent = path_node[path_node.Count - 3];
        if (parent.Colors == binary_node<int>.Colour.red)
        {
            binary_node<int>.Colour side_color=binary_node<int>.Colour.black;
            binary_node<int> side = new binary_node<int>(0);
            (side_color,side)= color_of_side(parent, grand_parent);
            if (side_color == binary_node<int>.Colour.black) 
            {
                binary_node<int> temp_child=do_rotations(grand_parent,temp);
                binary_node<int> temp_parent = new binary_node<int>(0);
                if (grand_parent == root) { root = temp_child; }
                else 
                {
                    temp_parent = path_node[path_node.Count - 4];
                    if (temp_parent.data < temp_child.data) { temp_parent.right = temp_child; }
                    else { temp_parent.left = temp_child; }
                }
                temp_child.Colors = binary_node<int>.Colour.black;
                if (temp_child.left.data == grand_parent.data) { temp_child.left.Colors = binary_node<int>.Colour.red; }
                else { temp_child.right.Colors = binary_node<int>.Colour.red; }
                root.Colors = binary_node<int>.Colour.black;

            }
            else
            {
                side.Colors = binary_node<int>.Colour.black;
                grand_parent.Colors = binary_node<int>.Colour.red;
                parent.Colors = binary_node<int>.Colour.black;
                root.Colors = binary_node<int>.Colour.black;
                color_check();
            }
        }
    }

    private binary_node<int> do_rotations(binary_node<int> grand_node, binary_node<int> node)
    {
        if (grand_node.left != null)
        {
            if (grand_node.left.left == node) { return left_left(grand_node); }
            else if (grand_node.left.right == node) { return left_right(grand_node); }
        }
        if (grand_node.right != null)
        {
            if (grand_node.right.left == node) { return right_left(grand_node); }
            else if (grand_node.right.right == node) { return right_right(grand_node); }
        }
        return null;
    }

    private void color_check()
    {
        Stack<binary_node<int>> all_nodes = new Stack<binary_node<int>>();
        all_nodes.Push(root);
        binary_node<int> temp = all_nodes.Peek();
        while (all_nodes.Count > 0)
        {
            temp = all_nodes.Pop();
            if (temp.right != null)
            {
                if (temp.right.Colors == binary_node<int>.Colour.red && temp.Colors == binary_node<int>.Colour.red) { check_conditions(temp.right); return; }
                else { all_nodes.Push(temp.right); }
            }
            else if (temp.left != null)
            {
                if (temp.left.Colors == binary_node<int>.Colour.red && temp.Colors == binary_node<int>.Colour.red) { check_conditions(temp.left); return; }
                else { all_nodes.Push(temp.left); }
            }
        }
    }

    private (binary_node<int>.Colour, binary_node<int>) color_of_side(binary_node<int> temp, binary_node<int> parent)
    {
        binary_node<int> side;
        if (parent.left == temp) { side = parent.right; }
        else { side = parent.left; }
        if (side == null) { return (binary_node<int>.Colour.black,null); }
        else { return (side.Colors,side); }
    }
}
