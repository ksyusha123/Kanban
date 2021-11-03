using System.Collections.Generic;

namespace Domain
{
    public class Table
    {
        private IReadOnlyCollection<ITask> Tasks { get; set; }
        private IReadOnlyCollection<IExecutor> Team { get; set; }
        private IReadOnlyCollection<IExecutor> Readers { get; set; }
        private IReadOnlyCollection<IExecutor> Commentators { get; set; }
        private IReadOnlyCollection<IExecutor> Writers { get; set; }
    }
}