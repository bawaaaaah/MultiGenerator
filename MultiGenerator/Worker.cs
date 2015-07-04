using System;
using System.Collections.Generic;

namespace MultiGenerator
{
	public class Worker
	{
		private Generator generator;
		public Boolean stopThread = false;
		private Random r;
		private PostWorker pw;
		private Object consoleLocker;
		public Worker (Generator g, PostWorker pw, Object consoleLocker)
		{
			this.generator = g;
			this.r = new Random ();
			this.pw = pw;
			this.consoleLocker = consoleLocker;
		}

		public void run()
		{
			List<String> ljobs = new List<String> ();
			while(!stopThread)
			{
				stopThread = !generator.generate (ref ljobs);

				foreach (var s in ljobs) {
					lock (consoleLocker)
						Console.WriteLine (System.Threading.Thread.CurrentThread.Name + " Jobs :" + s);
					System.Threading.Thread.Sleep (r.Next (25, 100));
					pw.doWork(s.Split('_'));
				}
			}
		}
	}
}

