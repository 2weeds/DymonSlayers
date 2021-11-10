namespace SgClient1.Observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }
}
