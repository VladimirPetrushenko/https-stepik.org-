using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Generics.Robots
{
    public interface RobotAI<out Tparam>
    {
        Tparam GetCommand();
    }

    public class ShooterAI : RobotAI<ShooterCommand>
    {
        int counter = 1;

        public ShooterCommand GetCommand()
        {
            return ShooterCommand.ForCounter(counter++);
        }
    }

    public class BuilderAI : RobotAI<BuilderCommand>
    {
        int counter = 1;

        public BuilderCommand GetCommand()
        {
            return BuilderCommand.ForCounter(counter++);
        }
    }

    public interface Device<in Tparam>
    {
        string ExecuteCommand(Tparam command);
    }

    public class Mover : Device<IMoveCommand>
    {
        public string ExecuteCommand(IMoveCommand _command)
        {
            var command = _command;
            if (command == null)
                throw new ArgumentException();
            return $"MOV {command.Destination.X}, {command.Destination.Y}";
        }
    }

    public class ShooterMover : Device<IMoveCommand>
    {
        public string ExecuteCommand(IMoveCommand _command)
        {
            var command = _command as IShooterMoveCommand;
            if (command == null)
                throw new ArgumentException();
            var hide = command.ShouldHide ? "YES" : "NO";
            return $"MOV {command.Destination.X}, {command.Destination.Y}, USE COVER {hide}";
        }
    }

    public class Robot
    {
        public static Robot<TCommand> Create<TCommand>(RobotAI<TCommand> ai, Device<TCommand> executor)
        {
            return new Robot<TCommand>(ai, executor);
        }
    }
    public class Robot<Tparams>
    {
        private readonly RobotAI<Tparams> ai;
        private readonly Device<Tparams> device;

        public Robot(RobotAI<Tparams> ai, Device<Tparams> executor)
        {
            this.ai = ai;
            this.device = executor;
        }

        public IEnumerable<string> Start(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                var command = ai.GetCommand();
                if (command == null)
                    break;
                yield return device.ExecuteCommand(command);
            }
        }

        public static Robot<TCommand> Create<TCommand>(RobotAI<TCommand> ai, Device<TCommand> executor)
        {
            return new Robot<TCommand>(ai, executor);
        }
    }
}
