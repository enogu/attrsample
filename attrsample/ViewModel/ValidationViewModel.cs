using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;

namespace attrsample.ViewModel {
	public abstract class ValidationViewModel : NotificationObject, IDataErrorInfo {
		readonly Dictionary<string, string> _ErrorMessages = new Dictionary<string,string>();

		public string Error
		{
			get { throw new NotImplementedException(); }
		}

		public string this[string columnName]
		{
			get {
				var property = this.GetType().GetProperty(columnName);
				var context = new ValidationContext(this) { MemberName = columnName };
				var errors = new List<ValidationResult>();
				if (Validator.TryValidateProperty(property.GetValue(this), context, errors)) {
					_ErrorMessages[columnName] = null;
				}
				else if (errors.Any()) {
					_ErrorMessages[columnName] = errors.First().ErrorMessage;
				}
				else {
					_ErrorMessages[columnName] = string.Format("{0}の入力に問題があります。");
				}
				_Errors.Clear();
				foreach (var error in _ErrorMessages.Values.Where(error => error != null).ToArray()) {
					_Errors.Add(error);
				}
				return _ErrorMessages[columnName];
			}
		}

		public IEnumerable<string> Errors
		{
			get { return _Errors; }
		}
		readonly ObservableCollection<string> _Errors = new ObservableCollection<string>();
	}
}
