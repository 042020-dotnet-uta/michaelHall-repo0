using System;
using System.Collections.Generic;
using System.Text;

namespace RPS_Game
{
    class Player
    {
		public int PlayerID { get; set; }

		private string _Name;
		public string Name
		{
			get {return _Name;}
			set { _Name = value; }
		}

	}
}