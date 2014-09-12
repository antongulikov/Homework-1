﻿using System;
using System.Collections.Generic;


namespace _1tree
{
    /// <summary>
    /// Class Tree.
    /// </summary>
    /// <typeparam name="ElementType"> Type of elements. </typeparam>
    public class Tree<ElementType> where ElementType : IComparable<ElementType>
    {
        public Tree()
        {   }

        /// <summary>
        /// Add new element.
        /// </summary>
        /// <param name="newValue"> Value of new element. </param>
        public void Add(ElementType newValue)
        {
            if (root == null)
                root = new ElementTree(newValue);
            else
                root.AddElement(newValue);
        }

        /// <summary>
        /// Delete element.
        /// </summary>
        /// <param name="deleteValue"> Value of deleted element. </param>
        public void Delete(ElementType deleteValue)
        {
            if (root == null)
                return;
            root = root.DeleteElement(deleteValue);
        }

        /// <summary>
        /// Is exist value.
        /// </summary>
        /// <param name="value"> Value of element. </param>
        /// <returns> True - if this value exists, false - otherwise. </returns>
        public bool IsExist(ElementType value)
        {
            if (root == null)
                return false;
            return root.IsExist(value);
        }



        public IEnumerator<ElementType> GetEnumerator()
        {
            foreach (ElementType i in root.Values())
                yield return i;
        }


        private ElementTree root;

        /// <summary>
        /// Node of tree.
        /// </summary>
        private class ElementTree
        {
            public ElementTree(ElementType value, ElementTree left = null, ElementTree right = null)
            {
                this.value = value;
                this.left = left;
                this.right = right;
            }

            public ElementType Value
            {
                get { return value; }
            }

            public ElementTree Left
            {
                get { return left; }
            }

            public ElementTree Right
            {
                get { return right; }
            }

            /// <summary>
            /// Return array of all elements in tree.
            /// </summary>
            /// <returns> Array of values. </returns>
            public ElementType[] Values()
            {
                ElementType[] mas;
                if (left != null)
                {
                    if (right != null)
                        mas = new ElementType[left.Values().Length + right.Values().Length + 1];
                    else
                        mas = new ElementType[left.Values().Length + 1];
                    left.Values().CopyTo(mas, 0);
                    mas[left.Values().Length] = value;
                    if (right != null)
                        right.Values().CopyTo(mas, left.Values().Length + 1);
                }
                else 
                {
                    if (right != null)
                    {
                        mas = new ElementType[right.Values().Length + 1];
                        right.Values().CopyTo(mas, 1);
                    }
                    else
                        mas = new ElementType[1];

                    mas[0] = value;
                }
                
                return mas;
            }


            /// <summary>
            /// Add new element.
            /// </summary>
            /// <param name="newValue"> Value of new element. </param>
            public void AddElement(ElementType newValue)
            {
                if (value.CompareTo(newValue) == 0)
                    return;

                if (value.CompareTo(newValue) > 0)
                    if (left != null)
                        left.AddElement(newValue);
                    else
                        left = new ElementTree(newValue);
                else
                    if (right != null)
                        right.AddElement(newValue);
                    else
                        right = new ElementTree(newValue);
            }

            /// <summary>
            /// Is exist.
            /// </summary>
            /// <param name="searchValue"> Value of element. </param>
            /// <returns> True - if this value exists, false - otherwise. </returns>
            public bool IsExist(ElementType searchValue)
            {
                if (value.CompareTo(searchValue) == 0)
                    return true;

                if (value.CompareTo(searchValue) > 0)
                    if (left != null)
                        return left.IsExist(searchValue);
                    else
                        return false;
                else
                    if (right != null)
                        return right.IsExist(searchValue);
                    else
                        return false;
            }

            /// <summary>
            /// Delete element.
            /// </summary>
            /// <param name="deleteValue"> Value of deleted element. </param>
            /// <rereturns> New root. </rereturns>
            public ElementTree DeleteElement(ElementType deleteValue)
            {
                if (value.CompareTo(deleteValue) == 0)
                    return DeleteElement(this);

                else if (value.CompareTo(deleteValue) > 0 && left != null)
                    if (left.value.CompareTo(deleteValue) == 0)
                        left = DeleteElement(left);
                    else
                        left.DeleteElement(deleteValue);
                else if (right != null)
                    if (right.value.CompareTo(deleteValue) == 0)
                        right = DeleteElement(right);
                    else
                        right.DeleteElement(deleteValue);
                return this;
            }

            /// <summary>
            /// Delete element which is right or left for this.
            /// </summary>
            /// <param name="deleteElement"> Deleted element which is left or right for this. </param>
            /// <returns> What u have put instead deleted element. </returns>
            private ElementTree DeleteElement(ElementTree deleteElement)
            {
                if (deleteElement.left == null && deleteElement.right == null)
                    return null;

                if (deleteElement.left != null)
                {
                    ElementTree temp = deleteElement.left;
                    ElementTree newRoot = temp;
                    if (temp.right!= null)
                    {
                        while (temp.right.right != null)
                            temp = temp.right;
                        newRoot = temp.right;
                        temp.right = temp.right.left;
                        newRoot.left = deleteElement.left;
                     }
                    newRoot.right = deleteElement.right;
                     return newRoot;   
                }
                else
                {
                    ElementTree temp = deleteElement.right;
                    ElementTree newRoot = temp;
                    if (temp.left != null)
                    {
                        while (temp.left.left != null)
                            temp = temp.left;
                        newRoot = temp.left;
                        temp.left = temp.left.right;
                        newRoot.right = deleteElement.right;
                    }
                    newRoot.left = deleteElement.left;
                    return newRoot;   
                }
            }

            private ElementType value;
            private ElementTree left;
            private ElementTree right;
        }
    }
}