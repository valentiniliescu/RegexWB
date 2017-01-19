using System;
using System.Diagnostics;

namespace Timing
{
	public class Counter
	{
	    private Stopwatch stopwatch = new Stopwatch();

		public void Start()
		{
            stopwatch.Start();
        }
		
		public void Stop()
		{
            stopwatch.Stop();
        }

	    public double Seconds => stopwatch.Elapsed.TotalSeconds;

	    public override string ToString()
		{
			return String.Format("{0} seconds", Seconds);
		}
	}
}
