using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace attrsample.ViewModel {
	/// <summary>
	/// ログイン画面のモデルを提供します。
	/// </summary>
	public class SignupViewModel : ValidationViewModel {
		/// <summary>
		/// ユーザー名を取得、または設定します。
		/// </summary>
		[Display(Name="ユーザー名")]
		[Required]
		public string UserName {
			get { return _UserName; }
			set {
				_UserName = value;
				RaisePropertyChanged("UserName");
			}
		}
		string _UserName;

		/// <summary>
		/// 電子メールアドレスを取得、または設定します。
		/// </summary>
		[Display(Name="電子メールアドレス")]
		[Required]
		public string Email {
			get { return _Email; }
			set {
				_Email = value;
				RaisePropertyChanged("Email");
			}
		}
		string _Email;

		/// <summary>
		/// パスワードを取得、または設定します。
		/// </summary>
		[Display(Name="パスワード")]
		[Required]
		[Validation.SecurePassword(MinLength=8, MinCases=3)]
		public string Password {
			get { return _Password; }
			set {
				_Password = value;
				RaisePropertyChanged("Password");
			}
		}
		string _Password;

		/// <summary>
		/// 再入力されたパスワードを取得、または設定します。
		/// </summary>
		[Display(Name="パスワードの確認")]
		[Required]
		[Validation.CloneOf("Password")]
		public string ConfirmPassword {
			get { return _ConfirmPassword; }
			set {
				_ConfirmPassword = value;
				RaisePropertyChanged("Password");
			}
		}
		string _ConfirmPassword;

		public InteractionRequest<Notification> SignupSucceeded {
			get {
				if (_SignupSucceeded == null) {
					_SignupSucceeded = new InteractionRequest<Notification>();
				}
				return _SignupSucceeded;
			}
		}
		InteractionRequest<Notification> _SignupSucceeded;

		#region Signup コマンド

		public DelegateCommand SignupCommand {
			get {
				if (_Signup != null) {
					_Signup = new DelegateCommand(Signup);
				}
				return _Signup;
			}
		}
		DelegateCommand _Signup;

		bool CanSignup() {
			return UserName.Length != 0
				&& Password.Length != 0
				&& ConfirmPassword.Length != 0;
		}

		void Signup() {
			if (!Errors.Any()) {
				SignupSucceeded.Raise(new Notification());
			}
		}

		#endregion
	}
}
