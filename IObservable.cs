using System;
using System.Collections.Generic;
using System.Text;

namespace patterns
{
  /// <summary>
  /// Pattern Observe non dédié
  /// </summary>
  public interface IObservable<T>
  {
    void Notify(T info);
    void registerObserver(IObserver<T> observer);
    void unregisterObserver(IObserver<T> observer);
  }
  public class Observable<T> : IObservable<T>
  {
    protected T info;

    public void Notify(T info)
    {
      foreach (IObserver<T> observer in list)
      {
        observer.ReceiveNotify(info);
      }
    }

    #region INotifier Membres

    List<IObserver<T>> list = new List<IObserver<T>>();
    public void registerObserver(IObserver<T> observer)
    {
      if (!list.Contains(observer))
      {
        //LogWriter.WriteLineInfoEx("[+] OBSERVER", "registerObserver : " + observer.ToString());
        list.Add(observer);
      }
    }

    public void unregisterObserver(IObserver<T> observer)
    {
      //LogWriter.WriteLineInfoEx("[-] OBSERVER", "unregisterObserver : " + observer.ToString());
      if (list.Contains(observer)) list.Remove(observer);
    }

    #endregion
  }
}
