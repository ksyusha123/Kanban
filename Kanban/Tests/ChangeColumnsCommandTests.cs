using System.Collections.Generic;
using System.Linq;
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
            var message = new Message {ReplyToMessage = new Message {Text = "1\n2\n3"}};
            BoardRepository.Entities["1"] = new Board("1", "test", new List<Column> {new("A", 0)});
            Client.Setup(c => c.SendTextMessageAsync(It.IsAny<ChatId>(), It.IsAny<string>(), ParseMode.Default, false,
                false, 0, null, default));

            _changeColumnsCommand.ExecuteAsync(_chat, message, Client.Object);

            BoardRepository.Entities["1"].Columns.Select(c => new {c.Name, c.OrderNumber}).Should()
                .BeEquivalentTo(
                    new List<dynamic>
                    {
                        new {Name = "1", OrderNumber = (double) 0},
                        new {Name = "2", OrderNumber = (double) 1},
                        new {Name = "3", OrderNumber = (double) 2}
                    });
        }
    }
}