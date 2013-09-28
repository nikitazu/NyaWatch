using System;
using FluentScheduler;

namespace NyaWatch.Core.Domain.Tasks
{
	public class TasksRegistry : Registry
	{
		public TasksRegistry ()
		{
			Schedule<FindTorrentsTask> ().ToRunNow ().AndEvery (60).Seconds ();
		}

		public static void Init()
		{
			TaskManager.Initialize (new TasksRegistry ());
		}
	}
}

