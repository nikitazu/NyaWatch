using System;
using FluentScheduler;

namespace NyaWatch.Core.Domain.Tasks
{
	public class TasksRegistry : Registry
	{
		public TasksRegistry ()
		{
			Schedule<FindTorrentsTask> ().ToRunNow ().AndEvery (10).Seconds ();
		}

		public static void Init()
		{
			TaskManager.Initialize (new TasksRegistry ());
		}
	}
}

