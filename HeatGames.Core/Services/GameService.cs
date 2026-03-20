using HeatGames.Data;
using HeatGamesWeb.Services.Interfaces;
using HeatGamesWeb.ViewModels;
namespace HeatGamesWeb.Services
{
    public class GameService : IGameService
    {
        private readonly HeatGamesDbContext _context;

        public GameService(HeatGamesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameViewModel>> GetAllGamesAsync()
        {
            // Взимаме игрите от базата и ги "мапваме" към ViewModel
            return await _context.Games
                .Select(g => new GameViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    Price = g.Price,
                    ReleaseDate = g.ReleaseDate,
                    CoverImageUrl = g.CoverImageUrl,
                    DeveloperId = g.DeveloperId
                })
                .ToListAsync();
        }

        public async Task<GameViewModel?> GetGameByIdAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return null;

            return new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                CoverImageUrl = game.CoverImageUrl,
                DeveloperId = game.DeveloperId
            };
        }

        public async Task CreateGameAsync(GameViewModel model)
        {
            // Създаваме ново Entity от данните на ViewModel-а
            var game = new Game
            {
                Id = model.Id, // Вече е генерирано в контролера
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ReleaseDate = model.ReleaseDate,
                CoverImageUrl = model.CoverImageUrl,
                DeveloperId = model.DeveloperId
            };

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateGameAsync(GameViewModel model)
        {
            var game = await _context.Games.FindAsync(model.Id);
            if (game == null) return false;

            // Обновяваме полетата
            game.Title = model.Title;
            game.Description = model.Description;
            game.Price = model.Price;
            game.ReleaseDate = model.ReleaseDate;
            game.CoverImageUrl = model.CoverImageUrl;
            game.DeveloperId = model.DeveloperId;

            _context.Games.Update(game);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteGameAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}