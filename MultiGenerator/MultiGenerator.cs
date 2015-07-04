using System;
using System.Threading;

namespace MultiGenerator
{
	public class MultiGenerator
	{
		private static Object consoleLocker = new Object();
		private Thread[] ts;

		public MultiGenerator ()
		{
			Generator g = new Generator (200);
			PostWorker pw = new PostWorker (50, consoleLocker);
			ts = new Thread[Environment.ProcessorCount * 2];

			for (int i = 0; i < ts.Length; ++i) {
				ts[i] = new Thread(new Worker (g, pw, consoleLocker).run);
				ts[i].Name = "Worker : " + i;
			}
		}

		public void go(){
			for (int i = 0; i < ts.Length; ++i) {
				ts[i].Start();
			}

			for (int i = 0; i < ts.Length; ++i) {
				ts[i].Join();
				lock (consoleLocker)
					Console.WriteLine (ts [i].Name + " DONE");
			}
		}
	}
}

