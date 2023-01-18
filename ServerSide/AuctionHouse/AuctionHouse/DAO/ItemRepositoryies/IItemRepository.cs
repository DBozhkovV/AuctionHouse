﻿using AuctionHouse.Models;

namespace AuctionHouse.DAO.ItemRepositoryies
{
    public interface IItemRepository
    {
        void AcceptItem(Guid id); // update the accepted item

        void BuyItem(Guid itemId); // update bought item

        void DeleteItemById(Guid id);

        IEnumerable<Item> GetAvailableItems();

        Item GetItemById(Guid id);

        Item GetNotAcceptedItemById(Guid id);

        IEnumerable<Item> GetNotAcceptedItems();

        IEnumerable<Item> GetNotAvailableItems();

        IEnumerable<Item> GetSearchedItem(string search); // search is the keyword with which costumer search items

        User GetUserByGuid(Guid id);

        void InsertItem(Item item);

        void RejectItem(Guid id); // If item is reject then it is deleted from database
    }
}
