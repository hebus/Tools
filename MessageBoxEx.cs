using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Vac.Tools.Log;
using System.ComponentModel;

namespace UIComponents
{
  /// <summary>
  /// Cette classe reprend tous les constructeurs de la classe static MessageBox. La seule différence avec la classe
  /// d'origine, est que cette classe log tous les messages qui sont affichés à l'utilisateur
  /// </summary>
  public class MessageBoxEx
  {
    // Methods
    private MessageBoxEx()
    {
    }

    public static DialogResult Show(string text)
    {
      return ShowCore(null, text, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(string text, string caption)
    {
      return ShowCore(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(IWin32Window owner, string text)
    {
      return ShowCore(owner, text, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
    {
      return ShowCore(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(IWin32Window owner, string text, string caption)
    {
      return ShowCore(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
      return ShowCore(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
    {
      return ShowCore(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
    {
      return ShowCore(null, text, caption, buttons, icon, defaultButton, 0);
    }

    public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
      return ShowCore(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0);
    }

    public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
    {
      return ShowCore(null, text, caption, buttons, icon, defaultButton, options);
    }

    public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
    {
      return ShowCore(owner, text, caption, buttons, icon, defaultButton, 0);
    }

    public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
    {
      return ShowCore(owner, text, caption, buttons, icon, defaultButton, options);
    }

    private static DialogResult ShowCore(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
    {
      //LogWriter.WriteLineInfo("MessageBox: " + text);
      DialogResult result = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
      //LogWriter.WriteLineInfo("MessageBox Result: " + result.ToString());
      return result;
    }
  }
}
