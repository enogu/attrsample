using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //Note:参照を追加した
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attrsample.Validation {
	/// <summary>
	/// パスワードの安全性を検証するカスタム検証属性を提供します。
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false)]
	public class SecurePasswordAttribute : ValidationAttribute {
		/// <summary>
		/// パスワードの長さの最小値を指定します。
		/// </summary>
		public int MinLength { get; set; }

		/// <summary>
		/// パスワードに使用する文字の種類の最小値を指定します。
		/// </summary>
		public int MinCases { get; set; }

		public SecurePasswordAttribute()
		{
			ErrorMessage = null;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var password = value != null ? value.ToString() : null;
			if (password == null || password.Length < MinLength) {
				return new ValidationResult(
					string.Format(ErrorMessage ?? "パスワードの文字数が不足しています。パスワードには最低{0}文字必要です。", MinLength)
					);
			}
			var cases = password
				.GroupBy(c => char.IsUpper(c) ? 1 : char.IsLower(c) ? 2 : char.IsNumber(c) ? 3 : char.IsSymbol(c) ? 4 : 0)
				.Where(group => group.Key != 0)
				.Count();
			if (cases < MinCases) {
				return new ValidationResult(
					string.Format(ErrorMessage ?? "文字の種類が少なすぎます。パスワードには大文字、小文字、数字、記号のうち{0}種類を組み合わせてください。", MinCases)
					);
			}
			return ValidationResult.Success;
		}
	}
}
