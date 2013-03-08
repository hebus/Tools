using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;

namespace UIComponents
{
  [ProvideProperty("CueText", typeof(TextBox))]
  public class CueExtender : Component, IExtenderProvider
  {

    Container components;
    Hashtable cueTexts;
    TextBox activeControl;


    public CueExtender()
    {
      components = new Container();
      cueTexts = new Hashtable();
    }

    [DefaultValue(""),
    Category("CueBanner"),
    Description("Afficher une CueBanner d'aide à la saisie")]
    public string GetCueText(Control control)
    {
      string text = (string)cueTexts[control];
      if (text == null)
        text = string.Empty;

      return text;
    }

    private void OnControlEnter(object sender, EventArgs e)
    {
      activeControl = (TextBox)sender;
    }
    private void OnControlLeave(object sender, EventArgs e)
    {
      if (sender == activeControl)
      {
        activeControl = null;
      }
    }

    public void SetCueText(TextBox control, string value)
    {
      if (value == null)
      {
        value = string.Empty;
      }

      if (value.Length == 0)
      {
        cueTexts.Remove(control);
        CueProvider.SetCue(control, null);

        control.Enter -= new EventHandler(OnControlEnter);
        control.Leave -= new EventHandler(OnControlLeave);
      }
      else
      {
        cueTexts[control] = value;
        CueProvider.SetCue(control, value);

        control.Enter += new EventHandler(OnControlEnter);
        control.Leave += new EventHandler(OnControlLeave);
      }
    }

    #region IExtenderProvider Members

    public bool CanExtend(object extendee)
    {
      return (extendee is Control && !(extendee is CueExtender));
    }

    #endregion
  }
}
