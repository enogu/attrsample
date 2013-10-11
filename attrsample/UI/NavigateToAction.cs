using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interactivity;
using System.Windows.Navigation;

namespace attrsample.UI {
	public class NavigateToAction : TriggerAction<System.Windows.UIElement> {
		public string Source { get; set; }

		protected override void Invoke(object parameter)
		{
			var service = NavigationService.GetNavigationService(this.AssociatedObject);
			service.Navigate(Source);
		}
	}
}
