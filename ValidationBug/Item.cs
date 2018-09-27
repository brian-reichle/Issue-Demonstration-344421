using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ValidationBug
{
	class Item : INotifyPropertyChanged, INotifyDataErrorInfo
	{
		public string Value1 { get => value1; set => SetValue(ref value1, value); }
		public string Value2 { get => value2; set => SetValue(ref value2, value); }
		public string Value3 { get => value3; set => SetValue(ref value3, value); }
		public string Value4 { get => value4; set => SetValue(ref value4, value); }

		public IEnumerable GetErrors(string propertyName)
		{
			if (propertiesWithErrors.Contains(propertyName))
			{
				yield return "Fail";
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		bool INotifyDataErrorInfo.HasErrors => propertiesWithErrors.Count > 0;

		void SetValue(ref string field, string value, [CallerMemberName] string propertyName = null)
		{
			if (value != field)
			{
				field = value;

				if (string.IsNullOrEmpty(value))
				{
					propertiesWithErrors.Remove(propertyName);
				}
				else
				{
					propertiesWithErrors.Add(propertyName);
				}

				OnPropertyChanged(propertyName);
				OnErrorsChanged(propertyName);
			}
		}

		void OnPropertyChanged([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		void OnErrorsChanged([CallerMemberName] string propertyName = null)
			=> ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

		readonly HashSet<string> propertiesWithErrors = new HashSet<string>();

		string value1;
		string value2;
		string value3;
		string value4;
	}
}
