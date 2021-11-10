using System.Collections.Generic;

namespace SgClient1.Command
{
    public class CommandController
    {
        private List<ICommand> Commands { get; set; }

        public CommandController()
        {
            Commands = new List<ICommand>();
        }

        public void run(ICommand command)
        {
            Commands.Add(command);
            command.run();
        }

        public string undo()
        {
            int lastIndex = Commands.Count - 1;
            Commands[lastIndex].undo();
            Commands.RemoveAt(lastIndex);

            if (Commands.Count == 0)
            {
                return "disable";
            }

            else return "";
        }
    }
}
