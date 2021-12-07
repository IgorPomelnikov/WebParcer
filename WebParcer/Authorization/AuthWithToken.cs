using VkNet;
using VkNet.Model;
using WebParcer.Interfaces;

namespace WebParcer.Authorization
{
    /// <summary>
    /// Авторизует с помощью токена, который захардкожен в конструкторе, класс использовался для работы в IDE во время разработки
    /// </summary>
    public class AuthWithToken : IAuthable
    {
        public VkApi Vk { get; }

        public AuthWithToken()
        {
            var vk = new VkApi();
            vk.Authorize(new ApiAuthParams
            {
                //ссылка для получения токена. её вставляем в браузер, де авторизован вк
                //https://oauth.vk.com/authorize?client_id=8016142&display=page&scope=groups&response_type=token&v=5.131

                AccessToken = "97b494a237af207547ecf0dfe3fe60a337c8941cdbc744a6b24dda1ccdf4d028036e4435ebf572c896958"
            });
            Vk = vk;
        }
    }
}
