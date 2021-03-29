using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_black_tree : binary_search_tree
{
    binary_node<int>.Colour black = binary_node<int>.Colour.black;
    binary_node<int>.Colour red = binary_node<int>.Colour.red;

    public red_black_tree(int data) : base(data) { }
    public red_black_tree() { }
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
        //print(parent.data + "   " + temp.data);
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
        if (main_node == root && height(main_node) == 1) { root = null; return; }
        else if (main_node.Colors == red && height(main_node) == 1)
        {
            binary_node<int> base_node = temp_path[temp_path.Count - 2];
            int temp_data = main_node.data;
            if (base_node.data > temp_data) { base_node.left = null; }
            else { base_node.right = null; }
            main_node = null; return; 
        }
        else
        {
            int temp_data=0;
            binary_node<int> temp_node=null;
            if (height(main_node) > 1 && main_node.right != null) { temp_data = min_value(main_node.right); temp_node = main_node.right; }
            if (temp_node == null && main_node.left != null) { temp_data = max_value(main_node.left); temp_node = main_node.left; }
            if (height(main_node) == 1) { temp_node = main_node; temp_data = main_node.data; }
            if (height(main_node) > 1) { remove_conditions(path(new binary_node<int>(temp_data))); main_node.data = temp_data; return; }
            remove_conditions(path(temp_node),temp_node);
            List<binary_node<int>> temp_path_remove = path(new binary_node<int>(temp_data));
            binary_node<int> base_node;
            if (temp_path_remove.Count < 2) { root = temp_path_remove[0]; base_node = root; }
            else base_node = temp_path_remove[temp_path_remove.Count - 2];
            if (base_node.data > temp_data) { base_node.left = null; }
            else { base_node.right = null; }
            return;
        }
    }

    private void remove_conditions(List<binary_node<int>> temp_path, binary_node<int> main_node)
    {
        if (main_node == root) { main_node.Colors = black;return; }
        binary_node<int>.Colour side_color = black;
        binary_node<int> parent = temp_path[temp_path.Count - 2];
        binary_node<int> grand_parent;
        binary_node<int> side = new binary_node<int>(0);
        if (parent == root) { grand_parent = null; }
        else { grand_parent = temp_path[temp_path.Count - 3]; }

        (side_color, side) = sibling_color(main_node, temp_path[temp_path.Count - 2]);
        bool first_cond = false;
        if (side == null) { first_cond = true; }
        else if (side_color == black)
        {
            if ((side.left == null) && (side.right == null)) { first_cond = true; }
            else if (side.left != null)
            {
                if ((side.right == null) && (side.left.Colors == black)) { first_cond = true; }
            }
            if (side.right != null)
            {
                if ((side.left == null) && (side.right.Colors == black)) { first_cond = true; }
                else if ((side.right.Colors == black) && (side.left.Colors == black)) { first_cond = true; }
            }

        }
        if (first_cond)
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
            else if (grand_parent.data > temp.data) { grand_parent.left = temp; }
            else { grand_parent.right = temp; }
            main_node.Colors = black;
            root.Colors = black;
            remove_conditions(path(main_node), main_node);
        }
        else if (side_color == black)
        {
            side.Colors = red;
            binary_node<int> away;
            binary_node<int> close;
            binary_node<int> temp;
            if (parent.left == main_node)
            {
                close = side.left;
                away = side.right;
            }
            else
            {
                close = side.right;
                away = side.left;
            }
            if (away == null) { away = new binary_tree<int>.binary_node<int>(0); }
            if (close == null) { close = new binary_tree<int>.binary_node<int>(0); }
            if (away.Colors == black && close.Colors == red)
            {
                close.Colors = black;
                side.Colors = red;
                if (parent.left == side) { temp = right_right(side); }
                else { temp = right_right(side); }
                if (parent == root) { temp = root; }
                else if (parent.data > temp.data) { parent.left = temp; }
                else { parent.right = temp; }
                root.Colors = black;
                remove_conditions(path(main_node), main_node);
                return;
            }
            side.Colors = parent.Colors;
            parent.Colors = black;
            if (parent.left == side) { temp = left_left(parent); }
            else { temp = right_right(parent); }
            if (parent == root) { root = temp; }
            else if (grand_parent.data > temp.data) { grand_parent.left = temp; }
            else { grand_parent.right = temp; }
            away.Colors = black;
            root.Colors = black;
        }
    }
}
