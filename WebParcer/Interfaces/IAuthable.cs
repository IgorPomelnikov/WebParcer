using VkNet;

namespace WebParcer.Interfaces
{
    /// <summary>
    /// Гарантирует наличие экземпляра VkApi
    /// </summary>
    public interface IAuthable
    {
        VkApi Vk { get; }
    }
}
