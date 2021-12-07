using System.Collections.Generic;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace WebParcer.Data.DataProviders
{
    public class GroupProvider
    {
        readonly VkApi vk;
        public GroupProvider(VkApi vk)
        {
            this.vk = vk;
        }

        /// <summary>
        /// Возвращает группы по запросу с минимумом данных
        /// </summary>
        /// <param name="query">ключевое слово для поиска</param>
        /// <param name="count">количество</param>
        /// <returns></returns>
        public VkCollection<Group> GetEmptyGroups(string query, int count)
        {
            GroupsSearchParams searchParams = new()
            {
                Query = query,
                CityId = 1,
                CountryId = 1,
                Count = count,
                Type = GroupType.Page,
                Future = false,
                Market = false,
                Offset = 0,
                Sort = GroupSort.Likes
            };
            return vk.Groups.Search(searchParams);
        }

        /// <summary>
        /// Наполняет коллекцию групп максимально полной информацией
        /// </summary>
        /// <param name="groups"> Коллекция пустых групп</param>
        /// <returns></returns>
        public IEnumerable<Group> GetFullGroups(VkCollection<Group> groups)
        {
            if (groups.Count <= 500)
            {
                List<string> ids = new();
                foreach (var group in groups)
                {
                    ids.Add(group.Id.ToString());
                }
                return vk.Groups.GetById(ids, null, GroupsFields.All);
            }
            else
            {
                List<string> ids = new();
                List<string> idsRest = new();
                for (int i = 0; i < 500; i++)
                {
                    ids.Add(groups[i].Id.ToString());
                }
                for (int i = 500; i < groups.Count; i++)
                {
                    idsRest.Add(groups[i].Id.ToString());
                }
                var firstPart = vk.Groups.GetById(ids, null, GroupsFields.All);
                var secondPart = vk.Groups.GetById(idsRest, null, GroupsFields.All);
                return firstPart.Union<Group>(secondPart);
            }
        }
    }
}
