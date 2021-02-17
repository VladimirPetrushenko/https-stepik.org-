using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Terrain() };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            Player player = new Player();
            Monster monster = new Monster();
            return conflictedObject.GetType() == player.GetType() || conflictedObject.GetType() == monster.GetType();
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            Sack sack = new Sack();
            var (x0, y0) = (0, 0);
            (x0, y0) = KeysReturn(x, y);
            if (Game.Map[x + x0, y + y0]?.GetType() == sack.GetType())
                (x0, y0) = (0, 0);
            return new CreatureCommand { DeltaX = x0, DeltaY = y0, TransformTo = new Player() };
        }

        public static (int, int) KeysReturn(int x, int y)
        {
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Down:
                    if (y < Game.MapHeight - 1 && y >= 0)
                        return (0, 1);
                    break;
                case System.Windows.Forms.Keys.Up:
                    if (y <= Game.MapHeight && y > 0)
                        return (0, -1);
                    break;
                case System.Windows.Forms.Keys.Left:
                    if (x <= Game.MapWidth && x > 0)
                        return (-1, 0);
                    break;
                case System.Windows.Forms.Keys.Right:
                    if (x < Game.MapWidth - 1 && x >= 0)
                        return (1, 0);
                    break;
                default:
                    return (0, 0);
            }
            return (0, 0);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.GetType() == typeof(Monster) || conflictedObject.GetType() == typeof(Sack);
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var (playerX, playerY) = FindPlayer();
            if (playerX == -1)
            {
                return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Monster() };
            }
            var (x0, y0) = (0, 0);
            if (playerX - x != 0)
            {
                if (!CheckForMonstr(x - 1, y))
                    (x0, y0) = (-1, 0);
                else if (!CheckForMonstr(x + 1, y))
                    (x0, y0) = (1, 0);
            }
            else if (!CheckForMonstr(x, y - 1))
                (x0, y0) = (0, -1);
            else if (!CheckForMonstr(x, y + 1))
                (x0, y0) = (0, 1);
            return new CreatureCommand { DeltaX = x0, DeltaY = y0, TransformTo = new Monster() };
        }

        public static bool CheckForMonstr(int x, int y)
        {
            if (y <= Game.MapHeight && y >= 0 && x < Game.MapWidth - 1 && x >= 0)
                return (Game.Map[x, y] != null)
                     && (Game.Map[x, y].GetType() == typeof(Sack)
                     || Game.Map[x, y].GetType() == typeof(Monster)
                     || Game.Map[x, y].GetType() == typeof(Terrain));
            return true;
        }

        public static (int, int) FindPlayer()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                    if (Game.Map[i, j]?.GetType() == typeof(Player))
                        return (i, j);
            return (-1, -1);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.GetType() == typeof(Sack) || conflictedObject.GetType() == typeof(Monster);
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetType() == typeof(Player))
                Game.Scores += 10;
            return conflictedObject.GetType() == typeof(Player) || conflictedObject.GetType() == typeof(Monster);
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Sack : ICreature
    {
        int speed = 0;
        public CreatureCommand Act(int x, int y)
        {
            if (y >= 0 && y < Game.MapHeight - 1)
            {
                Type map = Game.Map[x, y + 1]?.GetType();
                if (map == null)
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1, TransformTo = new Sack { speed = speed + 1 } };
                else if (speed >= 1 && (map == typeof(Player) || map == typeof(Monster)))
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1, TransformTo = new Sack { speed = speed + 1 } };
                else if (speed > 1)
                    return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            if (speed > 1)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Sack { speed = 0 } };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
}
