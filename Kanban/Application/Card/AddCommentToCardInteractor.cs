﻿using System;
using Domain;

namespace Application
{
    public class AddCommentToCardInteractor
    {
        private readonly IRepository<Card, Guid> _cardRepository;

        public AddCommentToCardInteractor(IRepository<Card, Guid> cardRepository) => _cardRepository = cardRepository;

        public async System.Threading.Tasks.Task AddCommentToTaskAsync(Guid taskId, Comment comment)
        {
            var task = await _cardRepository.GetAsync(taskId);
            task.AddComment(comment);
            await _cardRepository.UpdateAsync(task);
        }
    }
}