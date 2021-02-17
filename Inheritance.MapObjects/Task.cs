using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    public class Dwelling : IOwner
    {
        public int Owner { get; set; }
    }

    public class Mine : IOwner, IArmy, ITreasure
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Creeps : IArmy, ITreasure
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Wolves : IArmy
    {
        public Army Army { get; set; }
    }

    public class ResourcePile : ITreasure
    {
        public Treasure Treasure { get; set; }
    }

    public static class Interaction
    {
        public static void Make(Player player, object mapObject)
        {
            if (mapObject is IArmy army)
            {
                if (player.CanBeat(army.Army))
                {
                    if (mapObject is IOwner owner)
                        owner.Owner = player.Id;
                    if (mapObject is ITreasure treasure)
                        player.Consume(treasure.Treasure);
                }
                else
                    player.Die();
            }
            else
            {
                if (mapObject is IOwner owner)
                    owner.Owner = player.Id;
                if (mapObject is ITreasure treasure)
                    player.Consume(treasure.Treasure);
            }
            return;
        }
    }
    interface IOwner
    {
        int Owner { get; set; }
    }
    interface IArmy
    {
        Army Army { get; set; }
    }
    interface ITreasure
    {
        Treasure Treasure { get; set; }
    }
    
}
