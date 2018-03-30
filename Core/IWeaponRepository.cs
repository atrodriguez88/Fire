using System.Collections.Generic;
using System.Threading.Tasks;
using Fire.Core.Models;
using Fire.Persistent.Models;

namespace Fire.Core
{
    public interface IWeaponRepository
    {
         Task<Weapon> GetWeapon(int id, bool? includeRelated = false);
         Task<IEnumerable<Weapon>> GetWeapons(Filters filters);
         void Add(Weapon weapon);
         void Remove(Weapon weapon);
    }
}