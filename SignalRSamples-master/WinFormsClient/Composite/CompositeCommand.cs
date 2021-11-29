<<<<<<< Updated upstream
﻿using SgClient1.Command;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SgClient1.Command;
>>>>>>> Stashed changes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Composite
{
    public class CompositeCommand : ICommand
    {
        private List<ICommand> Commands { get; set; }

        public CompositeCommand()
        {
            Commands = new List<ICommand>();
        }

        public void add(ICommand command)
        {
            Commands.Add(command);
        }

        public void run()
        {
            //Commands.Add(command);
<<<<<<< Updated upstream
            foreach(var command in Commands)
=======
            foreach (var command in Commands)
>>>>>>> Stashed changes
            {
                command.run();
            }
        }

        /*public string undo()
        {
            int lastIndex = Commands.Count - 1;
            Commands[lastIndex].undo();
            Commands.RemoveAt(lastIndex);

            if (Commands.Count == 0)
            {
                return "disable";
            }

            else return "";
        }*/
    }
}
