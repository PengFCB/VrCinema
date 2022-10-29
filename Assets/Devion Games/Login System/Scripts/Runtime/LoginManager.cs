using DevionGames.LoginSystem.Configuration;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace DevionGames.LoginSystem
{
    public class LoginManager : MonoBehaviour
    {
		private static LoginManager m_Current;

		/// <summary>
		/// The LoginManager singleton object. This object is set inside Awake()
		/// </summary>
		public static LoginManager current
		{
			get
			{
				Assert.IsNotNull(m_Current, "Requires Login Manager.Create one from Tools > Devion Games > Login System > Create Login Manager!");
				return m_Current;
			}
		}

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		private void Awake()
		{
			if (LoginManager.m_Current != null)
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Multiple LoginManager in scene...this is not supported. Destroying instance!");
				Destroy(gameObject);
				return;
			}
			else
			{
				LoginManager.m_Current = this;
				if(LoginManager.DefaultSettings.debug)
					Debug.Log("LoginManager initialized.");

			}
		}

        private void Start()
        {
			Global.UpdateFilmList();
			if (LoginManager.DefaultSettings.skipLogin)
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Login System is disabled...Loading "+ LoginManager.DefaultSettings.sceneToLoad+" scene.");
				UnityEngine.SceneManagement.SceneManager.LoadScene(LoginManager.DefaultSettings.sceneToLoad);
			}
			//UnityEngine.VR.VRSettings.enabled = false;
		}


        [SerializeField]
		private LoginConfigurations m_Configurations = null;

		/// <summary>
		/// Gets the login configurations. Configurate it inside the editor.
		/// </summary>
		/// <value>The database.</value>
		public static LoginConfigurations Configurations
		{
			get
			{
				if (LoginManager.current != null)
				{
					Assert.IsNotNull(LoginManager.current.m_Configurations, "Please assign Login Configurations to the Login Manager!");
					return LoginManager.current.m_Configurations;
				}
				return null;
			}
		}


		private static Default m_DefaultSettings;
		public static Default DefaultSettings
		{
			get
			{
				if (m_DefaultSettings == null)
				{
					m_DefaultSettings = GetSetting<Default>();
				}
				return m_DefaultSettings;
			}
		}

		private static UI m_UI;
		public static UI UI
		{
			get
			{
				if (m_UI == null)
				{
					m_UI = GetSetting<UI>();
				}
				return m_UI;
			}
		}

		private static Notifications m_Notifications;
		public static Notifications Notifications
		{
			get
			{
				if (m_Notifications == null)
				{
					m_Notifications = GetSetting<Notifications>();
				}
				return m_Notifications;
			}
		}

		private static Server m_Server;
		public static Server Server
		{
			get
			{
				if (m_Server == null)
				{
					m_Server = GetSetting<Server>();
				}
				return m_Server;
			}
		}

		private static T GetSetting<T>() where T : Configuration.Settings
		{
			if (LoginManager.Configurations != null)
			{
				return (T)LoginManager.Configurations.settings.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
			}
			return default(T);
		}



		public static void CreateAccount(string username, string password, string email)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(CreateAccountInternal(username, password, email));
			}
		}

		private static IEnumerator CreateAccountInternal(string username, string password, string email)
		{
			if (LoginManager.Configurations == null)
			{
				EventHandler.Execute("OnFailedToCreateAccount");
				yield break;
			}
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[CreateAccount]: Trying to register a new account with username: " + username + " and password: " + password + "!");
			/**
			WWWForm newForm = new WWWForm();
			newForm.AddField("name", username);
			newForm.AddField("password", password);
			newForm.AddField("email", email);

			using (UnityWebRequest www = UnityWebRequest.Post(LoginManager.Server.serverAddress + "/" + LoginManager.Server.createAccount, newForm)) {
				yield return www.SendWebRequest();
				if (www.isNetworkError || www.isHttpError)
				{
					Debug.Log(www.error);
				}
				else {
					if (www.downloadHandler.text.Contains("true"))
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[CreateAccount] Account creation was successfull!");
						EventHandler.Execute("OnAccountCreated");
					}else {
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[CreateAccount] Failed to create account.");
						EventHandler.Execute("OnFailedToCreateAccount");
					}
				}
			}**/
			UsernameValid(username, email, password);
		}

		/// <summary>
		/// Logins the account.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public static void LoginAccount(string username, string password)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(LoginAccountInternal(username, password));
			}
		}

		private static IEnumerator LoginAccountInternal(string username, string password)
		{
			if (LoginManager.Configurations == null)
			{
				EventHandler.Execute("OnFailedToLogin");
				yield break;
			}
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[LoginAccount] Trying to login using username: " + username + " and password: " + password + "!");

			/**	WWWForm newForm = new WWWForm();
				newForm.AddField("name", username);
				newForm.AddField("password", password);


				using (UnityWebRequest www = UnityWebRequest.Post(LoginManager.Server.serverAddress + "/" + LoginManager.Server.login, newForm))
				{
					yield return www.SendWebRequest();
					if (www.isNetworkError || www.isHttpError)
					{
						Debug.Log(www.error);
					}
					else
					{
						if (www.downloadHandler.text.Contains("true"))
						{
							PlayerPrefs.SetString(LoginManager.Server.accountKey, username);
							if (LoginManager.DefaultSettings.debug)
								Debug.Log("[LoginAccount] Login was successfull!");
							EventHandler.Execute("OnLogin");
						}
						else
						{
							if (LoginManager.DefaultSettings.debug)
								Debug.Log("[LoginAccount] Failed to login.");
							EventHandler.Execute("OnFailedToLogin");
						}
					}
				}**/
			CheckLoginDetails(username, password);
		}

		/// <summary>
		/// Recovers the password.
		/// </summary>
		/// <param name="email">Email.</param>
		public static void RecoverPassword(string email)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(RecoverPasswordInternal(email));
			}
		}

		private static IEnumerator RecoverPasswordInternal(string email)
		{
			if (LoginManager.Configurations == null)
			{
				EventHandler.Execute("OnFailedToRecoverPassword");
				yield break;
			}
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[RecoverPassword] Trying to recover password using email: " + email + "!");

			WWWForm newForm = new WWWForm();
			newForm.AddField("email", email);

			using (UnityWebRequest www = UnityWebRequest.Post(LoginManager.Server.serverAddress + "/" + LoginManager.Server.recoverPassword, newForm))
			{
				yield return www.SendWebRequest();
				if (www.isNetworkError || www.isHttpError)
				{
					Debug.Log(www.error);
				}
				else
				{
					if (www.downloadHandler.text.Contains("true"))
					{
						EventHandler.Execute("OnPasswordRecovered");
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[RecoverPassword] Password recovered successfull!");
					}
					else
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[RecoverPassword] Failed to recover password.");
						EventHandler.Execute("OnFailedToRecoverPassword");
					}
				}
			}
		}

		/// <summary>
		/// Resets the password.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public static void ResetPassword(string username, string password)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(ResetPasswordInternal(username, password));
			}
		}

      
        private static IEnumerator ResetPasswordInternal(string username, string password)
		{
			if (LoginManager.Configurations == null)
			{
				EventHandler.Execute("OnFailedToResetPassword");
				yield break;
			}
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[ResetPassword] Trying to reset password using username: " + username + " and password: " + password + "!");

			WWWForm newForm = new WWWForm();
			newForm.AddField("name", username);
			newForm.AddField("password", password);

			using (UnityWebRequest www = UnityWebRequest.Post(LoginManager.Server.serverAddress + "/" + LoginManager.Server.resetPassword, newForm))
			{
				yield return www.SendWebRequest();
				if (www.isNetworkError || www.isHttpError)
				{
					Debug.Log(www.error);
				}
				else
				{
					if (www.downloadHandler.text.Contains("true"))
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[RecoverPassword] Password resetted!");
						EventHandler.Execute("OnPasswordResetted");
				
					}
					else
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("Failed to reset password.");
						EventHandler.Execute("OnFailedToResetPassword");
					}
				}
			}
		}

		/// <summary>
		/// Validates the email.
		/// </summary>
		/// <returns><c>true</c>, if email was validated, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		public static bool ValidateEmail(string email)
		{
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			System.Text.RegularExpressions.Match match = regex.Match(email);
			if (match.Success)
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Email validation was successfull for email: " + email + "!");
			}
			else
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Email validation failed for email: " + email + "!");
			}

			return match.Success;
		}

		private static void CheckLoginDetails(string username, string passwd)
		{
			if (username.Equals("") || passwd.Equals(""))
			{
				Debug.Log("Uname or Passwd is empty");
				//noticeText.text = "Uname or Passwd is empty";
				return;
			}
			passwd = CreateMD5(passwd);

			FirebaseDatabase.GetInstance("https://elec5620-43fef-default-rtdb.firebaseio.com")
		 .GetReference("Users").Child(username)
		 .GetValueAsync().ContinueWithOnMainThread(task => {
			 if (task.IsFaulted)
			 {
			 // Handle the error...
			 //noticeText.text = "Database Error...";
			 }
			 else if (task.IsCompleted)
			 {
				 DataSnapshot snapshot = task.Result;
			 // Do something with snapshot...
			 if (snapshot.Value == null)
				 {
					 //noticeText.text = "Username or Password Incorrect!";
					 Debug.Log("Username not exist");
					 EventHandler.Execute("OnFailedToLogin");
				 }
				 else if (!snapshot.Child("passwd").Value.ToString().Equals(passwd))
				 {
					 //noticeText.text = "Username or Password Incorrect!";
					 Debug.Log("passwd incorrect");
					 EventHandler.Execute("OnFailedToLogin");
				 }
				 else
				 {
					 //noticeText.text = "Login successfully";
					 Global.currentUser = username;
					 //Global.GetUserFromDatabase();
					 Debug.Log("Login successfully for " + username);
					 //EventHandler.Execute("OnLogin");
					 
					 SceneManager.LoadScene("reception_scene");
				 }
			 }
		 });
		}

		public static string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);



				//Convert the byte array to hexadecimal string prior to .NET 5
				StringBuilder sb = new System.Text.StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}

		private static void UsernameValid(string username, string email, string passwd)
		{
			//noticeText.text = "Please wait ...";
			FirebaseDatabase.GetInstance("https://elec5620-43fef-default-rtdb.firebaseio.com").GetReference("Users").GetValueAsync().ContinueWithOnMainThread(task => {
				if (task.IsFaulted)
				{
					// Handle the error...
					//noticeText.text = "Database Error...";
					Debug.Log("Database Error...");

				}
				else if (task.IsCompleted)
				{
					DataSnapshot snapshot = task.Result;
					// Do something with snapshot...
					if (snapshot.HasChild(username))
					{
						EventHandler.Execute("OnFailedToCreateAccount");
					}
					else
					{
						WriteDatabase(username, email, passwd);
						//noticeText.text = "Successful registration!";
						EventHandler.Execute("OnAccountCreated");
						Debug.Log("Account created!");

					}
				}
			});
		}

		private static void WriteDatabase(string username, string email, string passwd)
		{
			DatabaseReference reference;
			reference = FirebaseDatabase.GetInstance("https://elec5620-43fef-default-rtdb.firebaseio.com").RootReference;
			reference.Child("Users").Child(username).Child("email").SetValueAsync(email);
			reference.Child("Users").Child(username).Child("passwd").SetValueAsync(CreateMD5(passwd));
			reference.Child("Users").Child(username).Child("balance").SetValueAsync(100.0);
		}
	}
}