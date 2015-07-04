using System;
using System.Collections.Generic;

namespace MultiGenerator
{
	public class Generator
	{
		private int numberOfDataGen;

		private int a = 0, b = 0, c = 0, d = 0;

		public Generator (int numberOfDataGen)
		{
			this.numberOfDataGen = numberOfDataGen > 0 ? numberOfDataGen : 1;
			b = 1;
		}

		public Boolean generate(ref List<String> s){
			String value = "";
			Boolean notEnd = true;
			for (int i = 0; i < numberOfDataGen; ++i) {
				if ((value = gen ()) == null) {
					notEnd = false;
					break;
				}
				s.Add (value);
			}
			return notEnd;
		}

		private String gen(){
			if (++a > 2) {
				a = 0;
				if ((b <<= 2) > 32000) {
					b = 0;
				}
			}
			c += b % (a+1);
			d++;
			if (d < 7000)
				return "m_" + a + "_" + b + "-" + c + "_D : " + d;
			return null;
		}
	}
}

