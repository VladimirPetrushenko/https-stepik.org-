namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            while (!robot.Finished)
            {
                Move(robot, height);
            }
        }

        private static void Move(Robot robot, int height)
        {
            if (robot.Y < height - 2)
                robot.MoveTo(Direction.Down);
            else
                robot.MoveTo(Direction.Right);
        }
    }
}