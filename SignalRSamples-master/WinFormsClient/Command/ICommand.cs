namespace SgClient1.Command
{
    public abstract class ICommand
    {
        public abstract void run();
        public abstract void undo();
    }
}
