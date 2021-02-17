using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable IsExpressionAlwaysTrue

namespace Inheritance.DataStructure
{
    [TestFixture]
    public class Category_should
    {
        Category A11 = new Category("A", MessageType.Incoming, MessageTopic.Subscribe);
        Category A21 = new Category("A", MessageType.Outgoing, MessageTopic.Subscribe);
        Category A12 = new Category("A", MessageType.Incoming, MessageTopic.Error);
        Category B11 = new Category("B", MessageType.Incoming, MessageTopic.Subscribe);

        Category[] Descending()
        {
            return new[] { A11, A12, A21, B11 };
        }

        Category A11_copy = new Category("A", MessageType.Incoming, MessageTopic.Subscribe);

        [Test]
        public void ImplementEqualsCorrectly()
        {
            var a = Descending();
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < a.Length; j++)
                    Assert.AreEqual(i == j, a[i].Equals(a[j]), $"Error on {i} {j}");
            Assert.True(A11.Equals(A11_copy));
        }
        
        [Test]
        public void ImplementGetHashCodeCorrectly()
        {
            var a = Descending();
            for (int i = 0; i < a.Length; i++)
            for (int j = 0; j < a.Length; j++)
                if (i != j)
                {
                    Assert.AreNotEqual(a[i].GetHashCode(), a[j].GetHashCode(), $"Error on {i} {j}");
                    // Обычно от хеш-функции не требуется,
                    // чтобы она возвращала разные значения на разных объектах.
                    // Однако в этой задаче вам нужно сделать так,
                    // чтобы на этом тесте разные объекты возвращали разные значений хеш-функций.
                }

            Assert.True(A11.Equals(A11_copy));
        }

        [Test]
        public void ImplementCompareToCorrectly()
        {
            var a = Descending();
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < a.Length; j++)
                    Assert.AreEqual(Math.Sign(i.CompareTo(j)), Math.Sign(a[i].CompareTo(a[j])), $"Error on {i} {j}");
            Assert.AreEqual(0, A11.CompareTo(A11_copy));
        }

        [Test]
        public void ImplementOperatorsCorrectly()
        {
            var a = Descending();
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < a.Length; j++)
                {
                    Assert.AreEqual(i <= j, a[i] <= a[j], $"Error on <=, {i} {j}");
                    Assert.AreEqual(i >= j, a[i] >= a[j], $"Error on >=, {i} {j}");
                    Assert.AreEqual(i < j, a[i] < a[j], $"Error on <, {i} {j}");
                    Assert.AreEqual(i > j, a[i] > a[j], $"Error on >, {i} {j}");
                    Assert.AreEqual(i == j, a[i] == a[j], $"Error on ==, {i} {j}");
                    Assert.AreEqual(i != j, a[i] != a[j], $"Error on !=, {i} {j}");
                }
        }

        [Test]
        public void ImplementIComparableInterface()
        {
            Assert.True(A11 is IComparable);
        }

        [Test]
        public void ImplementToStringCorrectly()
        {
            Assert.AreEqual("A.Incoming.Subscribe", A11.ToString());
        }
        [Test]
        public void GreaterThanWithNullString()
        {
            Assert.AreEqual(true, new Category(null, MessageType.Incoming, MessageTopic.Error) > new Category("N", MessageType.Incoming, MessageTopic.Error));
        }

        [Test]
        public void GreaterThanOrEqualWithNullString()
        {
            Assert.AreEqual(true, new Category(null, MessageType.Incoming, MessageTopic.Error) >= new Category("N", MessageType.Incoming, MessageTopic.Error));
            Assert.AreEqual(true, new Category(null, MessageType.Incoming, MessageTopic.Error) >= new Category(null, MessageType.Incoming, MessageTopic.Error));
        }

        [Test]
        public void GreaterThanlWithNull()
        {
            Assert.AreEqual(true, null > A11);
            Assert.AreEqual(false, A11 > null);
        }

        [Test]
        public void GreaterThanOrEqualWithNull()
        {
            Assert.AreEqual(true, null >= A11);
            Assert.AreEqual(false, A11 >= null);
        }

        [Test]
        public void CompareToWithNullString()
        {
            Assert.AreEqual(1, new Category(null, MessageType.Incoming, MessageTopic.Error).CompareTo(new Category("N", MessageType.Incoming, MessageTopic.Error)));
            Assert.AreEqual(-1, new Category("N", MessageType.Incoming, MessageTopic.Error).CompareTo(new Category(null, MessageType.Incoming, MessageTopic.Error)));
        }

        [Test]
        public void CompareToNull()
        {
            Assert.AreEqual(-1, new Category("N", MessageType.Incoming, MessageTopic.Error).CompareTo(null));
            Assert.AreEqual(-1, new Category(null, MessageType.Incoming, MessageTopic.Error).CompareTo(null));
        }

        [Test]
        public void EqualsNull()
        {
            Assert.AreEqual(false, new Category("N", MessageType.Incoming, MessageTopic.Error).Equals(null));
        }
    }
}
