namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			bool straight = true;
            robot.MoveTo(Direction.Right);
            while (!robot.Finished)
            {
                if (robot.X < width - 2 && robot.X > 1) 
					robot.MoveTo(straight ? Direction.Right : Direction.Left);
                else
                    straight = ReverseDirection(robot, straight);
            }
		}

        private static bool ReverseDirection(Robot robot, bool straight)
        {
            MoveDown2Step(robot);
            straight = !straight;
            robot.MoveTo(straight ? Direction.Right : Direction.Left);
            return straight;
        }

        private static void MoveDown2Step(Robot robot)
        {
            robot.MoveTo(Direction.Down);
            robot.MoveTo(Direction.Down);
        }
    }
}