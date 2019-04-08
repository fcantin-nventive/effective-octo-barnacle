using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TEditor;
using TEditor.Abstractions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RTE.Uno
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

#pragma warning disable NV0016 // NV0016 - Do not create an async void method
		public async void OpenRTE(object sender, RoutedEventArgs e)
		{
			try
			{
				var text = "<!-- This is an HTML comment --><p>This is a test of the <strong style=\"font-size:20px\">TEditor</strong> by <a title=\"XAM consulting\" href=\"http://www.xam-consulting.com\">XAM consulting</a></p>";
					var response = await CrossTEditor.Current.ShowTEditor(text, autoFocusInput: true);
			}
			catch (Exception ex)
			{
				//TODO
				var exception = ex.Data;
			}
		}
#pragma warning restore NV0016 // NV0016 - Do not create an async void method
	}
}
