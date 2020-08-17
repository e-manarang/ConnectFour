using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectFour
{
    /// <summary>
    /// This is the Player class that holds information about players.
    /// </summary>
    public abstract class Player
    {
        private string _name;
        private ConsoleColor _color;
        private sbyte _token;
        protected bool _isHuman;
        
        // Public Property Name
        public string Name
        {
            get { return _name; }
        }

        // Public Property PlayerColor
        public ConsoleColor PlayerColor
        {
            get { return _color;  }
            set { _color = value;  }
        }

        // Public Property Token
        public sbyte Token
        {
            get { return _token; }
            set { _token = value; }
        }

        // Public Property IsComputer
        public bool IsHuman
        {
            get { return _isHuman;  }
        }

        /// <summary>
        /// The Player class constructor.
        /// </summary>
        /// <param name="name">Name of the player</param>
        public Player(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Function that handles the players status before/after moving.
        /// </summary>
        public abstract int Move(sbyte[] pieces);
    }
}
