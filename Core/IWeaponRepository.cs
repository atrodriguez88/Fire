using System.Threading.Tasks;
using Fire.Persistent.Models;

namespace Fire.Core
{
    public interface IWeaponRepository
    {
         Task<Weapon> GetWeapon(int id, bool? includeRelated = false);
         void Add(Weapon weapon);
         void Remove(Weapon weapon);
    }
}