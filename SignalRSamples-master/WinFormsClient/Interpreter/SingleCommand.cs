using SgClient1.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterConsole.Interpreter
{
    class SingleCommand : CommandExpression
    {
        ICommand Command;
        public SingleCommand(ICommand Command)
        {
            this.Command = Command;
        }
        public override void Execute()
        {
            Command.run();
        }
    }
}
