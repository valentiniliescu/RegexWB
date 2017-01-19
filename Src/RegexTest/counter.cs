using System;

namespace Timing
{
	public class Counter 
	{
		long elapsedCount = 0;
		long startCount = 0;

		public void Start()
		{
			startCount = 0;
			QueryPerformanceCounter(ref startCount);
		}
		
		public void Stop()
		{
			long stopCount = 0;
			QueryPerformanceCounter(ref stopCount);

			elapsedCount += (stopCount - startCount);
		}

	    public float Seconds
		{
			get
			{
				long freq = 0;
				QueryPerformanceFrequency(ref freq);
				return((float) elapsedCount / (float) freq);
			}
		}

		public override string ToString()
		{
			return String.Format("{0} seconds", Seconds);
		}

	    [System.Runtime.InteropServices.DllImport("KERNEL32")]
		private static extern bool QueryPerformanceCounter(  ref long lpPerformanceCount);

		[System.Runtime.InteropServices.DllImport("KERNEL32")]
		private static extern bool QueryPerformanceFrequency( ref long lpFrequency);                     
	}
}
