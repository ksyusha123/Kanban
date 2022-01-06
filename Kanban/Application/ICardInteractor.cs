﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface ICardInteractor
    {
        Task<Card> CreateCardAsync(string name, string boardId);
        Task EditCardNameAsync(string cardId, string name);
        Task AssignCardExecutor(string cardId, string executorId);
        Task ChangeColumn(string cardId, Column column);
        Task<IEnumerable<Card>> GetCardsAsync(string nameQuery, string boardId);
    }
}