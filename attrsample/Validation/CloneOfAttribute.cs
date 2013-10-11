using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attrsample.Validation {
	/// <summary>
	/// 指定されたプロパティと同一の値を入力するように要求するカスタム検証属性を作成します。
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false)]
	public class CloneOfAttribute : ValidationAttribute {
		/// <summary>
		/// 比較対象のプロパティを取得します。
		/// </summary>
		public string PropertyName { get; private set; }

		/// <summary>
		/// 比較対象のプロパティ名を指定してCloneOf属性の新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="propertyName"></param>
		public CloneOfAttribute(string propertyName)
		{
			PropertyName = propertyName;
			ErrorMessage = "コピー元のプロパティ[{0}]と値が異なります。";
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var instance = validationContext.ObjectInstance;
			var type = instance.GetType();
			var target = type.GetProperty(PropertyName);
			if (object.Equals(value, target.GetValue(instance))) {
				return ValidationResult.Success;
			}
			return new ValidationResult(string.Format(ErrorMessage, PropertyName));
		}
	}
}
