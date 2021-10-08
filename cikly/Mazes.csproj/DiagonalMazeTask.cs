using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int stepWidth = width / height > 0 ? (int)Math.Round((double)width / height) : 1;
            int stepHeight = height / width > 0 ? (int)Math.Round((double)height / width) : 1;
            while (!robot.Finished)
            {
                if (stepWidth > stepHeight)
                    MakeMove(robot, stepWidth, stepHeight, Direction.Right, Direction.Down);
                else
                    MakeMove(robot, stepHeight, stepWidth, Direction.Down, Direction.Right);
            }
        }

        private static void Move(Robot robot, int step, Direction direction)
        {
            for (int i = 0; i < step; i++)
                robot.MoveTo(direction);
        }

        private static void MakeMove(Robot robot, int stepCount1, int stepCount2, Direction dir1, Direction dir2)
        {
            Move(robot, stepCount1, dir1);
            if (robot.Finished)
                return ;
            Move(robot, stepCount2, dir2);
            if (robot.Finished)
                return;
        }
    }
}