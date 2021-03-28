using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_black_tree : binary_search_tree
{
    binary_node<int>.Colour black = binary_node<int>.Colour.black;
    binary_node<int>.Colour red = binary_node<int>.Colour.red;

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
        if (parent.Colors == black) { return; }
        binary_node<int> grand_parent = path_node[path_node.Count - 3];
        if (parent.Colors == red)
        {
            binary_node<int>.Colour side_color=black;
            binary_node<int> side = new binary_node<int>(0);
            (side_color,side)= sibling_color(parent, grand_parent);
            if (side_color == black) 
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
                temp_child.Colors = black;
                if (temp_child.left.data == grand_parent.data) { temp_child.left.Colors = red; }
                else { temp_child.right.Colors = red; }
                root.Colors = black;

            }
            else
            {
                side.Colors = black;
                grand_parent.Colors = red;
                parent.Colors = black;
                root.Colors = black;
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
                if (temp.right.Colors == red && temp.Colors == red) { check_conditions(temp.right); return; }
                else { all_nodes.Push(temp.right); }
            }
            else if (temp.left != null)
            {
                if (temp.left.Colors ==red && temp.Colors == red) { check_conditions(temp.left); return; }
                else { all_nodes.Push(temp.left); }
            }
        }
    }

    private (binary_node<int>.Colour, binary_node<int>) sibling_color(binary_node<int> temp, binary_node<int> parent)
    {
        binary_node<int> side;
        if (parent.left == temp) { side = parent.right; }
        else { side = parent.left; }
        if (side == null) { return (black,null); }
        else { return (side.Colors,side); }
    }
    public override void remove(int data)
    {
        List<binary_node<int>> temp_path = path(new binary_node<int>(data));
        remove_conditions(temp_path);
    }

    private void remove_conditions(List<binary_node<int>> temp_path)
    {
        binary_node<int> main_node = temp_path[temp_path.Count - 1];
        if (height(main_node) == 0)
        {
            if (main_node == root) { root = null; return; }
            else if (main_node.Colors == red) { main_node = null; return; }
        }
        else
        {
            int temp_data = min_value(main_node.right);
            binary_node<int> temp_node = main_node.right;
            if (main_node.data == temp_data) { temp_data = max_value(main_node.left); temp_node = main_node.left; }
            remove_conditions(path(temp_node));
            main_node.data = temp_data;
            return;
        }
        remove_conditions(temp_path, main_node);
        main_node = null;
    }

    private void remove_conditions(List<binary_node<int>> temp_path, binary_node<int> main_node)
    {
        if (main_node == root) { main_node.Colors = black;return; }
        binary_node<int>.Colour side_color = black;
        binary_node<int> parent = temp_path[temp_path.Count - 2];
        binary_node<int> grand_parent = temp_path[temp_path.Count - 2];
        binary_node<int> side = new binary_node<int>(0);
        (side_color, side) = sibling_color(main_node, temp_path[temp_path.Count - 2]);
        if (side_color == black && ((side.left == null) || side.left.Colors == black) && ((side.right == null) || side.right.Colors == black))
        {
            if (parent.Colors == red)
            {
                main_node.Colors = black;
                parent.Colors = black;
                side.Colors = red;
                return;
            }
            else
            {
                side.Colors = red;
                main_node.Colors = black;
                remove_conditions(path(parent), parent);
            }
        }
        else if (side_color == red)
        {
            binary_node<int> temp;
            parent.Colors = red;
            side.Colors = black;
            if (parent.left == main_node) { temp = right_right(parent); }
            else { temp = left_left(parent); }
            if (parent == root) { temp = root; }
            main_node.Colors = black;
            remove_conditions(path(main_node), main_node);
        }
        else if (side_color == black)
        {
            side.Colors = red;
            binary_node<int> parent_side;
            binary_node<int> away;
            binary_node<int> close;
            binary_node<int> temp;
            if (grand_parent.left == parent)
            {
                parent_side = grand_parent.right;
                close = parent_side.left;
                away = parent_side.right;
            }
            else
            {
                parent_side = grand_parent.left;
                close = parent_side.right;
                away = parent_side.left;
            }
            if (away.Colors == black && close.Colors == red)
            {
                close.Colors = black;
                parent_side.Colors = red;
                if (grand_parent.left == parent) { left_left(parent_side); }
                else { right_right(parent_side); }
            }
            parent_side.Colors = grand_parent.Colors;
            grand_parent.Colors = black;
            if (grand_parent.left == parent) { temp = right_right(grand_parent); }
            else { temp = left_left(grand_parent); }
            if (grand_parent == root) { temp = root; }
            away.Colors = black;
        }
    }
}
