using System;
using System.Text;

namespace WaveTech.Dafuscator.Model
{
	public class ConnectionString
	{
		private string _serverName;
		private string _databaseName;
		private string _userName;
		private string _password;
		private bool _useSqlAuth;

		private string _fullConnectionString;

		public ConnectionString()
		{
			
		}

		public ConnectionString(string fullConnectionString)
		{
			_fullConnectionString = fullConnectionString;
		}

		public ConnectionString(string serverName, string databaseName)
		{
			_serverName = serverName;
			_databaseName = databaseName;

			_useSqlAuth = false;
		}

		public ConnectionString(string serverName, string databaseName, string userName, string password)
		{
			_serverName = serverName;
			_databaseName = databaseName;
			_userName = userName;
			_password = password;

			_useSqlAuth = true;
		}



		public string DatabaseName
		{
			get { return _databaseName; }
			set { _databaseName = value; }
		}

		public string ServerName
		{
			get { return _serverName; }
			set { _serverName = value; }
		}

		public string UserName
		{
			get { return _userName; }
			set { _userName = value; }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		public bool UseSqlAuth
		{
			get { return _useSqlAuth; }
			set { _useSqlAuth = value; }
		}

		public string FullConnectionString
		{
			get { return _fullConnectionString; }
			set { _fullConnectionString = value; }
		}

		public string GetConnectionString()
		{
			if (!String.IsNullOrEmpty(_fullConnectionString))
				return _fullConnectionString;

			StringBuilder connectionString = new StringBuilder();

			connectionString.Append(string.Format("Provider=SQLOLEDB;Data Source={0};Initial Catalog={1};", _serverName, _databaseName));

			if (_useSqlAuth)
				connectionString.Append(string.Format("Uid={0};Pwd={1};", _userName, _password));
			else
				connectionString.Append("Integrated Security=SSPI;");

			return connectionString.ToString();
		}
	}
}
