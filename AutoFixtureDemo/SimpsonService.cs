using System.Threading.Tasks;

namespace AutoFixtureDemo
{
    public class SimpsonService : ICartoonService
    {
        private readonly ILogger _logger;
        private readonly ICharacterRepository _characterRepository;
        private readonly IEmailService _emailService;

        public SimpsonService(
            ILogger logger, ICharacterRepository characterRepository, IEmailService emailService)
        {
            _logger = logger;
            _characterRepository = characterRepository;
            _emailService = emailService;
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching character with id {id}");
            return await _characterRepository.GetByIdAsync(id);
        }

        public async Task EmailCharacterByIdAsync(int id, string subject, string message)
        {
            var character = await GetCharacterByIdAsync(id);
            await _emailService.SendEmailAsync(character.Email, subject, message);
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public interface ICartoonService
    {
        Task<Character> GetCharacterByIdAsync(int id);
        Task EmailCharacterByIdAsync(int id, string subject, string message);
    }

    public interface ILogger
    {
        void LogInformation(string message);
    }

    public interface ICharacterRepository
    {
        Task<Character> GetByIdAsync(int id);
    }

    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message);
    }
}