using System.Collections.Generic;
using Domain;
using Kanban;
using Moq;
using NUnit.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FluentAssertions;

namespace Tests
{
    public class ChangeColumnsCommandTests : CommandTests
    {
        private ChangeColumnsCommand _changeColumnsCommand;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _changeColumnsCommand = new ChangeColumnsCommand(Apps);
        }

        [Test]
        public void ChangeColumns_WhenNoCardsAndNoColumnsIntersection_RewritesColumns()
        {
            var message = new Message{ReplyToMessage = new Message{Text = "1\n2\n3"}};
            BoardRepository.Entities["1"] = new Board("1", "test", new List<Column> {new("A", 0)});
            Client.Setup(c => c.SendTextMessageAsync(It.IsAny<ChatId>(), It.IsAny<string>(), ParseMode.Default, false,
                false, 0, null, default));

            _changeColumnsCommand.ExecuteAsync(_chat, message, Client.Object);

            BoardRepository.Entities["1"].Columns.Should()
                .BeEquivalentTo(new List<Column> {new("1", 0), new("2", 1), new("3", 2)},
                    options => options.ComparingByMembers<Column>());
        }
    }
}