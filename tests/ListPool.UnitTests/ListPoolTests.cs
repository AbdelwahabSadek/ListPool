using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Xunit;

namespace ListPool.UnitTests
{
    public class ListPoolTests
    {
        private static readonly Fixture s_fixture = new Fixture();

        [Fact]
        public void Add_item_without_indicate_capacity_of_list()
        {
            int expectedItem = s_fixture.Create<int>();
            using var sut = new ListPool<int>
            {
                expectedItem
            };

            Assert.Equal(expectedItem, sut[0]);
        }

        [Fact]
        public void Add_items_when_capacity_is_full_then_buffer_autogrow()
        {
            using var sut = new ListPool<int>(128);
            var expectedItems = s_fixture.CreateMany<int>(sut.Capacity * 2).ToList();

            foreach (int expectedItem in expectedItems)
            {
                sut.Add(expectedItem);
            }

            Assert.Equal(expectedItems.Count, sut.Count);
            Assert.True(expectedItems.All(expectedItem => sut.Contains(expectedItem)));
        }

        [Fact]
        public void Contains_return_true_when_item_exists()
        {
            int expectedAt0 = s_fixture.Create<int>();
            int expectedAt1 = s_fixture.Create<int>();
            int expectedAt2 = s_fixture.Create<int>();
            int unexpected = s_fixture.Create<int>();

            using var sut = new ListPool<int>(3) { expectedAt0, expectedAt1, expectedAt2 };

            Assert.Contains(expectedAt0, sut);
            Assert.Contains(expectedAt1, sut);
            Assert.Contains(expectedAt2, sut);
            Assert.DoesNotContain(unexpected, sut);
        }

        [Fact]
        public void CopyTo_copy_all_elements_to_target_array()
        {
            int expectedAt0 = s_fixture.Create<int>();
            int expectedAt1 = s_fixture.Create<int>();
            int expectedAt2 = s_fixture.Create<int>();
            using var sut = new ListPool<int>(3) {expectedAt0, expectedAt1, expectedAt2};
            int[] array = new int[3];

            sut.CopyTo(array, 0);

            Assert.Equal(sut.Count, array.Length);
            Assert.Contains(expectedAt0, array);
            Assert.Contains(expectedAt1, array);
            Assert.Contains(expectedAt2, array);
        }

        [Fact]
        public void Count_property_is_for_items_Added_count_not_capacity_of_buffer()
        {
            const int listCapacity = 10;
            const int expectedItemsCount = 3;

            using var sut = new ListPool<int>(listCapacity) { 1, 2, 3 };

            Assert.Equal(expectedItemsCount, sut.Count);
        }

        [Fact]
        public void Create_list_and_add_values()
        {
            int expectedAt0 = s_fixture.Create<int>();
            int expectedAt1 = s_fixture.Create<int>();
            int expectedAt2 = s_fixture.Create<int>();

            using var sut = new ListPool<int>(3) { expectedAt0, expectedAt1, expectedAt2 };

            Assert.Equal(expectedAt0, sut[0]);
            Assert.Equal(expectedAt1, sut[1]);
            Assert.Equal(expectedAt2, sut[2]);
        }

        [Fact]
        public void Create_list_and_add_values_after_clear()
        {
            using var sut =
                new ListPool<int>(3) {s_fixture.Create<int>(), s_fixture.Create<int>(), s_fixture.Create<int>()};

            sut.Clear();

            Assert.Empty(sut);
        }

        [Fact]
        public void Create_without_parameters_should_add_and_get_items()
        {
            const int expectedItemsCount = 3;
            int expectedAt0 = s_fixture.Create<int>();
            int expectedAt1 = s_fixture.Create<int>();
            int expectedAt2 = s_fixture.Create<int>();

            using var sut = new ListPool<int> { expectedAt0, expectedAt1, expectedAt2 };

            Assert.Equal(expectedAt0, sut[0]);
            Assert.Equal(expectedAt1, sut[1]);
            Assert.Equal(expectedAt2, sut[2]);
            Assert.Equal(expectedItemsCount, sut.Count);
        }

        [Fact]
        public void Get_item_with_index_above_itemsCount_throws_ArgumentOutOfRangeException()
        {
            const int index = 2;
            using var sut = new ListPool<int> {s_fixture.Create<int>()};

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut[index]);

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void Get_item_with_index_bellow_zero_throws_ArgumentOutOfRangeException()
        {
            int index = -1;
            var sut = new ListPool<int>();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut[index]);

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void IndexOf_returns_index_of_item()
        {
            int expectedAt0 = s_fixture.Create<int>();
            int expectedAt1 = s_fixture.Create<int>();
            int expectedAt2 = s_fixture.Create<int>();
            using var sut = new ListPool<int>(3) {expectedAt0, expectedAt1, expectedAt2};

            Assert.Equal(0, sut.IndexOf(expectedAt0));
            Assert.Equal(1, sut.IndexOf(expectedAt1));
            Assert.Equal(2, sut.IndexOf(expectedAt2));
        }

        [Fact]
        public void Insert_at_existing_index_move_items_up()
        {
            int[] expectedItems = s_fixture.CreateMany<int>(3).ToArray();
            int expectedItemAt1 = s_fixture.Create<int>();
            int expectedItemsCount = expectedItems.Length + 1;
            using var sut = expectedItems.ToListPool();

            sut.Insert(1, expectedItemAt1);

            Assert.Equal(expectedItemsCount, sut.Count);
            Assert.Equal(expectedItems[0], sut[0]);
            Assert.Equal(expectedItemAt1, sut[1]);
            Assert.Equal(expectedItems[1], sut[2]);
            Assert.Equal(expectedItems[2], sut[3]);
        }

        [Fact]
        public void Insert_at_the_end_add_new_item()
        {
            int expectedAt3 = s_fixture.Create<int>();
            using var sut =
                new ListPool<int>(4) {s_fixture.Create<int>(), s_fixture.Create<int>(), s_fixture.Create<int>()};

            sut.Insert(3, expectedAt3);

            Assert.Equal(4, sut.Count);
            Assert.Equal(expectedAt3, sut[3]);
        }

        [Fact]
        public void Insert_item_with_index_above_itemsCount_throws_ArgumentOutOfRangeException()
        {
            const int index = 2;
            using var sut = new ListPool<int> {s_fixture.Create<int>()};
            int item = s_fixture.Create<int>();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Insert(index, item));

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void Insert_item_with_index_bellow_zero_throws_ArgumentOutOfRangeException()
        {
            const int index = -1;
            int item = s_fixture.Create<int>();
            using var sut = new ListPool<int>();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Insert(index, item));

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void Insert_items_when_capacity_is_full_then_buffer_autogrow()
        {
            using var sut = new ListPool<int>(128);
            var expectedItems = s_fixture.CreateMany<int>(sut.Capacity * 2).ToList();
            int index = 0;

            foreach (int expectedItem in expectedItems)
            {
                sut.Insert(index++, expectedItem);
            }

            Assert.Equal(expectedItems.Count, sut.Count);
            Assert.True(expectedItems.All(expectedItem => sut.Contains(expectedItem)));
        }

        [Fact]
        public void Insert_without_indicating_capacity_of_list()
        {
            const int index = 0;
            int expectedItem = s_fixture.Create<int>();
            using var sut = new ListPool<int>();

            sut.Insert(index, expectedItem);

            Assert.Equal(expectedItem, sut[0]);
        }

        [Fact]
        public void Readonly_property_is_always_false()
        {
            using var sut = new ListPool<int>();

            Assert.False(sut.IsReadOnly);
        }

        [Fact]
        public void Remove_item_that_doesnt_exists_return_false()
        {
            string item = s_fixture.Create<string>();
            using var sut = new ListPool<string> {s_fixture.Create<string>()};

            Assert.False(sut.Remove(item));
            Assert.Single(sut);
        }

        [Fact]
        public void Remove_when_item_exists_remove_item_and_decrease_itemsCount()
        {
            const int expectedCountAfterRemove = 2;
            int expectedAt0 = s_fixture.Create<int>();
            using var sut = new ListPool<int>(3) { expectedAt0, s_fixture.Create<int>(), s_fixture.Create<int>() };

            bool wasRemoved = sut.Remove(expectedAt0);

            Assert.True(wasRemoved);
            Assert.DoesNotContain(expectedAt0, sut);
            Assert.Equal(expectedCountAfterRemove, sut.Count);
        }

        [Fact]
        public void Remove_when_item_is_null_return_false()
        {
            string item = null;
            using var sut = new ListPool<string>();

            Assert.False(sut.Remove(item));
        }

        [Fact]
        public void RemoveAt_when_item_exists_remove_item_and_decrease_itemsCount()
        {
            const int expectedCountAfterRemove = 2;
            int expectedAt1 = s_fixture.Create<int>();
            using var sut = new ListPool<int>(3) { s_fixture.Create<int>(), expectedAt1, s_fixture.Create<int>() };

            sut.RemoveAt(1);

            Assert.DoesNotContain(expectedAt1, sut);
            Assert.Equal(expectedCountAfterRemove, sut.Count);
        }

        [Fact]
        public void RemoveAt_with_index_above_itemsCount_throws_ArgumentOutOfRangeException()
        {
            const int index = 2;
            using var sut = new ListPool<int> { s_fixture.Create<int>() };

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut.RemoveAt(index));

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void RemoveAt_with_index_bellow_zero_throws_ArgumentOutOfRangeException()
        {
            const int index = -1;
            using var sut = new ListPool<int>();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut.RemoveAt(index));

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void RemoveAt_with_index_zero_when_not_item_added_throws_ArgumentOutOfRangeException()
        {
            const int index = 0;
            using var sut = new ListPool<int>();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut.RemoveAt(index));

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void Set_at_existing_index_update_item()
        {
            const int expectedItemsCount = 3;
            int expectedItem = s_fixture.Create<int>();
            using var sut =
                new ListPool<int>(3) {s_fixture.Create<int>(), s_fixture.Create<int>(), s_fixture.Create<int>()};

            sut[2] = expectedItem;

            Assert.Equal(expectedItemsCount, sut.Count);
            Assert.Equal(expectedItem, sut[2]);
        }

        [Fact]
        public void Set_item_with_index_above_itemsCount_throws_ArgumentOutOfRangeException()
        {
            const int index = 2;
            using var sut = new ListPool<int> {s_fixture.Create<int>()};
            int item = s_fixture.Create<int>();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut[index] = item);

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void Set_item_with_index_bellow_zero_throws_ArgumentOutOfRangeException()
        {
            const int index = -1;
            int item = s_fixture.Create<int>();
            var sut = new ListPool<int>();

            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => sut[index] = item);

            Assert.Equal(nameof(index), exception.ParamName);
        }

        [Fact]
        public void Enumerate_when_capacity_is_not_set_dont_throw_exception()
        {
            using var sut = new ListPool<int>();

            foreach (int _ in sut)
            {
                
            }
        }

        [Fact]
        public void IndexOf_empty_ListPool_without_indicating_capacity_returns_negative_one()
        {
            int randomItem = s_fixture.Create<int>();
            const int expected = -1;
            using var sut = new ListPool<int>();

            int actual = sut.IndexOf(randomItem);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Contains_empty_ListPool_without_indicating_capacity_returns_false()
        {
            int randomItem = s_fixture.Create<int>();
            using var sut = new ListPool<int>();

            bool actual = sut.Contains(randomItem);

            Assert.False(actual);
        }
    }
}
