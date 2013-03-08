using System;
using System.Collections.Generic;
using System.Text;

namespace patterns
{
  public interface IObserver<T>
  {
    void AddObservable(IObservable<T> observable);
    void RemoveObservable(IObservable<T> observable);
    void ReceiveNotify(T info);
  }

  public class Observer<T> : IObserver<T>
  {
    List<IObservable<T>> list = new List<IObservable<T>>();

    #region INotifiable Membres

    public void AddObservable(IObservable<T> observable)
    {
      if (!list.Contains(observable))
      {
        LogWriter.WriteLineInfoEx("[+] OBSERVABLE", "AddObservable : " + observable.ToString());
        list.Add(observable);
      }
      // indiquer a l'"Observable" que ce contrôle l'observe
      observable.registerObserver(this);
    }

    public void RemoveObservable(IObservable<T> observable)
    {
      LogWriter.WriteLineInfoEx("[-] OBSERVABLE", "RemoveObservable : " + observable.ToString());
      if (list.Contains(observable)) list.Remove(observable);
      observable.unregisterObserver(this);
    }

    public virtual void ReceiveNotify(T info)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion
  }
}
