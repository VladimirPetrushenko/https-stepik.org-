using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    public class Category : IComparable
    {
        string NameProduct { get; set; }
        MessageType MessageType { get; set; }
        MessageTopic MessageTopic { get; set; }
        public Category(string nameProduct, MessageType messageType, MessageTopic messageTopic)
        {
            NameProduct = nameProduct;
            MessageType = messageType;
            MessageTopic = messageTopic;
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
                return base.Equals(obj as Category);
            return false;
        }
        public bool Equals(Category category)
        {
            return category != null && NameProduct!=null && NameProduct.Equals(category.NameProduct)
                    && MessageTopic == category.MessageTopic
                    && MessageType == category.MessageType;
        }
        public override int GetHashCode()
        {
            return NameProduct.ToArray()[0] * 100 + (int)MessageType * 10 + (int)MessageTopic;
        }

        public override string ToString()
        {
            return NameProduct + "." + MessageType + "." + MessageTopic;
        }
        public int CompareTo(object obj)
        {
            if (obj != null)
                return CompareTo(obj as Category);
            return -1;
        }

        public int CompareTo(Category b)
        {
            if (b != null)
            {
                int result;
                if (NameProduct == null || b.NameProduct == null)
                    result = -String.Compare(NameProduct, b.NameProduct);
                else
                    result = String.Compare(NameProduct, b.NameProduct);
                if (result == 0)
                {
                    result = (int)MessageType - (int)b.MessageType;
                    if (result == 0)
                        result = (int)MessageTopic - (int)b.MessageTopic;
                }
                return result;
            }
            return -1;
        }

        public static bool operator <(Category a, Category b)
        {
            if (a == null)
                return false;
            if (b == null)
                return true;
            return a.CompareTo(b) < 0;
        }
        public static bool operator >(Category a, Category b)
        {
            if (a == null)
                return true;
            if (b == null)
                return false;
            return a.CompareTo(b) > 0;
        }
        public static bool operator >=(Category a, Category b)
        {
            if (a == null)
                return true;
            if (b == null)
                return false;
            return a.CompareTo(b) >= 0;
        }
        public static bool operator <=(Category a, Category b)
        {
            if (a == null)
                return false;
            if (b == null)
                return true;
            return a.CompareTo(b) <= 0;
        }
    }
}