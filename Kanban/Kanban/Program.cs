namespace Kanban
{
    internal static class Program
    {
        private static void Main(string[] args) => ConfigureContainer().GetInstance<TelegramBot>();

        private static Container ConfigureContainer()
        {
            var container = new Container();
            container.RegisterSingleton<IConfiguration>(() =>
                new ConfigurationBuilder()
                    .AddJsonFile(
                        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,
                            "config.json"),
                        true)
                    .Build());
            container.Register(() =>
                new TrelloClient(container.GetInstance<IConfiguration>().GetSection("token").Value,
                    container.GetInstance<IConfiguration>().GetSection("api-key").Value));
            container.RegisterApplications();
            container.RegisterCommands();
            container.Register(() => new DbContextOptionsBuilder<KanbanDbContext>()
                .UseNpgsql(container.GetInstance<IConfiguration>().GetSection("connectionString").Value)
                .Options);
            container.Register<KanbanDbContext>();
            container.Register<IRepository<Board, string>, Repository<Board, string>>();
            container.Register<IRepository<Chat, long>, Repository<Chat, long>>();
            container.Register<IRepository<Card, string>, Repository<Card, string>>();
            container.Register<IRepository<Executor, Guid>, Repository<Executor, Guid>>();
            container.Register<IDateTimeProvider, StandardDateTimeProvider>();
            container.Register<TelegramBot>();
            container.RegisterInitializer<TelegramBot>(bot => bot.Start());

            container.Register<ChatInteractor>();
            return container;
        }

        private static void RegisterCommands(this Container container) =>
            container.Collection.Register<ICommand>(Assembly.GetCallingAssembly());

        private static void RegisterApplications(this Container container) =>
            container.Collection.Register<IApplication>(typeof(IApplication).Assembly);
    }
}