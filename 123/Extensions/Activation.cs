using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
/*
namespace DotAllCombo.Extensions
{
    class Activation
    {
		//  private static string _activationKey;
		//  private static string _user;
		//   private static string _secretKey;
		//  private static string _version;
		//   private static string _key;

//  private static int _activationState;

//   private static string _address = "http://srv.luckydota.tk/activation.php?key=";

  public static void Init()
  {
	  _activationKey = Variables.Settings.ActivationKey;
	  _user = Variables.Settings.User.ToString();
	  _secretKey = Bootstrap.a2ee688cda9e724885e23cd2cfdee;
	  _version = Variables.Version;
  }

  public static void SendValidator()
  {
	  WebRequest req = WebRequest.Create(_address + _activationKey + "&user=" + _user + "&secretKey=" + _secretKey + "&version=" + _version);
	  WebResponse resp = req.GetResponse();

	  using (StreamReader stream = new StreamReader(
	  resp.GetResponseStream(), Encoding.UTF8))
	  {
		  _key = stream.ReadToEnd();
	  }

	  switch (_key)
	  {
		  case "cfcd208495d565ef66e7dff9f98764da":
			  _activationState = 0;
			  break;
		  case "c4ca4238a0b923820dcc509a6f75849b":
			  _activationState = 1;
			  break;
		  case "c81e728d9d4c2f636f067f89cc14862c":
			  _activationState = 2;
			  break;
		  case "eccbc87e4b5ce2fe28308fd9f2a7baf3":
			  _activationState = 3;
			  break;
		  default:
			  _activationState = 0;
			  break;
	  }
  }

  public static bool IsActivated()
  {
	  if (_activationState == 1 || _activationState == 3)
		  return true;
	  else
		  return false;
  }

  public static string GetServerResponse()
  {
	  return _key;
  }

  public static int GetActivationState()
  {
	  return _activationState;
  }

}
} */
