using System.Collections.Generic;

namespace Domain
{
    public class Project
    {
        private IReadOnlyCollection<Table> Tables { get; set; }
    }
}