using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {

        [Test]
        public void Test_Collection_EmptryConstructor()
        {
            var nums = new Collection<int>();

            Assert.That(nums.Count == 0, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[]");
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            var nums = new Collection<int>(3);

            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[3]");
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItem()
        {
            var nums = new Collection<int>(3, 7);

            Assert.That(nums.Count == 2, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[3, 7]");
        }

        [Test]
        public void Test_Collection_Add()
        {
            var nums = new Collection<int>();

            nums.Add(6);

            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[6]");
        }

        [Test]
        public void Test_Collection_ToStringEmptry()
        {
            var nums = new Collection<string>();

            Assert.That(nums.Count == 0, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[]");
        }

        [Test]
        public void Test_Collection_ToStringSingle()
        {
            var nums = new Collection<string>("QA");

            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[QA]");
        }

        [Test]
        public void Test_Collection_ToStringMultiple()
        {
            var nums = new Collection<string>("QA", "QA");

            Assert.That(nums.Count == 2, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[QA, QA]");
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");
            // Act
            var item0 = names[0];
            var item1 = names[1];
            // Assert
            Assert.That(item0, Is.EqualTo("Peter"));
            Assert.That(item1, Is.EqualTo("Maria"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");
            // Act
            var item0 = names[0];
            var item1 = names[1];
            // Assert
            Assert.That(item0, Is.EqualTo("Peter"));
            Assert.That(item1, Is.EqualTo("Maria"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Bob", "Joe");
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            var names = new Collection<string>("Bob", "Joe");
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Teddy", "Gerry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            Assert.That(nestedToString,
              Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_10000Items()
        {
            const int itemsCount = 10000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removed = names.RemoveAt(0);

            Assert.That(removed, Is.EqualTo("Peter"));
            Assert.That(names.ToString(), Is.EqualTo("[Maria, Steve, Mia]"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removed = names.RemoveAt(1);

            Assert.That(removed, Is.EqualTo("Maria"));
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Steve, Mia]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var names = new Collection<string>("Peter", "Maria", "Steve", "Mia");
            var removed = names.RemoveAt(3);

            Assert.That(removed, Is.EqualTo("Mia"));
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria, Steve]"));
        }


        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "Count and Capacity - 5 elements + 1 added")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "Count and Capacity - 7 elements + 1 added")]
        public void Count_and_Capacity(int[] values)
        {
            int Collection_Count = 0;
            int capacity = 0;

            Collection<int> nums = new Collection<int>(values);

            Collection_Count = nums.Count;
            capacity = nums.Capacity;

            for (int i = 0; i < 1000; i++)
            {
                nums.Add(1);
                Collection_Count++;
                if (Collection_Count == capacity)
                {
                    capacity = 2 * capacity;
                }
            }

            Assert.AreEqual(nums.Count, Collection_Count);
            Assert.AreEqual(nums.Capacity, capacity);

        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "InsertAtStart - 5 elements + 1 added")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "InsertAtStart - 7 elements + 1 added")]
        public void InsertAtStart(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int Collection_Count = nums.Count;
            int firstValue = nums[0];
            nums.InsertAt(0, 22);
            int insertedValue = nums[0];
            Assert.AreEqual(firstValue, 10);
            Assert.AreEqual(insertedValue, 22);

        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "InsertAtEnd - 5 elements + 1 added")]
        [TestCase(new int[] { 10, 22, 33, 44, 60, 80, 50 }, TestName = "InsertAtEnd - 7 elements + 1 added")]
        public void InsertAtEnd(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int Collection_Count = nums.Count;
            int lastValue = nums[Collection_Count - 1];
            int insertedValue = nums[Collection_Count - 1] + 10;
            nums.Add(lastValue + 10);
            Assert.AreEqual(insertedValue, 60);
            Assert.AreEqual(nums.Count, Collection_Count + 1);

        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "InsertWithGrowAtStart - 5 elements + 3 added")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "InsertWithGrowAtStart - 7 elements + 3 added")]
        public void InsertWithGrowAtStart(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int count = nums.Count;

            Random rnd = new Random();
            int max = nums[0];
            int random_number = rnd.Next(0, max);
            nums.InsertAt(0, random_number);
            Assert.That(nums[0] < nums[1]);
            Assert.AreEqual(nums.Count, count + 1);

        }


        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "InsertWithGrowAtEnd - 5 elements + 3 added")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "InsertWithGrowAtEnd - 7 elements + 3 added")]
        public void InsertWithGrowAtEnd(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int count = nums.Count;
            Random rnd = new Random();
            int min = nums[count - 1];
            int random_number = rnd.Next(min, min + 1000000);
            nums.Add(random_number);
            Assert.That(nums[count] > nums[count - 1]);
            Assert.AreEqual(nums.Count, count + 1);
        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60 }, TestName = "InsertWithGrowAtMiddle - 6 elements")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "InsertWithGrowAtMiddle - 7 elements")]
        public void InsertWithGrowAtMiddle(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int count = nums.Count;
            int middleIndex = 0;
            int min;
            int max;

            Random rnd = new Random();
            int random_number = 0;
            if (count % 2 == 1)
            {
                middleIndex = ((count + 1) / 2) - 1;
            }

            if (count % 2 == 0)
            {
                int lastIndexValue = nums[count - 1];
                random_number = rnd.Next(lastIndexValue, 1000000);
                nums.Add(random_number);
                count++;
                middleIndex = ((count + 1) / 2) - 1;
            }

            min = nums[middleIndex - 1];
            max = nums[middleIndex];
            random_number = rnd.Next(min, max);
            nums.InsertAt(middleIndex, random_number);
            Assert.That(nums[middleIndex] < nums[middleIndex + 1]);
            Assert.That(nums[middleIndex] >= nums[middleIndex - 1]);
            Assert.AreEqual(nums.Count, count + 1);


        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "Clear 5 elements ")]
        [TestCase(new int[] { }, TestName = "Clear-all")]
        public void Test_Collections_Clear(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            nums.Clear();
            Assert.AreEqual(nums.Count, 0);

        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "Exchange_First_Last - 5 elements - 1 removed")]
        [TestCase(new int[] { 10, 20, 30, 40, 60, 70, 50 }, TestName = "Exchange_First_Last - 7 elements - 1 removed")]
        public void Exchange_First_Last(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int Collection_Count = nums.Count;
            int firstElement = nums[0];
            int lastElement = nums[Collection_Count - 1];
            nums.Exchange(0, Collection_Count - 1);
            Assert.AreEqual(nums[Collection_Count - 1], firstElement);
            Assert.AreEqual(nums[0], lastElement);

        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60 }, TestName = "ExchangedMiddleFirst - 6 elements")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "ExchangedMiddleFirst - 7 elements")]
        public void ExcganedMiddleFirst(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int count = nums.Count;
            int firstIndexValue = nums[0];
            int middleIndex = 0;

            Random rnd = new Random();
            int random_number = 0;

            if (count % 2 == 1)
            {
                middleIndex = ((count + 1) / 2) - 1;
            }

            if (count % 2 == 0)
            {
                int lastIndexValue = nums[count - 1];
                random_number = rnd.Next(lastIndexValue, 1000000);
                nums.Add(random_number);
                count++;
                middleIndex = ((count + 1) / 2) - 1;
            }

            int middleIndexValue = nums[middleIndex];

            nums.Exchange(0, middleIndex);

            Assert.AreEqual(nums[middleIndex], firstIndexValue);
            Assert.AreEqual(nums[0], middleIndexValue);
            Assert.AreEqual(nums.Count, count);
        }


        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "GetElementByIndex - 5 elements - 1 removed")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "GetElementByIndex - 7 elements - 1 removed")]
        public void GetElementByIndex(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int Collection_Count = nums.Count;
            int i = 0;
            foreach (int value in values)
            {

                Assert.AreEqual(value, nums[i]);
                i++;

            }
        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "GetElementByInvalidIndex - 5 elements - 1 removed")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "GetElementByInvalidIndex - 7 elements - 1 removed")]
        public void GetElementByInvalidIndex(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int Collection_Count = nums.Count;

            Random rnd = new Random();
            int random_number;
            random_number = rnd.Next(0, 8);

            if (random_number > Collection_Count - 1)

            {

                Assert.That(random_number, Is.Not.InRange(0, Collection_Count - 1));

            }
        }

        [Test]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, TestName = "InsertAtInvalidIndex - 5 elements - 1 removed")]
        [TestCase(new int[] { 10, 20, 30, 40, 50, 60, 70 }, TestName = "InsertAtInvalidIndex - 7 elements - 1 removed")]
        public void InsertAtInvalidIndex(int[] values)
        {
            Collection<int> nums = new Collection<int>(values);
            int Collection_Count = nums.Count;

            Random rnd = new Random();
            int random_number;
            random_number = rnd.Next(0, 8);

            if (random_number > Collection_Count - 1)

            {

                Assert.That(random_number, Is.Not.InRange(0, Collection_Count - 1));

            }

            else 
            {
                nums.InsertAt(random_number, 25);
                Collection_Count++;
                int insertedValue = nums[random_number];
                Assert.AreEqual(insertedValue, 25);
                Assert.That(random_number, Is.InRange(0, Collection_Count - 1));

            }
        }
    }
}
