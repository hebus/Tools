using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace patterns
{
  public enum eCentralDispatchFlags
  {
    GetAlgosForUserFinished
  }

  /// <summary>
  /// Cette classe permet d'envoyer des flags à ses abonnés afin qu'ils puissent effectuer une
  /// opération particulière dès réception du message.
  /// 
  /// Elle permet également d'envoyer des message de type string à ses abonnés afin qu'ils 
  /// puissent les afficher sous la forme qu'ils souhaitent (barre de progression, notification bar...)
  /// ou bien traiter l'information à d'autres fins.
  /// </summary>
  public class CentralDispatch<T> : IObserver<T>, IObservable<T>
  {
    private readonly object _lock;

    public CentralDispatch()
    {
      _lock = new object();
    }

    static CentralDispatch<T> central = null;
    public static CentralDispatch<T> Instance()
    {
      if (central == null)
        central = new CentralDispatch<T>();

      return central;
    }


    // envoyer des flags aux abonnés afin qu'ils effectuent une opération particulière dès réception
    #region IObservable<T> Membres

    List<IObserver<T>> listOfObserversDispatchFlags = new List<IObserver<T>>();
    public void Notify(T info)
    {
      // http://jira/jira/browse/DSIAVEN-1311
      // Ajout d'un lock() dans la méthode Notify()
      // Ajout d'un bloc try/catch dans la méthode Notify()

      if (Monitor.TryEnter(_lock, 5000))
      {
        foreach (IObserver<T> obs in listOfObserversDispatchFlags)
        {
          try
          {
            obs.ReceiveNotify(info);
          }
          catch (Exception)
          {
            continue;
          }
        }
      }
      Monitor.Exit(_lock);
    }

    public void registerObserver(IObserver<T> observer)
    {
      if (!listOfObserversDispatchFlags.Contains(observer))
      {
        //LogWriter.WriteLineInfoEx("[+] OBSERVER", GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + observer.ToString());
        listOfObserversDispatchFlags.Add(observer);
      }
    }

    public void unregisterObserver(IObserver<T> observer)
    {
      if (listOfObserversDispatchFlags.Contains(observer))
      {
        //LogWriter.WriteLineInfoEx("[-] OBSERVER", GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + observer.ToString());
        listOfObserversDispatchFlags.Remove(observer);
      }
    }

    #endregion

    #region IObserver<T> Membres

    List<IObservable<T>> list = new List<IObservable<T>>();
    public void AddObservable(IObservable<T> observable)
    {
      if (!list.Contains(observable))
      {
        //LogWriter.WriteLineInfoEx("[+] OBSERVABLE", GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + observable.ToString());
        list.Add(observable);
      }
      observable.registerObserver(this);
    }

    virtual public void ReceiveNotify(T info)
    {
      // transmettre l'information aux observers
      Notify(info);
    }

    public void RemoveObservable(IObservable<T> observable)
    {
      //LogWriter.WriteLineInfoEx("[-] OBSERVABLE",GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + observable.ToString());
      list.Remove(observable);
      observable.unregisterObserver(this);
    }
    public void RemoveAllObservables()
    {
      foreach (IObservable<T> observable in list)
      {
        //LogWriter.WriteLineInfoEx("[-] OBSERVABLE",GetType().ToString() + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + " " + observable.ToString());
        list.Remove(observable);
        observable.unregisterObserver(this);
      }
    }
    #endregion
  }
}
