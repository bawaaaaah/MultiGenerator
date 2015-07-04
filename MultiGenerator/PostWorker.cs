using System;
using System.Threading;

namespace MultiGenerator
{
	public class PostWorker
	{
		private Semaphore sem = null;
		private Random r;
		private Object consoleLocker;
		private int multiplicator;

		public PostWorker (int maxJobs, Object consoleLocker)
		{
			if ((multiplicator = maxJobs) > 0)
				sem = new Semaphore (maxJobs, maxJobs);
			this.r = new Random ();
			this.consoleLocker = consoleLocker;
		}

		public void doWork(String[] str){
			if (sem != null) {
				sem.WaitOne ();
				var t = new Thread (doWork);
				t.Name = "PostWorker of " + System.Threading.Thread.CurrentThread.Name;
				t.Start (str);
			} else {
				doWork (str);
			}
		}

		private void doWork(Object o){
			String[] str = (String[])o;

			System.Threading.Thread.Sleep (r.Next (25 + str.Length, 50 * multiplicator));

			int j = 0;

			if (sem != null)
				j = sem.Release();

			lock (consoleLocker)
				Console.WriteLine (System.Threading.Thread.CurrentThread.Name + " Jobs :" + (multiplicator = (j+1)));
		}
	}
}

